using Flavors;
using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalScript : Resource
{

	public Dictionary<string, Flavor> AllFlavors = new Dictionary<string, Flavors.Flavor>()
	{
		{ "Strawberry", new Flavor("Tastes red", new Color(1.163f, 0.212f, 0.32f)) },
		{ "Pistacio", new Flavor("Nobody likes it. And if you do, grow up.", new Color(0.0f, 1.419f, 0.783f))}
	};
	//
	


}
