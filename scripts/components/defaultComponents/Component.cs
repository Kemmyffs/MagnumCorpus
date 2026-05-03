using Godot;

public partial class Component : Node2D
{
	public Character Parent;
	public GlobalScript.Team Team;

	public override void _Ready()
	{
		Parent = GetParent<Character>();
		Team = Parent.Team;
	}
}