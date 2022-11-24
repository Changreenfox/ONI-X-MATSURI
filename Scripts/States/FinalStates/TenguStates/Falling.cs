using Godot;
using System;

//Class should only be used for a flying enemy who innately has 0 gravity
public class Falling : JustGravity
{
    [Export]
    private float gravity = 40;

    public Falling(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        host.Gravity = gravity;
        base.Enter();
    }

    public override void Exit()
    {
        host.Gravity = 0;
        base.Exit();
    }

    public override void PlayAnimation()
    {
        host.PlayAnimation("Dive");
    }

    public override string StateName()
    {
        return "Falling";
    }
}