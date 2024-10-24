using Godot;
using System;

public partial class Spikes : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEnteredSpikes;
	}

	private void OnBodyEnteredSpikes(Node body)
	{
		if(body.Name == "Player")
		{
			ulong id = body.GetInstanceId();
			Player player = (Player)InstanceFromId(id);
			GD.Print(player);
			player.TakeDamage(1);
			
		}
	}
}
