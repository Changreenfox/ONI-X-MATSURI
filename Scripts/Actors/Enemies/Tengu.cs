using Godot;
using System;

public class Tengu : Enemy
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        alertArea = GetNode<Area2D>("Alert");

        container.SetState("Falling", new Falling(this));
        container.SetState("Alert", new TenguAlert(this, "Fly"));
        container.SetState("Fly", new TenguFly(this));

        state = container.GetState("Falling");
        state.Enter();
    }

    public override void HandleAlert(KinematicBody2D player)
    {
        alertArea.SetDeferred("monitoring", false);
        ChangeState("Alert");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
