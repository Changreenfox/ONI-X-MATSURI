using Godot;
using System;

public class BossPhase2Attack : AIMotion
{
	bool finished = false;
	
	public BossPhase2Attack(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		base.Enter(); //Unless base is just State
		finished = false;
		//host.StateTimer.Start(3.0f);
		PlayAnimation();
		//Set any necessary variables here
	}

	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta); //Unless base is just State

		if(finished)
		{
			finished = false;
			
			return "Idle";
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
		host.GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Phase2Attack").SetLoop(true);
		host.GetNode<AnimationPlayer>("AnimationPlayer").Queue("Phase2Attack");
	}
	
	private async void FinishAnimation()
	{
		host.GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Phase2Attack").SetLoop(false);
		await ToSignal(host.GetNode<AnimationPlayer>("AnimationPlayer"), "animation_finished(Phase2Attack)");
	}
}
