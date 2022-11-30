using Godot;
using System;

public class RunAway : Motion
{
    //BulletSpawner is stored in host.attacks[0]
    private Actor player;
    private int runningDirection = 1;

    public RunAway(Actor _host)
    {
        host = _host;
        player = host.GManager.PlayerRef;
    }

    public override void Enter()
    {
        PlayAnimation();
        host.StateTimer.Start(0.5f);
        //Run away if within 500 horizontal ft
        Vector2 distance = player.GlobalPosition - host.GlobalPosition;
        if(Mathf.Abs(distance.x) < 500)
            runningDirection = -1;
        else
            runningDirection = 1;
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
        direction.x *= runningDirection;
        
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