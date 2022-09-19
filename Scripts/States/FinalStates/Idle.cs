using Godot;
using System;

public class Idle : OnGround
{
    public Idle(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        base.Enter();
        velocity.x = 0;
        host.FacingRight = true;
        host.Velocity = velocity;
        host.PlayAnimation("Idle");
    }

    public override string HandlePhysics(float delta)
    {
        string temp = base.HandlePhysics(delta);
        if(-0.01 > velocity.x || velocity.x > 0.01)
            return "Walk";

        return temp;
    }

    public override string StateName()
    {
        return "Idle";
    }
}