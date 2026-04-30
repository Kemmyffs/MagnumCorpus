using System;
using Godot;

[Icon("res://customResources//iconPack/32x32/window_frame_show.png")]
public partial class Hud : Control
{

	public TileMapLayer TileMapLayer;
	private Label EnemiesLeftLabel;
	private RoomMapIcon[,] roomMapIconGrid;

	private Character PlayerParent;
	public TextureProgressBar HealthBar;
	public TextureProgressBar SpecialBar;
	public EnemyCounterProgress EnemiesLeftProgressbar;
	public DialogueManager dialogueManager;

	public override void _Ready()
	{
		TileMapLayer = GetNode<TileMapLayer>("MapCenterContainer//TileMapLayer");
		EnemiesLeftLabel = GetNode<Label>("TextureRect//CenterContainer//EnemiesLeftLabel");
		PlayerParent = GetParent<CanvasLayer>().GetParent<Character>();
		HealthBar = GetNode<TextureProgressBar>("TextureRect//HealthBar"); //TODO
		SpecialBar = GetNode<TextureProgressBar>("TextureRect//SpecialBar"); //TODO
		EnemiesLeftProgressbar = GetNode<EnemyCounterProgress>("TextureRect//ColorRect");

		if (GetTree().CurrentScene.Name == "TutorialMap")
		{
			GetNode<DialogueManager>("DialogManager").Visible = true;
			dialogueManager = GetNode<DialogueManager>("DialogManager");
			dialogueManager.init();
			dialogueManager.DisplayDialogue();
		}
		else
		{
			GetNode<DialogueManager>("DialogManager").Visible = false;
			//GetNode<DialogueManager>("DialogManager").QueueFree();
		}
	}

	public void UpdateEnemyCounter()
	{
		int totalCount = GlobalScript.FloorTotalEnemyCount;
		int currentCount = GlobalScript.FloorCurrentEnemyCount;
		int enemiesKilled = totalCount - currentCount;

		EnemiesLeftLabel.Text = $"{enemiesKilled}/{totalCount}";

		float fillPercentage = totalCount > 0 ? (float)enemiesKilled / totalCount : 0.0f;
		EnemiesLeftProgressbar.GraduallyIncrementEnemyProgressBar(fillPercentage);
	}

    public void GenerateMinimap(int x, int y)
	{
		Console.WriteLine("Generating Minimap From HUD");
		//roomMapIconGrid = PlayerParent.GetParent()("DungeonGenerator").RoomGrid_Icons;
		roomMapIconGrid = PlayerParent.GetParent<DungeonGenerator>().RoomGrid_Icons;
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				if (roomMapIconGrid[i, j] != null)
				{
					TileMapLayer.SetCell(new Vector2I(i, j), 0, new Vector2I(0, 0), 0);
				}
			}
		}
	}

	bool[,] Reconstruct(bool[] grid, int x, int y, int height)
	{
		int width = x;
		int h = height;

		var reconstructed = new bool[width, h];

		for (int ix = 0; ix < width; ix++)
		{
			for (int iy = 0; iy < h; iy++)
			{
				reconstructed[ix, iy] = grid[ix * h + iy];
			}
		}

		return reconstructed;
	}

	public void OnEnemyDeath()
	{
		GlobalScript.FloorCurrentEnemyCount--;
		UpdateEnemyCounter();
		if (GlobalScript.FloorCurrentEnemyCount == 0)
		{

		}
	}

	public void UpdateFloorLabel()
	{
		GetNode<RichTextLabel>("LevelNumberLabelContainer/LevelNumberLabel").Text = GlobalScript.CurrentFloorLevel.ToString();
	}

}
