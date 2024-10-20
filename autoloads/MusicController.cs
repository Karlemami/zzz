using Godot;
using System;

public partial class MusicController : AudioStreamPlayer2D
{
	public override void _Ready()
	{
		this.Play();
		Finished += OnFinished;
	}

	private void OnFinished()
	{
		this.Play();
	}

}
