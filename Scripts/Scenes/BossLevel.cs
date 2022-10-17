using Godot;
using System;

public class BossLevel : SceneBase
{
	private const float camShakeDur = 0.25f;
	private const float camShakeStrength = 300f;
	private const float shakeTimerDur = 1.0f;
	RandomNumberGenerator random = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		gManager.Signals.Connect(nameof(SignalManager.OniBossLanded), this, nameof(ShakeCamera));
		
		random.Randomize();
		SetProcess(false);
	}
	
	public override void _Process(float delta)
	{
		gManager.CurrentCamera.Offset = (new Vector2(random.RandfRange(-1 * camShakeStrength, camShakeStrength), 
				 			  random.RandfRange(-1 * camShakeStrength, camShakeStrength))) * delta;
	}

	public void ShakeCamera()
	{
		Timer shakeTimer = new Timer();
		shakeTimer.Name = "ShakeTimer";
		AddChild(shakeTimer);
		shakeTimer.Connect("timeout",
							this,
							nameof(On_ShakeTimer_Timeout)
							);
		shakeTimer.Start(shakeTimerDur);
		SetProcess(true);
		return;
	}


	public void On_ShakeTimer_Timeout()
	{
		SetProcess(false);
		gManager.CurrentCamera.Offset = Vector2.Zero;
	}
}
