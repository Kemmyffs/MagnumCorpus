using Godot;
using System;

//0,0 až 23,23
//TODO neni dobrej napad si pocet tilů roomky (bez zdí) automaticky vypocitavat? Mozna?
//doors -1, 10-13


public partial class RoomPrefab : Node2D
{

	public TileMapLayer TileMapBase;
	public TileMapLayer TileMapObjects;
	public int FloorSize = 24;


	public override void _Ready()
	{
		TileMapBase = GetNode<TileMapLayer>("TileMapLayer_Base");
	}

	public void RandomizeFloor()
	{
		Random rnd = new Random();
		for (int x = 0; x < FloorSize; x++)
		{
			for (int y = 0; y < FloorSize; y++)
			{
				TileMapBase.SetCell(new Vector2I(x, y), 0, new Vector2I(3, rnd.Next(0, 3)), 0);
				//TileMapBase.SetCell(new Vector2I(x,y), 0, new Vector2I(3, 3), 0);
			}
		}
	}

	public void GenerateRoomObjects()
	{
		//TODO
	}

	public void GenerateEnemies()
	{
		//TODO
	}


	
	public void SetDoor(Vector2 side, bool open)
	{
		int startX = 0, startY = 0, doorWidth = 0, doorHeight = 0;
		if (side == Vector2.Up)
		{
			startY = -1;
			startX = 10;
			doorWidth = 4;
			doorHeight = 1;
		}
		else if (side == Vector2.Left)
		{
			startY = 10;
			startX = -1;
			doorWidth = 1;
			doorHeight = 4;
		} else if(side == Vector2.Down)
		{
			startY = 24;
			startX = 10;
			doorWidth = 4;
			doorHeight = 1;
		} else if(side == Vector2.Right)
		{
			startY = 10;
			startX = 24;
			doorWidth = 1;
			doorHeight = 4;
		}

		Vector2I tile;

		/* 
			if (open)
			{
				tile = new Vector2I(3, 0);
			}
			else
			{
				tile = new Vector2I(0, 0);
			}
		*/

		tile = open ? new Vector2I(3,0) : new Vector2I(0,0);

		for (int x = 0; x < doorWidth; x++)
		{
			for (int y = 0; y < doorHeight; y++)
			{
				TileMapBase.SetCell(new Vector2I(startX + x, startY + y), 0, tile, 0);
			}
		}
	}

	public void GenerateBridges(bool rightBridge, bool bottomBridge)
	{
		//mosty jsou předem udělaný v každém roomPrefabu –> když přijde FALSE, SCHOVÁVÁM mosty a jen zavřu dveře
		const int startX = 25;
		const int lastX = 30;
		const int startY = 10;
		const int lastY = 14;
		if (!rightBridge)
		{
			//SCHOVAT pravy most a ZAVRIT DVERE
			EraseTileArea(startX, startY, lastX, lastY);
			SetDoor(Vector2.Right, true);
		}

		if (!bottomBridge)
		{
			//SCHOVAT levy most a ZAVRIT DVERE
			EraseTileArea(startY, startX, lastY, lastX);
			SetDoor(Vector2.Up, true);
		}
	}

	/// <summary>
	/// Erases tiles in a rectangular area. Parameters are the coordinates of the Topleft and bottomright tiles. 
	/// </summary>
	/// <param name="x0">Top-left tile's X coordinate</param>
	/// <param name="y0">Top-left tile's Y coordinate</param>
	/// <param name="x1">Bottom-right tile's X coordinate</param>
	/// <param name="y1">Bottom-right tile's Y coordinate</param>
	private void EraseTileArea(int x0, int y0, int x1, int y1)
	{
		for (int x = x0; x <= x1; x++)
			{
				for (int y = y0; y <= y1; y++)
				{
					TileMapBase.EraseCell(new Vector2I(x,y));
				}
			}
	}


}
