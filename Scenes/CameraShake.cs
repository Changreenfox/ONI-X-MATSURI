using Godot;
using System;
using System.Threading.Tasks;

public class CameraShake : Camera2D
{
	private GameManager gManager;
	
	float shake_amount = 3;
	bool shaking = false;
	RandomNumberGenerator random = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		gManager.CurrentCamera = this;
		random.Randomize();
		SetProcess(false);
	}
	
	public override void _Process(float delta)
	{
		Offset = (new Vector2(random.RandfRange(-1 * shake_amount, shake_amount), 
				 			  random.RandfRange(-1 * shake_amount, shake_amount))) * delta;
	}

	public void Shake(float duration, float strength)
	{
		Timer shake_time = (Timer)GetNode("Timer");
		shake_time.Start(duration);
		shake_amount = strength;
		SetProcess(true);
		return;
	}


	public void _on_Timer_timeout()
	{
		SetProcess(false);
		Offset = Vector2.Zero;
	}
}
