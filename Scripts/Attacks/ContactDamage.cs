using Godot;
using System;
using System.Threading.Tasks;

public class ContactDamage : Attack
{
	//The range of the attack
	private CollisionShape2D range;

	protected override void GetRange()
	{    
		range = (CollisionShape2D)GetNode("Range");
	}
}
