using Godot;

public partial class MoveComponent : Node
{
	private Character _character;

	[Export] public float BaseSpeed = 150;
	[Export] public float DashForce = 450;
	[Export] public float DashDecay = 750; // higher -> stops faster
	[Export] public float Acceleration = 2000f;
	[Export] public float Friction = 2000f;

	private Vector2 _dashVelocity = Vector2.Zero;

	private bool _isCharging = false;

	public Vector2 DesiredDirection { get; set; } = Vector2.Zero;

	public override void _Ready()
	{
		_character = GetParent<Character>();
		//BaseSpeed = _character.BaseSpeed;
	}

	public override void _PhysicsProcess(double delta)
	{
		float d = (float)delta;
		Vector2 velocity = _character.Velocity;

		//If charging -> slow to full stop
		if (_isCharging)
		{
			velocity = velocity.MoveToward(
				Vector2.Zero,
				Friction * d
			);
		}
		else
		{
			// Normal movement
			if (DesiredDirection != Vector2.Zero)
			{
				Vector2 target = DesiredDirection * BaseSpeed;
				velocity = velocity.MoveToward(
					target,
					Acceleration * d
				);
			}
			else
			{
				velocity = velocity.MoveToward(
					Vector2.Zero,
					Friction * d
				);
			}
		}

		//Dash overrides everything
		if (_dashVelocity.Length() > 0)
		{
			velocity += _dashVelocity;

			_dashVelocity = _dashVelocity.MoveToward(
				Vector2.Zero,
				DashDecay * d
			);
		}
		Vector2 combined = velocity + _dashVelocity;
		if (combined.Length() > BaseSpeed + _dashVelocity.Length())
			combined = combined.Normalized() * (BaseSpeed + _dashVelocity.Length());
		velocity = combined;
		_character.Velocity = velocity;
		_character.MoveAndSlide();
	}

	public void Dash(Vector2 direction)
	{
		if (direction == Vector2.Zero)
			return;

		_dashVelocity = direction.Normalized() * DashForce;
	}

	public void StartCharge()
	{
		_isCharging = true;
	}

	public void StopCharge()
	{
		_isCharging = false;
	}
}