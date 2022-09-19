using Godot;
using System;

// Base class for walking & jumping
public abstract class Motion : State
{
    [Export]
    protected float speed = 90;
    [Export]
    protected float maxSpeed = 600;
    [Export]
    protected float gravity = 40;
    [Export]
    protected float maxFallSpeed = 5000;

    protected Vector2 direction = new Vector2();
    protected Vector2 velocity = new Vector2();

    public override void Enter()
    {
        velocity = host.GetVelocity();
        direction = host.Direction;
    }

    // Input we're looking for is attacking
    public override string HandlePhysics(float delta)
    {
        velocity.y += gravity;

        velocity.y = Mathf.Min(maxFallSpeed, velocity.y);
        // Might be better to not make this a state
        if (Input.IsActionPressed("Attack"))
            Attack();

        //Update direction
        GetInputDirection();

        //If no direction, we slow until motion.x == 0
        if(direction.Equals(Vector2.Zero))
        {
            velocity.x = Mathf.Lerp(velocity.x, 0, 0.15f);
        }
        else
        {
            velocity.x += direction.x * speed;
        }
        

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        
        velocity = host.Move(velocity);

        return null;
    }

    protected void GetInputDirection()
    {
        float goRight = Input.GetActionStrength("MoveRight");
		float goLeft = Input.GetActionStrength("MoveLeft");
        if(goRight != goLeft)
            host.FacingRight = goRight > goLeft;

        float jump = Input.GetActionStrength("Jump");
        direction = new Vector2(goRight - goLeft, jump);
        host.Direction = direction;
    }

    protected abstract void Attack();
}