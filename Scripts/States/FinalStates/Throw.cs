using Godot;
using System;

public class Throw : JustGravity
{
	private Actor player;

	public Throw(Actor _host)
	{
		host = _host;
		host.Velocity = new Vector2(0, host.Velocity.y);
		player = host.GManager.PlayerRef;
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

	public override void PlayAnimation()
	{
		//Always shoot towards the player
		Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
		if(direction.x < 0)
			host.PlayAnimation("AttackLeft");
		else
			host.PlayAnimation("AttackRight");
	}

	public override string StateName()
    {
        return "Throw";
    }

}
