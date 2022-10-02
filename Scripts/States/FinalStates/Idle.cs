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
		Vector2 velocity = host.Velocity;
		velocity.x = 0;
		host.Velocity = velocity;
	}

	public override string HandlePhysics(float delta)
	{
		string move = base.HandlePhysics(delta);
		if (time > faceForwardCooldown)
			host.PlayAnimation("IdleForward");
		else
			time += delta;
			
		if(-0.01 > host.Velocity.x || host.Velocity.x > 0.01)
			return "Walk";

		return move;
	}

	public override string StateName()
	{
		return "Idle";
	}

	public override void PlayAnimation()
	{
		host.PlayAnimation("Idle");
	}

	protected override void Attack()
	{
		base.Attack();
		time = 0;
	}
}
