using Godot;
using System;

public class Bullet : KinematicBody2D
{
    [Export]
    private int damage = 1;

    [Export]
    private float speed = 30;

    [Export]
    private Vector2 impulse = new Vector2(500,750);

    private Vector2 direction = new Vector2(0,0);
    public Vector2 Heading
    {
        get{ return direction; }
        set{ direction = value; }
    }

    public override void _Ready()
    {
        KillBulletTimer();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(float delta)
    {
        //Kinematic Collision captures a collision if it occurs during the movement... this is how we damage :)
        KinematicCollision2D collisionInfo = MoveAndCollide(direction.Normalized() * speed);

        //We adjust the mask so it can only collide with a selected Actor
        if (collisionInfo?.Collider != null)
        {
            Actor damaged = (Actor)collisionInfo.Collider;
            damaged.TakeDamage(damage, GlobalPosition, impulse);
            QueueFree();
        }
    }

    public async void KillBulletTimer()
    {
        await ToSignal(GetTree().CreateTimer(1000), "timeout");
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
