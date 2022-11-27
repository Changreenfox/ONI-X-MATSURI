using Godot;
using System;

public class BossDeath : JustGravity
{
	private int deathCounter = 0;
	
	public BossDeath(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		host.Velocity = new Vector2(0, 0);
		deathCounter++; //increase the number of times he has died
		GD.Print("Entered: BossDeath\t|\t", "Death counter: ", deathCounter);
		
		if(deathCounter == 1){//if the boss has only died 1 time, enter Phase 2
			host.EmitManagedSignal(nameof(SignalManager.OniBossPhase2));
			host.ChangeState("BossAngry");
			host.HP = 3;
		}
		else if(deathCounter==2){ //else play die animation and show win screen after a bit
			Timer timer = (Timer)host.GetNode("BossDeathTimer");
			timer.Start(0.5f);
			//State abstract class will enter Death state
		}
	}
	
	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		return null;
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Death");
	}
}
