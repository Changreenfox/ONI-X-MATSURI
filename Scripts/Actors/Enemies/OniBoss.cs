using Godot;
using System;

public class OniBoss : Enemy
{
	private bool cycled = false;
	public bool Cycled
	{
		get { return cycled; }
		set { cycled = value; }
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		
		container.SetState("Idle", new BossIdle(this));
		container.SetState("Death", new Death(this));
		container.SetState("Attack", new BossAttack(this));
		
		container.SetState("Motion", new BossMotion(this));
		
		direction.x = -1;

		state = container.GetState("Idle");
		state.Enter();
		
	}
	
	private void _on_BossWall_body_entered(object body)
	{
		if(body == this) 
		{
			velocity.x = 0;
			direction.x *= -1;
		}
	}
	
}
