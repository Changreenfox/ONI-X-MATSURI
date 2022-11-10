using Godot;
using System;

public class PitCheck : Area2D
{
    private Node2D RespawnNode;
    public override void _Ready()
    {
        RespawnNode = GetParent().GetNode<Node2D>("RespawnPoint");
    }

    public void OnPitCheck(KinematicBody2D collision)
    {
        //Actors might behave differently from normal objects that fall to oblivion
        if(collision is Actor)
        {
			((Actor)collision).Reset(1, RespawnNode);
        }
        else
            collision.QueueFree();
    }

}
