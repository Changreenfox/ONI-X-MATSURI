using Godot;
using System;
using System.Collections.Generic;

public class Tengu : Enemy
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private bool nearCeiling = false;
    public bool NearCeiling
    {
        get { return nearCeiling; }
    }

    private List<BulletSpawn> spawners;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        alertArea = GetNode<Area2D>("Alert");

        container.SetState("Falling", new Falling(this));
        container.SetState("Alert", new TenguAlert(this, "Fly"));
        container.SetState("Fly", new TenguFly(this));
        container.SetState("Attack", new TenguAttack(this));

        state = container.GetState("Falling");
        state.Enter();

        //Ready the Spawners
        spawners = new List<BulletSpawn>();
        Node spawnerParent = GetNode("BulletSpawns");
        foreach(BulletSpawn bs in spawnerParent.GetChildren())
        {
            spawners.Add(bs);
        }
    }

    public override void HandleAlert(KinematicBody2D player)
    {
        alertArea.SetDeferred("monitoring", false);
        ChangeState("Alert");
    }

    //Handle Timer gets called here, but it's just used to signal we've hit a collider
    public void TargetReached(KinematicBody2D host)
    {
        state.HandleTimer();
    }

    public void ShootRight()
    {
        //Shoot from bulletSpawners [3][4][5]
        for(int i = 0; i < 3; ++i)
        {
            spawners[i].StartAttack();
        } 
    }

    public void ShootLeft()
    {
        //Shoot from bulletSpawners [0][1][2]
        for(int i = 3; i < 6; ++i)
        {
            spawners[i].StartAttack();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
