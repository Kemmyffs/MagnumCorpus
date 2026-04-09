using Godot;
using System;

public partial class Enemy : Character
{

    public override void _Ready()
    {

    }


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Velocity = velocity;
	}

}
