using Godot;
using System;

public partial class Enemy : Character
{

    public override void _Ready()
    {
		//MoveCollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Velocity = velocity;
	}

}
