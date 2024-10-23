using Godot;
using System;

public partial class HealthBar : HBoxContainer
{
	Texture2D HeartFull = ResourceLoader.Load<Texture2D>("../assets/heart_full.png");
	Texture2D HeartEmpty = ResourceLoader.Load<Texture2D>("../assets/heart_empty.png");

	internal void UpdateHealthbar(int health)
	{
		for(int i = 0; i < GetChildCount(); i++)
		{
			TextureRect HeartBox = (TextureRect)GetChild(i);
			if(health>i)
			{
				HeartBox.Texture = HeartFull;
			}
			else
			{
				HeartBox.Texture = HeartEmpty;
			}
		}
	}

}
