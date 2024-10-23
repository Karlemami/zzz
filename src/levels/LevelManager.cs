using Godot;
using System;

public partial class LevelManager : Node
{
	public int score;

	internal void CoinCollected()
	{
		score++;
		AudioStreamPlayer streamPlayer = (AudioStreamPlayer)GetNode("CoinSound");
		streamPlayer.Play();
	}
	internal void RubyCollected()
	{
		score+=10;
		AudioStreamPlayer streamPlayer = (AudioStreamPlayer)GetNode("RubySound");
		streamPlayer.Play();
	}
		
		internal void ReloadScene()
	{
		GetTree().ReloadCurrentScene();
	}
}
