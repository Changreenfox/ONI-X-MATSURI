using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BasicAttack : Attack
{
	[Signal]
	public delegate void AttackFinished();

	//The range of the attack
	private List<CollisionPolygon2D> leftRanges;
	private List<CollisionPolygon2D> rightRanges;
	private bool rightAttack = true;
	private int currentFrame = 0;

	protected override void GetRange()
	{    
		leftRanges = new List<CollisionPolygon2D>();
		rightRanges = new List<CollisionPolygon2D>();

		//The first frame of attack doesn't have a collider
		rightRanges.Add(null);
		leftRanges.Add(null);

		//fill out the amount of ranges provided by each side
		foreach(Node child in GetChildren())
		{

			if(child is CollisionPolygon2D)
			{
				if(child.Name.Contains("Left"))
					leftRanges.Add((CollisionPolygon2D)child);
				else
					rightRanges.Add((CollisionPolygon2D)child);
			}
		}

		rightRanges.Add(null);
		leftRanges.Add(null);
	}

	public override void StartAttack(string prevAnim = "")
	{
		//GD.Print("StartAttack? ", !waiting, " ", host.CurrentAttack < 0, " ", time.IsStopped());
		//If you're not waiting for the attack to finish and the Time is stopped
		if(!waiting && host.CurrentAttack < 0 && time.IsStopped())
		{
			previousAnim = prevAnim;
			//Start waiting for the attack to finish
			waiting = true;

			//Allows a single entrance into the co-routine
			active = true;

			rightAttack = host.FacingRight;
		}
	}

	public void FinishAttack()
	{
		//Emit Signal
		EmitSignal(nameof(AttackFinished));
	}

	protected override async void ActivateCollider()
	{
		//Play the sound & animation (PlayAnimation MUST come before host.Attacking = true;)
		string temp = name;
		if(host.FacingRight)
			temp += "Right";
		else
			temp += "Left";
		host.PlayAnimation(temp);
		
		attacking = true;
		host.CurrentAttack = attackNumber;
		
		host.PlaySound("Attack");

		//Enable the attack range until animation is finished
		await ToSignal(this, "AttackFinished");

		if(host.HP <= 0)
			return;

		//Reset currentFrame
		currentFrame = 0;

		host.CurrentAttack = -1;

		//Restart previous animations
		host.PlayAnimation(previousAnim);

		rightAttack = host.FacingRight;

		await WaitCooldown();
	}

	protected override async Task WaitCooldown()
	{
		await base.WaitCooldown();
		host.AfterAttack();
	}

	//Called in animation player when the next collider is required
	public void updateFrame()
	{
		//?. operator means it calls if and only if the var is not null
		if(rightAttack)
		{
			if (currentFrame >= rightRanges.Count - 1)
			{
				FinishAttack();
				return;
			}
			rightRanges[currentFrame++]?.SetDeferred("disabled", true);
			rightRanges[currentFrame]?.SetDeferred("disabled", false);
		}
		else
		{
			if (currentFrame >= leftRanges.Count - 1)
			{
				FinishAttack();
				return;
			}
			leftRanges[currentFrame++]?.SetDeferred("disabled", true);
			leftRanges[currentFrame]?.SetDeferred("disabled", false);
		}
	}

	public override void Cancel()
	{
		if(rightAttack)
			rightRanges[currentFrame]?.SetDeferred("disabled", true);
		else
			leftRanges[currentFrame]?.SetDeferred("disabled", true);
		host.Animator.Stop(true);
		FinishAttack();
	}
}
