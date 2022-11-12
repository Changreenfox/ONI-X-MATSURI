using Godot;
using System;

public class BruteAttack : JustGravity
{
	public BruteAttack(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		base.Enter();
		host.Velocity = Vector2.Zero;
		velocity = host.Velocity;
		host.Attack(0);
	}

	public override string HandlePhysics(float delta)
	{
		string temp = base.HandlePhysics(delta);
		if(Mathf.Abs(velocity.x) > 0.5f)
			return "Approach";
		return temp;
	}

	public override void Exit()
	{
		//Interrupt if attack is still occuring
		host.Interrupt();
	}

	public override void PlayAnimation()
	{
		return;
	}

	public override string StateName()
	{
		return "BruteAttack";
	}
}
