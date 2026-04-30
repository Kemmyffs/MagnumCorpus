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
	private EnemyRoot enemyRoot;

	public override void _Ready()
	{
		TileMapLayer = GetNode<TileMapLayer>("MapCenterContainer//TileMapLayer");
		EnemiesLeftLabel = GetNode<Label>("TextureRect//CenterContainer//EnemiesLeftLabel");
		PlayerParent = GetParent<CanvasLayer>().GetParent<Character>();
		HealthBar = GetNode<TextureProgressBar>("TextureRect//HealthBar"); //TODO
		SpecialBar = GetNode<TextureProgressBar>("TextureRect//SpecialBar"); //TODO
		EnemiesLeftProgressbar = GetNode<EnemyCounterProgress>("TextureRect//ColorRect");

		enemyRoot = PlayerParent.GetParent<DungeonGenerator>().GetNode<EnemyRoot>("EnemyRoot");

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
		int totalCount = enemyRoot.TotalEnemyCount;
		int currentCount = enemyRoot.CurrentEnemyCount;
		int enemiesKilled = totalCount - currentCount;

		EnemiesLeftLabel.Text = $"{enemiesKilled}/{totalCount}";

		float fillPercentage = totalCount > 0 ? (float)enemiesKilled / totalCount : 0.0f;
		EnemiesLeftProgressbar.GraduallyIncrementEnemyProgressBar(fillPercentage);
	}

    public void GenerateMinimap(int x, int y)
	{
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

	public void UpdateFloorLabel()
	{
		GetNode<RichTextLabel>("LevelNumberLabelContainer/LevelNumberLabel").Text = GlobalScript.CurrentFloorLevel.ToString();
	}

}
