using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BasicAttack : Attack
{
	//The range of the attack
	private List<CollisionPolygon2D> leftRanges;
	private List<CollisionPolygon2D> rightRanges;
	private bool rightAttack = true;
	private int currentFrame = 0;

	public override void _Ready()
	{
		leftRanges = new List<CollisionPolygon2D>();
		rightRanges = new List<CollisionPolygon2D>();
		base._Ready();
	}

	protected override void GetRange()
	{    
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
		//If you're not waiting for the attack to finish and the Time is stopped
		//GD.Print(!waiting, " and ", time.IsStopped());
		if(!waiting && !host.Attacking && time.IsStopped())
		{
			previousAnim = prevAnim;
			//Start waiting for the attack to finish
			waiting = true;

			//Allows a single entrance into the co-routine
			active = true;

			rightAttack = host.FacingRight;


		}
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
		host.Attacking = true;
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,
										"Attack"
										);

		//Enable the attack range until animation is finished
		await ToSignal(host.Animator, "animation_finished");

		//Reset currentFrame
		currentFrame = 0;

		//Reset attacking variables
		attacking = false;
		host.Attacking = false;

		//Restart previous animations
		host.PlayAnimation(previousAnim);

		rightAttack = host.FacingRight;

		await WaitCooldown();
	}

	//Called in animation player when the next collider is required
	public void updateFrame()
	{
		//?. operator means it calls if and only if the var is not null
		if(rightAttack)
		{
			rightRanges[currentFrame++]?.SetDeferred("disabled", true);
			rightRanges[currentFrame]?.SetDeferred("disabled", false);
		}
		else
		{
			leftRanges[currentFrame++]?.SetDeferred("disabled", true);
			leftRanges[currentFrame]?.SetDeferred("disabled", false);
		}
	}
}