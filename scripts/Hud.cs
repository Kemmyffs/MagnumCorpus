using Godot;
using System;

public partial class Hud : Control
{
	
	public Node2D MinmapRoot;

	public override void _Ready()
	{
		MinmapRoot = GetNode<Node2D>("MapCenterContainer//MapTextureRect");
	}

	public void GenerateMinimap(Node2D MapRoot)
	{
        Node2D newRoot = (Node2D) MapRoot.Duplicate();
        MinmapRoot.AddChild(newRoot);
	}
}
