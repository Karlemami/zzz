using Godot;
using System;

public partial class LevelManager : Node
{
	public int score;

	internal void CoinCollected()
	{
		score++;
		GD.Print("+1 score");
		AudioStreamPlayer streamPlayer = (AudioStreamPlayer)GetNode("CoinSound");
		streamPlayer.Play();
		GD.Print(streamPlayer.Playing);
	}
	internal void RubyCollected()
	{
		score+=10;
		GD.Print("+10 score");
		AudioStreamPlayer streamPlayer = (AudioStreamPlayer)GetNode("RubySound");
		streamPlayer.Play();
		GD.Print(streamPlayer.Playing);
	}
		
		internal void ReloadScene()
	{
		GetTree().ReloadCurrentScene();
	}
}
