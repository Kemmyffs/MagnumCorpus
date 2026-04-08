using Godot;
using System;

public partial class HealthComponent : Node2D
{

	private Character _parent;
	private TextureProgressBar HealthBar;
	private TextureProgressBar SpecialBar;
	public int MaxHealth;
	public int CurrentHealth { get; set; }
	[Export] public int SpecialBarRechargeTime { get; set; }
	public double SpecialBarCurrentValue {get; set;}



	public override void _Ready()
	{
		_parent = GetParent<Character>();
		MaxHealth = _parent.MaxHealth;
		
		if (_parent.Name == "Player")
		{
			
			HealthBar = _parent.GetNode<Hud>("CanvasLayer//HUD").HealthBar;
			SpecialBar = _parent.GetNode<Hud>("CanvasLayer//HUD").SpecialBar;

			GetNode<TextureProgressBar>("HealthBar").Visible = false;
			GetNode<TextureProgressBar>("SpecialBar").Visible = false;
			GetNode<TextureRect>("Background").Visible = false;

		}
		else
		{
			HealthBar = GetNode<TextureProgressBar>("HealthBar");
			SpecialBar = GetNode<TextureProgressBar>("SpecialBar");
		}

		CurrentHealth = MaxHealth;
		HealthBar.MaxValue = MaxHealth;
		HealthBar.MinValue = 0;
		UpdateHealthBar();

	}

	public override void _Process(double delta)
	{
		SpecialBarCurrentValue += delta;
    	SpecialBarCurrentValue = Math.Min(SpecialBarCurrentValue, SpecialBarRechargeTime);
		//UpdateSpecialBar();
		//UpdateHealthBar();
	}

	public void Damage(int dmg)
	{
		CurrentHealth -= dmg;
	}

	public void Heal(int amount)
	{
		CurrentHealth = Math.Max(CurrentHealth + amount, MaxHealth);
	}
	public void UpdateHealthBar()
	{
		HealthBar.Value = CurrentHealth;
	}

	public void UpdateSpecialBar()
	{
		//SpecialBar.Value = SpecialBarCurrentValue;
	}
	
}
