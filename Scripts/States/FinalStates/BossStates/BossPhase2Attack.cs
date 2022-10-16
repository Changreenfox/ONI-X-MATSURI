using Godot;
using System;

public class BossPhase2Attack : AIMotion
{

	public BossPhase2Attack(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		base.Enter(); //Unless base is just State
		PlayAnimation();
		//Set any necessary variables here
	}

	public override string HandlePhysics(float delta)
	{
		string temp = base.HandlePhysics(delta); //Unless base is just State

		// If you need the character to move, handle it here

		// Can be used to check for state switches, in which case return the name of the next state as a string
		/* Example:
		//	if(Input.Equals("Jump"))
		//		return "Jump"      */

		return temp; //Unless base is just State, then return null;
	}

	public override string StateName()
	{
		//just return the name of the State in the state machine and animator
		//For example where new BossMotion = "Motion" in the FSM
		//	return "Motion"
		return "Phase2Attack";
	}

	public override void PlayAnimation()
	{
		//Make any calls to playing animation in here. Should be handled in the state that calls it
		host.PlayAnimation("ChargeAttack");
		host.GetNode<AnimationPlayer>("AnimationPlayer").Queue("Phase2Attack");
	}
	
}
