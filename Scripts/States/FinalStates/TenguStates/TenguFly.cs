using Godot;
using System;

public class TenguFly : Fly
{
    Actor target;
    public TenguFly(Actor _host)
    {
        host = _host;
        target = host.GManager.PlayerRef;
    }

    public override void Enter()
    {
        PlayAnimation();
        host.SetProcess(false);
    }

    public override string HandlePhysics(float delta)
    {
        
        return base.HandlePhysics(delta);
    }

    public override void Exit()
    {
        host.SetProcess(true);
    }

    public override void PlayAnimation()
    {
        if(host.GlobalPosition > target.GlobalPosition)
            host.PlayAnimation("FlyRight");
        else
            host.PlayAnimation("FlyLeft");
    }

    public override void HandleTimer()
    {
        
    }

    public override string StateName()
    {
        return "TenguFly";
    }
}