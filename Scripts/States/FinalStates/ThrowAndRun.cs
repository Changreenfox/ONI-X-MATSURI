using Godot;
using System;

public class ThrowAndRun : AIMotion
{
    //BulletSpawner is stored in host.attacks[0]
    private Actor player;
    private BulletSpawn bs;
    private float timeFalling = 1.5f;

    public ThrowAndRun(Actor _host)
    {
        host = _host;
        player = host.GManager.PlayerRef;
        //This is guaranteed based on current scene hierarchy
        bs = (BulletSpawn)host.attacks[0];
    }

    public override void Enter()
    {
        //Play Animation is called in Throw function
        Throw();
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
        Throw();
    }

    public async void Throw()
    {
        //Stop moving
        host.Direction = Vector2.Zero;

        //Shoot


        Vector2 target = player.GlobalPosition;
        //target.x *= 0.95f;
        //Want to shoot just short of the player's current position,

        Vector2 heading = Vector2.Zero;
        //Want to reach the player, as if he were equal level, in 2s
        //X acceleration is 0, so x = vt + c, x is playerGlobal, c is Global, t is given, so
        //x = vt, and t = 2, so v = (x - c)/t
        heading.x = (target.x - bs.GlobalPosition.x) * 0.95f / timeFalling;
        if(heading.x < 0)
            heading.x = Mathf.Min(-100f, heading.x);
        else
            heading.x = Mathf.Max(heading.x, 100f);

        //We want to crash into the player after timeFalling seconds
        //y = gt^2 + vt + c, so v = (y - c - gt^2) / 2, where y is target - pos, c is pos, and g is in bulletSpawner
        //Remember that up is negative in this world
        heading.y = -1 * (target.y - bs.GlobalPosition.y - -0.5f * bs.Gravity * timeFalling * timeFalling) / timeFalling;

        //This should be shown in Attack
        bs.Heading = heading;
        GD.Print(heading);
        host.Attack(0);

        //Wait until the attack animation is complete
        await ToSignal(host.Animator, "animation_finished");

        //Run away again
        ChangeDirection();
    }

    public void ChangeDirection()
    {
        Vector2 direction = host.GlobalPosition.DirectionTo(player.GlobalPosition);
        direction.y = 0;
        direction.x *= -1;
        host.Direction = direction;
        bool newFace = direction.x > 0;
        if(facingRight != newFace)
            facingRight = newFace;
        PlayAnimation();
    }
}