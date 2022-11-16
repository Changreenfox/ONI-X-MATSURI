using Godot;
using System;

//Meant to play a cheeky animation
public class Lost : JustGravity
{
    public Lost(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        host.Velocity = new Vector2(0, host.Velocity.y);
        PlayAnimation();
        Process();
    }

    public async void Process()
    {
        await ToSignal(host.Animator, "animation_finished");
        host.ChangeState("Idle");
    }

    public override void Exit()
    {
        host.AfterLost();
    }

    public override string StateName()
    {
        return "Lost";
    }

    public override void PlayAnimation()
    {
        host.PlayAnimation("Lost");
    }
}