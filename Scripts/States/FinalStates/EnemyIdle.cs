using Godot;
using System;

// Will inherit from enemy AI
public class EnemyIdle : AIMotion
{
    public EnemyIdle(Actor _host)
    {
        host = _host;
    }
    
    public override void Enter()
    {
        host.PlayAnimation("Idle");
    }

    public override string HandlePhysics(float delta)
    {
        return base.HandlePhysics(delta);
    }

    public override string StateName()
    {
        return "Idle";
    }
}