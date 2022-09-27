using Godot;
using System;

public class Idle : OnGround
{
	private float time = 0;
	private float faceForwardCooldown = 1.5f;

	public Idle(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		base.Enter();
		time = 0;
		velocity.x = 0;
		host.Velocity = velocity;
		host.PlayAnimation("Idle");
	}

	public override string HandlePhysics(float delta)
	{
		string temp = base.HandlePhysics(delta);
		time += delta;
		if (time > faceForwardCooldown)
			host.PlayAnimation("IdleForward");
		if(-0.01 > velocity.x || velocity.x > 0.01)
			return "Walk";

		return temp;
	}

	public override string StateName()
	{
		return "Idle";
	}
}
