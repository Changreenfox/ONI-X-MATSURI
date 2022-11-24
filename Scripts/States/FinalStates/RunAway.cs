using Godot;
using System;

public class RunAway : Motion
{
    //BulletSpawner is stored in host.attacks[0]
    private Actor player;

    public RunAway(Actor _host)
    {
        host = _host;
        player = host.GManager.PlayerRef;
    }

    public override void Enter()
    {
        PlayAnimation();
        host.StateTimer.Start(0.5f);
    }

    public override string HandlePhysics(float delta)
    {
        return base.HandlePhysics(delta);
    }

    public override void PlayAnimation()
    {
        if(facingRight)
            host.PlayAnimation("WalkRight");
        else
            host.PlayAnimation("WalkLeft");
    }

    // Timer gets called in the attack script (check tree heirarchy, AttackCooldown)
    public override void HandleTimer()
    {
        host.ChangeState("Throw");
    }

    //Overriden because direction is updated every physics frame
    protected override void GetInputDirection()
    {
        Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
        direction.y = 0;
        //We're running away!
        direction.x *= -1;
        host.Direction = direction;
        facingRight = direction.x > 0;
        host.FacingRight = facingRight;
        PlayAnimation();
    }

    public override string StateName()
    {
        return "RunAway";
    }

}