using Godot;
using System;

//Meant to play a cheeky animation
public class Alert : JustGravity
{
    private string next;
    private Sprite alert;

    public Alert(Actor _host, string _next)
    {
        host = _host;
        next = _next;
        alert = (Sprite)host.GetNode("AlertSprite");
    }

    public override void Enter()
    {
        alert.Visible = true;
        PlayAnimation();
        Process();
    }

    public async void Process()
    {
        await ToSignal(host.Animator, "animation_finished");
        alert.Visible = false;
        host.ChangeState(next);
    }

    public override void PlayAnimation()
    {
        host.PlayAnimation("Alert");
    }
}