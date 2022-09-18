using Godot;
using System;

public class Walk : OnGround
{
    public Walk(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        base.Enter();
        host.PlayAnimation("Walk");
    }

    public override string HandlePhysics(float delta)
    {
        string temp = base.HandlePhysics(delta);
        if(-0.01 < velocity.x && velocity.x < 0.01)
            return "Idle";
        return temp;
    }

    public override string StateName()
    {
        return "Walk";
    }
}