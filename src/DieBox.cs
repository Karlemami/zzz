using Godot;
using System;

public partial class DieBox : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player player = (Player)GetParent();
		Pressed += () => player.Die();
	}

}
