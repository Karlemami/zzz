using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 120.0f;
	public const float JumpVelocity = -300.0f;
	private AnimatedSprite2D sprite;
	internal bool IsClimbing = false;
	internal bool IsOnClimbableSurface = false;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		
		if (!IsOnFloor())
		{

			if(IsClimbing == false)
			{
				velocity += GetGravity() * (float)delta;
				if (IsOnClimbableSurface == true)
				{
					if(Input.IsActionJustPressed("move_up"))
					{
						velocity.Y = 0;
						IsClimbing = true;
					}
				}
			}
			else
			{
				velocity = Climb(velocity);
			}	
		}
		

		Animate(direction);
		velocity = Jump(velocity);
		velocity = MoveHorizontal(velocity, direction);

		GD.Print("velo: ",velocity);
		Velocity = velocity;
		MoveAndSlide();
	}


	internal void Die()
	{
		// Use call_deferred to reload the scene after the physics processing is complete
		LevelManager levelManager = (LevelManager)GetNode("%LevelManager");
		levelManager.CallDeferred(nameof(levelManager.ReloadScene));
	}

	private void Animate(Vector2 direction)
	{
		if(direction.X > 0)
		{
			sprite.FlipH = false;
		}
		else if(direction.X < 0)
		{
			sprite.FlipH = true;
		}

		if (IsOnFloor())
		{
			if(direction != Vector2.Zero)
			{
				sprite.Play("run");
			}
			else
			{
				sprite.Play("idle");
			}
		}
		else if(!IsOnFloor() && !IsClimbing)
		{
			sprite.Play("jump");
		}
	}

	private Vector2 Climb(Vector2 velocity)
	{	
		if(Input.IsActionPressed("move_up"))
		{
			velocity.Y = -Speed;
		}
		else if (Input.IsActionPressed("move_down"))
		{
			velocity.Y = Speed;
		}
		else
		{
			velocity.Y = 0;
		}
		GD.Print("climbing vector : ", velocity);
		return velocity;
	}

	private Vector2 MoveHorizontal(Vector2 velocity, Vector2 direction)
	{
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		return velocity;
	}

	private Vector2 Jump(Vector2 velocity)
	{
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		return velocity;
	}
}
