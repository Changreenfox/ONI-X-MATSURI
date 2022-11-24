using Godot;
using System;

//Meant to play a cheeky animation
public class Alert : JustGravity
{
	protected string next;

	public Alert(Actor _host, string _next)
	{
		host = _host;
		next = _next;
	}

	public override void Enter()
	{
		host.Velocity = new Vector2(0, host.Velocity.y);
		PlayAnimation();
		Process();
	}

	public async void Process()
	{
		GD.Print("Waiting");
		await ToSignal(host.Animator, "animation_finished");
		host.ChangeState(next);
		GD.Print("done");
	}

	public override string StateName()
	{
		return "Alert";
	}

	public override void Exit()
	{
		host.AfterAlert();
	}

	public override void PlayAnimation()
	{
		host.PlayAnimation("Alert");
	}
}
