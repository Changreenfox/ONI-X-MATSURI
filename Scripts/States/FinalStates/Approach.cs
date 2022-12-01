using Godot;
using System;

//Approach towards the player and attack when the player is in range
public class Approach : Motion
{
    private Player player;

    public Approach(Actor _host)
    {
        host = _host;
        player = host.GManager.PlayerRef;
    }

    public override void Enter()
    {
        if(player.HP <= 0)
        {
            host.ChangeState("Idle");
            return;
        }
        host.FacingRight = host.GlobalPosition.DirectionTo(player.GlobalPosition).x >= 0;
        facingRight = host.FacingRight;
        PlayAnimation();
    }

    public override string HandlePhysics(float delta)
    {
        Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
        direction.y = 0;
        host.Direction = direction;
        facingRight = host.GlobalPosition.DirectionTo(player.GlobalPosition).x >= 0;
        if(host.FacingRight != facingRight)
        {
            host.FacingRight = facingRight;
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

    public override string StateName()
    {
        return "Approach";
    }
}