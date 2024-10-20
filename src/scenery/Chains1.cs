using Godot;
using System;

public partial class Chains1 : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEnteredChains;
        BodyExited += OnBodyExitedChains;
    }

    private void OnBodyEnteredChains(Node body)
    {
        Player player = (Player)InstanceFromId(body.GetInstanceId());
        player.IsOnClimbableSurface = true;
    }
    private void OnBodyExitedChains(Node body)
    {
        Player player = (Player)InstanceFromId(body.GetInstanceId());
        player.IsOnClimbableSurface = false;
        player.IsClimbing = false;
    }
}
