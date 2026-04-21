using Godot;
using System;
[Icon("res://customResources//iconPack/32x32/location_character.png")]

public partial class ChaseComponent : DirectionProvidingComponent
{
	public Character Target;
	public override void _Ready()
	{
		base._Ready();
	}

	public void Area2DBodyEntered(Node2D body)
	{
		if (body.Name.Equals("Player"))
		{
			SetTarget((Character)body);
		}
	}

	public void Area2DBodyExited(Node2D body)
	{
		if (body.Name.Equals("Player"))
		{
			SetTarget(null);
		}
	}

	public void SetTarget(Character body)
	{
		Target = body;
	}

	public override Vector2 ProvideDirection()
	{
		return Target != null
			? (Target.GlobalPosition - this.GlobalPosition).Normalized()
			: Vector2.Zero;
	}

}
