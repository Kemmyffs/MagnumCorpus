using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Reflection.Metadata;

public partial class Player : Character
{
	public Vector2 LastDirection { get; private set; } = Vector2.Right;
	[Export] public float ChargeDelay = 5.0f;


	public override void _PhysicsProcess(double delta)
	{

		Vector2 direction = Input.GetVector(
			"plr_left", "plr_right",
			"plr_up", "plr_down"
		);

		_moveComponent.DesiredDirection = direction;

		if (direction != Vector2.Zero)
			LastDirection = direction;

		if (Input.IsActionPressed("plr_charge"))
			if (_healthComponent.hasEnoughSpecial(ChargesAmountInFullBar))
			{
				_moveComponent.StartCharge();
			}
		if (Input.IsActionJustReleased("plr_charge"))
		{

			_moveComponent.StopCharge();
			_moveComponent.Dash(LastDirection);

		}

		if (Input.IsActionJustPressed("plr_attack"))
		{
			if (_attackComponent.CanAttack) _attackComponent.Attack(LastDirection);
		}

		if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}
	}

	public override void Die()
	{
		GetNode<AnimatedSprite2D>("AttackComponent/AnimatedSprite2D").Stop();
		BaseSpeed = 0;
		GetNode<AnimationPlayer>("CanvasLayer/HUD/AnimationPlayer").Play("transition_out");
	}


}