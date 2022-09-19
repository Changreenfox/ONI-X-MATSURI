using Godot;
using System;

// Finite State Machine for the Player
public class Player : Actor
{
	public override void _Ready()
	{
		//Set the character sprite
		base._Ready();

		container.SetState("Idle", new Idle(this));
		container.SetState("Walk", new Walk(this));
		container.SetState("Jump", new Jump(this));
		container.SetState("Death", new Death(this));

		/*
		Node states = GetNode("States");
		container.SetState("Idle", (State)states.GetNode("Idle"));
		container.SetState("Walk", (State)states.GetNode("Walk"));
		container.SetState("Jump", (State)states.GetNode("Jump"));
		*/

		state = container.GetState("Idle");
	}

<<<<<<< Updated upstream
    public override void _PhysicsProcess(float delta)
    {
        if(!active)
            return;
        string name = state.HandlePhysics(delta);
        if(name != null)
            ChangeState(name);
        
        Vector2 scale = character.Scale;
        
		if(facingRight)
			scale.x = 1;
		else
			scale.x = -1;

        foreach (Node2D node in attacks)
                node.Scale = scale;
=======
	public override void _PhysicsProcess(float delta)
	{
		if(!active)
			return;
		string name = state.HandlePhysics(delta);
		if(name != null)
			ChangeState(name);
	}

	public override void _Process(float delta)
	{
		if(!active)
			return;
		state.HandleProcess(delta);

		Vector2 scale = character.Scale;
		facingRight = velocity.x >= 0;
		if(facingRight)
		{
			scale.x = 1;
			foreach (Node2D node in attacks)
				node.Scale = scale;
			
		}
		else
		{
			scale.x = -1;
			foreach (Node2D node in attacks)
				node.Scale = scale;
		}
>>>>>>> Stashed changes
		character.Scale = scale;
	}

<<<<<<< Updated upstream
    public override void _Process(float delta)
    {
        if(!active)
            return;
        state.HandleProcess(delta);
    }

=======
>>>>>>> Stashed changes
	public void ChangeState(string name)
	{
		GD.Print(name);
		state = container.GetState(name);
		state.Enter();
	}
}
