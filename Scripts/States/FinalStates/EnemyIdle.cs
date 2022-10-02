using Godot;
using System;

// Will inherit from enemy AI
public class EnemyIdle : State
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
        if(host.HP <= 0)
            return "Death";

        return null;
    }

    public override string StateName()
    {
        return "Idle";
    }
}