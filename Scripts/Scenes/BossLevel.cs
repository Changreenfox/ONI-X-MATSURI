using Godot;
using System;

public class BossLevel : SceneBase
{
	private float curCamShakeStrength = 0.0f;
	RandomNumberGenerator random = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		gManager.Signals.Connect(nameof(SignalManager.OniBossAttacked), this, nameof(On_OniBoss_Attack));
		gManager.Signals.Connect(nameof(SignalManager.OniBossLanded), this, nameof(On_OniBoss_Land));
		
		random.Randomize();
		SetProcess(false);
	}
	
	public override void _Process(float delta)
	{
		if(curCamShakeStrength == 0)
		{
			SetProcess(false);
		}
		gManager.CurrentCamera.Offset = (new Vector2(random.RandfRange(-1 * curCamShakeStrength, curCamShakeStrength), 
				 			  random.RandfRange(-1 * curCamShakeStrength, curCamShakeStrength))) * delta;
	}

	public void ShakeCamera(float duration, float strength)
	{
		curCamShakeStrength += strength;
		Timer shakeTimer = new Timer();
		shakeTimer.Name = "ShakeTimer";
		AddChild(shakeTimer);
		shakeTimer.Connect("timeout", 
							this, 
							nameof(On_ShakeTimer_Timeout),
							new Godot.Collections.Array() { shakeTimer, strength }
							);
		shakeTimer.Start(duration);
		SetProcess(true);
	}


	public void On_OniBoss_Attack()
	{
		ShakeCamera(0.25f, 300.0f);
	}
	
	public void On_OniBoss_Land()
	{
		ShakeCamera(1.0f, 300.0f);
	}
	
	public void On_ShakeTimer_Timeout(Timer shakeTimer, float shakeStrength)
	{
		curCamShakeStrength -= shakeStrength;
		GD.Print("Timer finished");
		shakeTimer.QueueFree();
	}
}
