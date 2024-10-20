using Godot;
using System;

public partial class Ruby : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEnteredSignal;
	}

	private void OnBodyEnteredSignal(Node body)
	{
		if(body.Name == "Player")
		{
			LevelManager levelManager = (LevelManager)GetNode("%LevelManager");
			levelManager.RubyCollected();
			QueueFree();
		}
	}
}
