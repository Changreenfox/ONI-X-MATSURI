using Godot;
using System;

public class FlyAway : Fly
{
    public FlyAway(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        base.Enter();
        host.Velocity = new Vector2(1000, -1000);
    }

    public override void PlayAnimation()
    {
        if(facingRight)
            host.PlayAnimation("FlyRight");
        else
            host.PlayAnimation("FlyLeft");
    }
}