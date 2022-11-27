using Godot;
using System;


public class BossStart : JustGravity
{
	public BossStart(Actor _host)
	{
		host = _host;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override string StateName()
	{
		return "Start";
	}
	
	public override string HandlePhysics(float delta)
	{
		string temp = base.HandlePhysics(delta); //Unless base is just State
		if(host.IsOnFloor()){
			host.GManager.Signals.EmitSignal(nameof(SignalManager.OniBossLanded));
			host.PlaySound("Land");
			return "Idle";
			
		}
		return temp;
	}
	
}
