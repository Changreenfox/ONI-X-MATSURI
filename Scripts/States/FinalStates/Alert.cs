using Godot;
using System;

//Meant to play a cheeky animation
public class Alert : JustGravity
{
    private string next;

    public Alert(Actor _host, string _next)
    {
        host = _host;
        next = _next;
    }

    public override void Enter()
    {
        PlayAnimation();
        Process();
    }

    public async void Process()
    {
        await ToSignal(host.Animator, "animation_finished");
        host.ChangeState(next);
    }

    public override void PlayAnimation()
    {
        host.PlayAnimation("Alert");
    }
}