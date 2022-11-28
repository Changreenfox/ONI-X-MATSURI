using Godot;
using System;
using System.Collections.Generic;

public class TenguFly : Fly
{
    private Actor target;
    private List<Area2D> positions;
    private int currentPos = -1;
    private bool nearCeiling = false;
    private Area2D currentTarget;

    public TenguFly(Actor _host)
    {
        host = _host;
        target = host.GManager.PlayerRef;

        positions = new List<Area2D>();
        Node positionList = host.GetParent().GetNode("TenguSpots");
        foreach(Area2D pos in positionList.GetChildren())
        {
            positions.Add(pos);
        }
    }

    public override void Enter()
    {
        host.SetProcess(false);
        //TenguFly is always moving at a set speed
        host.Velocity = new Vector2(host.Speed, host.Speed);

        //Begins the loop
        currentPos = (currentPos + 1) % positions.Count;
        currentTarget = positions[currentPos];
        currentTarget.SetDeferred("monitoring", true);

        GetInputDirection();
        PlayAnimation(); 
    }

    public override void Exit()
    {
        host.SetProcess(true);
        host.Velocity = Vector2.Zero;
    }

    protected override void HandleMotion(float xSpeed, float ySpeed, float delta = 0)
    {
        //Inheritly, xSpeed and ySpeed = host.Speed
        Vector2 desiredVelocity = host.Direction;
        desiredVelocity.x *= host.MaxSpeed;
        desiredVelocity.y *= host.MaxSpeed;

        desiredVelocity = (desiredVelocity - host.Velocity) * delta * 4;
        host.Velocity += desiredVelocity;

        host.Move(host.Velocity);
    }

    public override void PlayAnimation()
    {
        if(facingRight)
            host.PlayAnimation("FlyRight");
        else
            host.PlayAnimation("FlyLeft");
    }

    protected override void GetInputDirection()
    {
        float test = currentTarget.GlobalPosition.x - host.GlobalPosition.x;

        //Change which way we're facing
        if(test > 0.5)
            facingRight = true;
        else if (test < 0.5)
            facingRight = false;

        host.FacingRight = facingRight;
        //Set the direction
        host.Direction = host.GlobalPosition.DirectionTo(currentTarget.GlobalPosition);
    }

    //Here, this is activated by running into a collider (called in Tengu.cs)
    //In this instance, that collider says for the Tengu to Attack
    public override void HandleTimer()
    {
        currentTarget.SetDeferred("monitoring", false);
        HandleAttack();
    }

    private async void HandleAttack()
    {
        //facingRight depends on currentTarget
        //Not as dynamic, there is probably a better solution here
        facingRight = currentPos == 0;
        host.FacingRight = facingRight;
        Attack();
        await ToSignal(host.Animator, "animation_finished");

        //TO-Do
        //Make sure we wait until after attacking to switch states to TenguBonkHead

        //Loops infinitely through the potential targets one at a time
        currentPos = (currentPos + 1) % positions.Count;
        currentTarget = positions[currentPos];
        currentTarget.SetDeferred("monitoring", true);
    }

    //Spawning the bullets is handled in the animation
    protected override void Attack()
    {
        if(facingRight)
            host.PlayAnimation("AttackRight");
        else
            host.PlayAnimation("AttackLeft");
    }

    public override string StateName()
    {
        return "TenguFly";
    }
}