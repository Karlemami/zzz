using Godot;
using System;

public partial class WarpZone : Area2D
{
    [Export] String ScenePath;
    public override void _Ready()
    {
        BodyEntered += OnBodyEnteredSignal;
    }

    private void OnBodyEnteredSignal(Node body)
    {
        if (body is Player)
        {
            PackedScene Scene = (PackedScene)ResourceLoader.Load(ScenePath);
            Scene.Instantiate();
            GetTree().ChangeSceneToPacked(Scene);
        }
    }
}

