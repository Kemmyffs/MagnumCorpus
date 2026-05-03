using System;
using Godot;

public partial class Enemy : Character
{

	private Player playerNode;
	[Export] public int ChaseRadius;
	[Signal] public delegate void JustDiedEventHandler();
	private DirectionProvidingComponent DirectionProvider;
	private ShaderMaterial sm;
	private bool _isFlashing = false;
	private float flashTime = 0.1f;

	public override void _Ready()
	{
		base._Ready();
		playerNode = GetParent().GetParent().GetNode<Player>("%Player");
		JustDied += GetParent<EnemyRoot>().GetParent<DungeonGenerator>().OnEnemyDeath;

		if (HasNode("ChaseComponent"))
		{
			var shape = (CircleShape2D)GetNode<CollisionShape2D>("ChaseComponent/Area2D/CollisionShape2D").Shape;
			shape.Radius = ChaseRadius;
			DirectionProvider = GetNode<DirectionProvidingComponent>("ChaseComponent");
		}

		//set different node responsible for movement

		SetShaderMaterial();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (DirectionProvider != null)
		{
			Vector2 direction = DirectionProvider.ProvideDirection();
			_moveComponent.DesiredDirection = direction;
		}
	}

	private void SetShaderMaterial()
	{
		sm = new ShaderMaterial();

		Shader shader = GD.Load<Shader>("res://shaders/EnemyShader.gdshader");
		sm.Shader = shader;
		sm.SetShaderParameter("flash_color", Colors.Red);

		GetNode<Sprite2D>("Sprite2D").Material = sm;
	}

	public override void Die()
	{
		EmitSignal("JustDied");
		base.Die();
	}

	internal async void DamageFlash()
	{
		if (_isFlashing) return;

		_isFlashing = true;
		sm.SetShaderParameter("active", true);
		await ToSignal(GetTree().CreateTimer(flashTime), SceneTreeTimer.SignalName.Timeout);
		sm.SetShaderParameter("active", false);
		_isFlashing = false;
	}

}
