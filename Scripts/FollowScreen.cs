using Godot;
using System;

public class FollowScreen : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    GameManager gManager;
    Camera2D camera;

    Vector2 prevPos = Vector2.Zero; 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gManager = (GameManager)GetNode("/root/GameManager");
        camera = gManager.CurrentCamera;
        prevPos = camera.GetCameraScreenCenter();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        Vector2 newPos = camera.GetCameraScreenCenter();
        if(newPos.Equals(prevPos))
            return;

        Position += (newPos - prevPos);
        prevPos = newPos;
    }
}
