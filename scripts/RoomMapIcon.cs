using Godot;
using System;

public partial class RoomMapIcon : Node2D
{
	public Color NormalColor = Colors.Blue;
	public Color EnterColor = Colors.Green;
	

	public override void _Ready()
	{
	}
	public void SetMapIconColor(Color c)
	{
		Modulate = c;
	}
	public void showBridge(bool rightBridge)
	{
		//TRUE -> RIGHT bridge
		//FALSE -> BOTTOM bridge
		if (rightBridge)
		{
			GetNode<Sprite2D>("BridgeRightSprite").Visible = true;
		} else
		{
			GetNode<Sprite2D>("BridgeBottomSprite").Visible = true;
		}
	}
}
