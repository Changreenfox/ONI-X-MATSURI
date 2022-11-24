using Godot;
using System;

//Base class for player walking & jumping state
public abstract class OnGround : PlayerMotion
{
	public override string HandlePhysics(float delta)
	{
		// Base here will be Motion, which will always return null
		string move = base.HandlePhysics(delta);
		if(host.IsOnFloor() && Input.IsActionPressed("Jump"))
			return "Jump";
		return move;
	}

	//Normal Attack
	protected override void Attack()
	{    
		string temp = StateName();
		if(host.FacingRight)
			temp += "Right";
		else
			temp += "Left";
		host.Attack(0, temp);
	}
	
}
