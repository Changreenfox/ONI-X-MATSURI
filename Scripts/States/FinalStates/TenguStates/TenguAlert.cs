using Godot;
using System;

public class TenguAlert : Alert
{
    public TenguAlert(Actor _host, string _next) : base (_host, _next)
	{
        host = _host;
        next = _next;
	}

    public override void Enter()
    {
        host.Velocity = Vector2.Zero;
        host.Gravity = 0;
        base.Enter();
    }
}