using Godot;
using System;
using Godot.Collections;

//We don't need to dynamically re-create new States if we store them in this container
public class StateContainer
{
    private Dictionary<string, State> states = new Dictionary<string, State>();

    public void SetState(string name, State state)
    {
        states.Add(name, state);
    }

    public State GetState(string name)
    {
        return states[name];
    }
}