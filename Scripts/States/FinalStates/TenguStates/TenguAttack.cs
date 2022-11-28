using Godot;
using System;

//Plays an animation that spawns bullets in it
public class TenguAttack : Fly
{
    public TenguAttack(Actor _host)
    {
        host = _host;
    }
}