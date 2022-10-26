using Godot;
using System;

//Meant to play a cheeky animation
public class Alert : State
{
    public Alert(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        PlayAnimation();
        Process();
    }

    public async void Process()
    {
        await ToSignal(host.Animator, "animation_finished");
        host.ChangeState("Approach");
    }

    public override void PlayAnimation()
    {
        host.PlayAnimation("Alert");
    }
}