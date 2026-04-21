using Flavors;
using Godot;
using System;
using System.Collections.Generic;


//[GlobalClass]
public static partial class GlobalScript : Object
{
	public enum EnemyTypes
	{
		SaltShaker
	};

	public static Dictionary<string, Flavor> AllFlavors = new Dictionary<string, Flavor>()
	{
		{ "Strawberry",
			new Flavor(
				"Tastes red",
				new Color(1.163f, 0.212f, 0.32f))
		},
		{ "Pistacio",
			new Flavor(
				"Nobody likes it. And if you do, grow up.",
				new Color(0.0f, 1.419f, 0.783f))
		},
		{ "Lemon",
			new Flavor(
				"Never eat yellow snow. This one's fine though\nFavoured by Čambára",
				new Color(1.0f, 1.0f, 0.918f))
		},
		{ "Evil Ass Rape Flavor",
			new Flavor(
				"Why would you even walk inside?",
				Colors.Red)
		}
	};

	public static int CurrentFloorLevel = 0;
	public static int FloorTotalEnemyCount { get; set; }
	public static int FloorCurrentEnemyCount { get; set; }
	public static bool PausedGame = false;

}
