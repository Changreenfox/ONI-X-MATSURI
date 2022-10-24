using Godot;
using System;

public class LastChunk : Node2D
{
	public void _on_EndWall_body_entered(object body)
	{
		GetTree().ChangeScene("res://Scenes/Tower.tscn");
	}
}
