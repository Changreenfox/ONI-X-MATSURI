using Godot;
using System;

public class BossPhase2Attack : Motion
{
	bool finished = false;
	private bool active = true;
	Vector2 prevDir;
	
	public BossPhase2Attack(Actor _host)
	{
		host = _host;
		prevDir = host.Direction;
	}

	public override void Enter()
	{
		base.Enter(); //Unless base is just State
		prevDir = host.Direction;
		host.StateTimer.Start(5.0f);
		//host.StateTimer.Start(3.0f);
		PlayAnimation();
		//Set any necessary variables here
	}

	public override string HandlePhysics(float delta)
	{
		if(!active)
			return null;
		
		base.HandlePhysics(delta); //Unless base is just State

		//This happens when the boss hits the wall
		/*if(prevDir.x != host.Direction.x)
		{
			FinishAnimation();
			
		}*/
		return null;
	}
	
	public override void HandleTimer()
	{
		FinishAnimation();
	}

	public override string StateName()
	{
		//just return the name of the State in the state machine and animator
		//For example where new BossMotion = "Motion" in the FSM
		//	return "Motion"
		return "Phase2Attack";
	}

	public override void PlayAnimation()
	{
		//Make any calls to playing animation in here. Should be handled in the state that calls it
		host.PlayAnimation("ChargeAttack");
		host.Animator.GetAnimation("Attack2").Loop = true;
		host.Animator.Queue("Attack2");
	}
	
	private async void FinishAnimation()
	{
		SetPhysicsProcess(false);

		host.Animator.GetAnimation("Attack2").Loop = false;
		await ToSignal(host.Animator, "animation_finished");
		host.ChangeState("Phase2Idle");

		SetPhysicsProcess(true);
	}
}
