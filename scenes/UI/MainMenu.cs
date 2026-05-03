using Godot;
using System;

public partial class MainMenu : Control
{
	//Connectnout v Godotu a potom udělat public void funkci se stejným jménem
	public void _on_button_button_up()
	{
		GetTree().ChangeSceneToFile("res://scenes/floor_manager.tscn");
	}

	public void OnStartTutorialButtonUp()
	{
		GetTree().ChangeSceneToFile("res://scenes/TutorialLevel.tscn");
	}
}
