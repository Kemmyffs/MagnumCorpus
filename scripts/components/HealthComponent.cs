using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

[Icon("res://customResources//iconPack/32x32/heart.png")]

public partial class HealthComponent : Component
{
	private TextureProgressBar HealthBar;
	private TextureProgressBar SpecialBar;
	private Area2D Hurtbox;
	private CollisionShape2D HurtboxShape;
	[Export] public int MaxHealth;
	[Export] public float KnockbackBaseStrenght = 200;
	public int CurrentHealth { get; set; }
	[Export] public float SpecialBarRechargeTime = 10.0f;
	public double SpecialBarMaxValue = 100;


	public override void _Ready()
	{
		base._Ready();
		Hurtbox = GetNode<Area2D>("Hurtbox");
		HurtboxShape = Hurtbox.GetNode<CollisionShape2D>("CollisionShape2D");

		Hurtbox.AreaEntered += OnAreaEntered;

		if (Parent.Name == "Player")
		{
			HealthBar = Parent.GetNode<TextureProgressBar>("CanvasLayer//HUD//TextureRect//HealthBar");
			SpecialBar = Parent.GetNode<TextureProgressBar>("CanvasLayer//HUD//TextureRect//SpecialBar");
			GetNode<TextureProgressBar>("HealthBar").Visible = false;
			GetNode<TextureProgressBar>("SpecialBar").Visible = false;
			GetNode<TextureRect>("Background").Visible = false;
		}
		else
		{
			HealthBar = GetNode<TextureProgressBar>("HealthBar");
			SpecialBar = GetNode<TextureProgressBar>("SpecialBar");

			//ReshapeHurtboxAsync();
		}

		HealthBar.MaxValue = MaxHealth;
		HealthBar.MinValue = 0;
		CurrentHealth = MaxHealth;
		HealthBar.Value = CurrentHealth;

		SpecialBar.MaxValue = SpecialBarMaxValue;
		SpecialBar.MinValue = 0;
		SpecialBar.Step = 0.00001;
		SpecialBar.Value = SpecialBarMaxValue;
	}

	public override void _Process(double delta)
	{
		if (SpecialBar.Value < SpecialBarMaxValue)
		{
			double amountToAdd = (SpecialBarMaxValue / SpecialBarRechargeTime) * delta;

			SpecialBar.Value = Mathf.Clamp(SpecialBar.Value + amountToAdd, 0, SpecialBarMaxValue);
		}
	}


	public void Damage(int dmg)
	{
		CurrentHealth -= dmg;
		if (CurrentHealth <= 0) Parent.Die();
		HealthBar.Value = CurrentHealth;

		//Player doesn't flash
		if (Parent.GetType() != typeof(Player))
		{
			Enemy tmp = (Enemy)Parent;
			tmp.DamageFlash();
		}
	}

	public void Heal(int amount)
	{
		CurrentHealth = Math.Max(CurrentHealth + amount, MaxHealth);
		HealthBar.Value = CurrentHealth;
	}
	public void AddSpecialBarValue(double delta)
	{
		SpecialBar.Value = Math.Max(SpecialBar.Value + 1 / delta, SpecialBarMaxValue);
	}
	public void SubtractSpecialBarValue(double value)
	{
		double realValue = SpecialBar.MaxValue / value;
		SpecialBar.Value = Math.Max(SpecialBar.Value - realValue, 0);
	}

	private void OnAreaEntered(Area2D area)
	{

		if (area.Name == "Hitbox")
		{
			if (area.GetParent().GetParent<Character>() == this.GetParent<Character>()) return;
			if (area.GetParent().GetParent<Character>().Team == Team) return;
			Character attacker = area.GetParent().GetParent<Character>();
			Damage(attacker._attackComponent.Damage);
			KnockBack(area);
		}
	}

	private void KnockBack(Area2D area)
	{
		Character attacker = area.GetParent().GetParent<Character>();
		Vector2 knockBackDirection = (Parent.GlobalPosition - attacker.GlobalPosition).Normalized();
		Parent._moveComponent.Dash(knockBackDirection, (attacker._attackComponent.Damage) + KnockbackBaseStrenght);

	}



	private async Task ReshapeHurtboxAsync()
	{
		if (Team == GlobalScript.Team.Enemy)
		{
			await ToSignal(Parent, Node.SignalName.Ready);
			Hurtbox.GetNode<CollisionShape2D>("CollisionShape2D").Shape = ShapeManipulator.CopyExpandedShape(Parent.MoveCollisionShape.Shape);
		}
	}

	internal bool hasEnoughSpecial(float singleActionCost)
	{
		return SpecialBar.Value > SpecialBar.MaxValue / singleActionCost;
	}

	public void ToggleHurtbox(bool enabled)
	{
		HurtboxShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, !enabled);
	}

}
