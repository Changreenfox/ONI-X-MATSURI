using Godot;
using System;

public class BonkArea : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    private String path = "../Tengu";

    private Tengu tengu;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tengu = (Tengu)GetNode(path);
        Connect("body_entered", tengu, "BonkHead");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
