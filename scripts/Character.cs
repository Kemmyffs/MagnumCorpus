using Godot;

public partial class Character : CharacterBody2D
{
    [Export] public float BaseSpeed = 200f;
    [Export] public int MaxHealth = 100;

	public float Speed => BaseSpeed;

    public bool IsAlive { get; protected set; } = true;

	public Vector2 FacingDirection {get; protected set;}
}