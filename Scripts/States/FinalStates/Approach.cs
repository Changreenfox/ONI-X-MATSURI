using Godot;
using System;

//Approach towards the player and attack when the player is in range
public class Approach : AIMotion
{
    private Player player;
    private bool facingRight = false;

    public Approach(Actor _host)
    {
        host = _host;
        player = host.GManager.PlayerRef;
    }

    public override void Enter()
    {
        facingRight = host.GlobalPosition.DirectionTo(player.GlobalPosition).x >= 0;
        PlayAnimation();
    }

    public override string HandlePhysics(float delta)
    {
        Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
        direction.y = 0;
        host.Direction = direction;
        bool newFace = host.GlobalPosition.DirectionTo(player.GlobalPosition).x >= 0;
        if(facingRight != newFace)
        {
            facingRight = newFace;
            PlayAnimation();
        }

        return base.HandlePhysics(delta);
    }

    public override void PlayAnimation()
    {
        if(facingRight)
            host.PlayAnimation("WalkRight");
        else
            host.PlayAnimation("WalkLeft");
    }
}