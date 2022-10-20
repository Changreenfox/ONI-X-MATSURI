using Godot;
using System;

public class BossAttack : JustGravity
{
	//private bool finished = false;
	private int NUM_OF_EXTRA_ATTACKS = 2;
	
	public BossAttack(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		//finished = false;
		GD.Print("BossAttack");
		//host.StateTimer.Start(3.0f);
		
	}
	
	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		if(!host.Animator.IsPlaying())
		{
			return "Idle";
		}
		return null;
	}
	
	public override string StateName()
	{
		return "Attack";
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("ChargeAttack");	//Contains the first attack
		host.Animator.GetAnimation("Attack").Loop = false;
		for(int i = 0; i < NUM_OF_EXTRA_ATTACKS; ++i) 
		{
			host.Animator.Queue("Attack");
		}
	}
	public override void HandleTimer()
	{
		//finished = true;
	}
}
