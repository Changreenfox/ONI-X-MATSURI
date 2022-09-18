using Godot;
using System;

public class BulletSpawn : Node2D
{

    //private Bullet bullet;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //private Scene bullet = ResourceLoader.load("filepath.tscn") as Scene;
        return;
    }


    public void Fire(Vector2 direction)
    {
        Timer cooldownTimer = GetNode("CooldownTimer") as Timer;
        if(!(bool)(cooldownTimer?.IsStopped()))
            return;
        
        cooldownTimer.Start();
        /*
        Bullet newBullet = bullet.instance();
        newBullet.direction = direction;
        addChild(newBullet);
        */
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
