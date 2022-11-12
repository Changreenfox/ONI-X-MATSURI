using Godot;
using System;

public class Throw : JustGravity
{
	public Throw(Actor _host)
	{
		host = _host;
		host.Velocity = new Vector2(0, host.Velocity.y);
	}

	public override void Enter()
	{
		//Stop moving
		host.Velocity = new Vector2(0, host.Velocity.y);
		//Shooting is called in AnimationPlayer
		PlayAnimation();
	}

	public override string HandlePhysics(float delta)
	{
		return base.HandlePhysics(delta);
	}

	//He will throw behind him
	public override void PlayAnimation()
	{
		if(host.FacingRight)
			host.PlayAnimation("AttackLeft");
		else
			host.PlayAnimation("AttackRight");
	}
}
