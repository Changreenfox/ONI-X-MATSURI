using Godot;
using System;
using System.Threading.Tasks;

public class CameraShake : Camera2D
{
	int shake_amount = 3;
	bool shaking = false;
	RandomNumberGenerator random = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		random.Randomize();
	}
	
	private async Task shaker()
	{
		while(shaking)
		{
			Offset = (new Vector2(random.RandiRange(-1 * shake_amount, shake_amount), 
								   random.RandiRange(-1 * shake_amount, shake_amount)));
		}
	}

	public void shake(int duration, int strength)
	{
		Timer shake_time = (Timer)GetNode("Timer");
		shake_amount = strength;
		shaking = true;
		shaker();
		return;
	}


	private void _on_Timer_timeout()
	{
		shaking = false;
		Offset = Vector2.Zero;
	}
}
