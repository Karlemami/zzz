using Godot;
 
public partial class Coin : Area2D
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
			levelManager.CoinCollected();
			QueueFree();
		}
	}

}
