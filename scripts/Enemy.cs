using Godot;
using System;

public partial class Enemy : Character
{
	private MoveComponent _move;

    public override void _Ready()
    {
        _move = GetNode<MoveComponent>("MoveComponent");
		//MoveCollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Velocity = velocity;
	}

}
