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
        velocity = host.Velocity;
        velocity.x = 0;
        host.Velocity = velocity;
        host.PlayAnimation("Idle");
    }

    public override string HandlePhysics(float delta)
    {
        //temp could be jump. What should we do?
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