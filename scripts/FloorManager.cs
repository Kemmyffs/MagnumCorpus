using Godot;
using System;

public partial class FloorManager : Node2D
{
	public int Floor = 0;
	private DungeonGenerator dungeonGenerator;

	private Camera2D camera;
	public override void _Ready()
	{
		dungeonGenerator = GetNode<DungeonGenerator>("DungeonGenerator");
		camera = GetNode<Camera2D>("Camera2D");
	}

	public void FailFloor()
	{
		dungeonGenerator.SetDeferred(Node.PropertyName.ProcessMode, (int)ProcessModeEnum.Disabled);
		ReplacePlayerCamera();
		var material = camera.GetNode<TextureRect>("TextureRect").Material as ShaderMaterial;
		CreateTween().TweenProperty(material, "shader_parameter/iris_progress", 8.0f, 4.0f)
			 .SetTrans(Tween.TransitionType.Cubic)
			 .SetEase(Tween.EaseType.In);
	}

	public void FinishFloor()
	{

	}

	public void ReplacePlayerCamera()
	{
		Camera2D playerCamera = GetNode<Camera2D>("DungeonGenerator/Player/Camera2D");
		camera.GlobalPosition = playerCamera.GetTargetPosition();
		playerCamera.Enabled = false;
		camera.Enabled = true;
	}
}
