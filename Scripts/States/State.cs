using Godot;
using System;

public abstract class State : Node2D
{
	protected Actor host;

	// Initialize the state and change the animation
	public abstract void Enter();

	// Clean up the state, reinitialize timers, etc, virtual because it won't always exist
	public virtual void Exit()
	{
		return;
	}

	// Called when PhysicsProcess is called in FSM
	public abstract string HandlePhysics(float delta);

	// Called when Process is called in FSM
	public virtual string HandleProcess(float delta)
	{
		if(host.HP <= 0)
			return "Death";
		return null;
	}

	//If anything needs to happen when the animation finished
	public virtual void _on_Animation_Finish(string animName)
	{
		return;
	}

	public virtual string StateName()
	{
	   return "State"; 
	}
	
	public virtual void PlayAnimation()
	{
		return;
	}
}
