using Godot;
using System;

public static class SpeedEnum
{
	public const float Default = 300.0f;
	public const float Sprint = 400.0f;
	public const float Dash = 800.0f;
	public const float Slowed = 150.0f;
}


public partial class Player : CharacterBody2D
{
	public float Speed = SpeedEnum.Default;
	public float Dash = 0.0f;
	public float Speed_DashDecay = 25.0f;
	public float Speed_ChargeDecay = 10.0f;
	public float animatedSprite_weaponSlash_Offset = 20;

    private Godot.Vector2 lastDirection = Vector2.Right;

    AnimatedSprite2D animatedSprite_weaponSlash;

    public Vector2 LastDirection { get => lastDirection; set => lastDirection = value; }

    public override void _Ready()
	{
		animatedSprite_weaponSlash = GetNode<AnimatedSprite2D>("WeaponSlash");
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (Speed <= SpeedEnum.Dash && Speed >= SpeedEnum.Default)
		{
			Speed -= Speed_DashDecay;
		}

		Vector2 direction = Input.GetVector("plr_left", "plr_right", "plr_up", "plr_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
			LastDirection = direction;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		if (Input.IsActionJustReleased("plr_attack"))
		{
			//set position
			var tempPos = animatedSprite_weaponSlash.Position;
            tempPos.X = LastDirection.X * animatedSprite_weaponSlash_Offset;
			tempPos.Y = LastDirection.Y * animatedSprite_weaponSlash_Offset;
			animatedSprite_weaponSlash.Position = tempPos;
			
			//set rotation;
			switch (LastDirection)
			{
				case Godot.Vector2(0,0):
					animatedSprite_weaponSlash.RotationDegrees = 0;
					break;
				case Godot.Vector2(0,1):
					animatedSprite_weaponSlash.RotationDegrees = 90;
					break;
				case Godot.Vector2(0,-1):
					animatedSprite_weaponSlash.RotationDegrees = -90;
					break;
				case Godot.Vector2(1,1):
					animatedSprite_weaponSlash.RotationDegrees = 45;
					break;
				case Godot.Vector2(1,-1):
					animatedSprite_weaponSlash.RotationDegrees = -15;
					break;
				case Godot.Vector2(1,0):
					animatedSprite_weaponSlash.RotationDegrees = 0;
					break;
				case Godot.Vector2(-1,0):
					animatedSprite_weaponSlash.RotationDegrees = 180;
					break;
				case Godot.Vector2(-1,1):
					animatedSprite_weaponSlash.RotationDegrees = 135;
					break;
				case Godot.Vector2(-1,-1):
					animatedSprite_weaponSlash.RotationDegrees = -135;
					break;
			}
            
			animatedSprite_weaponSlash.Play("slashC");
			Speed = SpeedEnum.Dash;
		}

		if (Input.IsActionPressed("plr_attack"))
		{
			if (Speed > 0)
			{
				Speed -= Speed_ChargeDecay;
			}
			else if (Speed <= 0)
			{
				Speed = 0;
			}

		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_weapon_slash_animation_finished()
	{
		//Console.WriteLine("Slashed!");
		animatedSprite_weaponSlash.Animation = "idle";
	}
}
