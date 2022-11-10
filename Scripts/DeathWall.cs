using Godot;
using System;

public class DeathWall : Area2D
{
    //Collision adh=justed to only interact with enemies
    public void HandleEnemy(KinematicBody2D collision)
    {
        collision.QueueFree();
    }
}
