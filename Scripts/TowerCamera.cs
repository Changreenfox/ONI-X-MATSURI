using Godot;
using System;

public class TowerCamera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private GameManager gManager;
    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		if(Current)
			gManager.CurrentCamera = this;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
