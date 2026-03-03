using Godot;

public partial class Player : Character
{
	private MoveComponent _move;

	public Vector2 LastDirection { get; private set; } = Vector2.Right;

	public override void _Ready()
	{
		_move = GetNode<MoveComponent>("MoveComponent");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector(
			"plr_left", "plr_right",
			"plr_up", "plr_down"
		);

		_move.DesiredDirection = direction;

		if (direction != Vector2.Zero)
			LastDirection = direction;

		if (Input.IsActionPressed("plr_attack"))
			_move.StartCharge();

		if (Input.IsActionJustReleased("plr_attack"))
		{
			_move.StopCharge();
			_move.Dash(LastDirection);
		}
	}
}