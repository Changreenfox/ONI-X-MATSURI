using Godot;
using System;

public class BossPhase2Attack : AIMotion
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
		finished = false;
		prevDir = host.Direction;
		//host.StateTimer.Start(3.0f);
		PlayAnimation();
		//Set any necessary variables here
	}

	public override string HandlePhysics(float delta)
	{
		if(!active)
			return null;
		base.HandlePhysics(delta); //Unless base is just State
		if(prevDir.x != host.Direction.x)
		{
			finished = true;
			FinishAnimation();
			
		}
		return null;
	}
	
	public override void HandleTimer()
	{
		//finished = true;
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
		host.Animator.GetAnimation("Phase2Attack").SetLoop(true);
		host.Animator.Queue("Phase2Attack");
	}
	
	private async void FinishAnimation()
	{
		active = false;
		host.Animator.GetAnimation("Phase2Attack").SetLoop(false);
		await ToSignal(host.Animator, "animation_finished");
		host.ChangeState("Idle");
		active = true;
	}
}
