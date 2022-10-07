using Godot;
using System;

public class OniBoss : Enemy
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Death", new Death(this));
		
		container.SetState("Moving", new Moving(this));

		state = container.GetState("Moving");
		state.Enter();
		
	}
	
	private void _on_BossLeftWall_body_entered(object body)
	{
		direction.x = 1;
	}


	private void _on_BossRightWall_body_entered(object body)
	{
		direction.x = -1;
	}
	
}
