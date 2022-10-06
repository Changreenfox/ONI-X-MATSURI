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

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
