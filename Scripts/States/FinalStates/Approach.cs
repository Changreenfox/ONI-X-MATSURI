using Godot;
using System;

//Approach towards the player and attack when the player is in range
public class Approach : AIMotion
{
    private Player player;
    public Approach(Actor _host)
    {
        host = _host;
    }

    public override void Enter()
    {
        player = host.GManager.PlayerRef;
    }

    public override string HandlePhysics(float delta)
    {
        Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
        direction.y = 0;
        host.Direction = direction;
        return base.HandlePhysics(delta);
    }
}