using Godot;

public partial class Component : Node2D
{
    public Character Parent;

    public override void _Ready()
	{
		Parent = GetParent<Character>();
	}
}