using Godot;
using System;

public partial class Player : CharacterBody2D
{
	internal const float Speed = 120.0f;
	internal const float JumpVelocity = -300.0f;
	internal int Hp = 3;
	private AnimatedSprite2D sprite;
	private Timer InvincibilityFrames;
	private HealthBar HealthBar;
	internal bool IsClimbing = false;
	internal bool IsOnClimbableSurface = false;
	internal bool IsInvincible = false;
	internal bool IsTakingDamage = false;
	internal bool IsAlive = true;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		InvincibilityFrames = GetNode<Timer>("InvicibilityFrames");
		InvincibilityFrames.OneShot = true;
		InvincibilityFrames.WaitTime = 0.8;	
		InvincibilityFrames.Timeout += OnInvincibilityFramesTimerTimeoutSignal;
		HealthBar = GetNode<HealthBar>("HealthBar");
		GD.Print(HealthBar);

	}

    public override void _PhysicsProcess(double delta)
	{
		if(IsAlive)
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

			Velocity = velocity;
			
			MoveAndSlide();
		}
	}

	private void OnInvincibilityFramesTimerTimeoutSignal()
	{
		IsInvincible = false;

		GD.Print("timer ends");
	}

	internal void TakeDamage(int amount)
	{
		GD.Print("taking damage");
		if(!IsInvincible)
		{
			if(Hp - amount <= 0)
			{
				Hp = 0;
				HealthBar.UpdateHealthbar(Hp);
				Die();
			}

			Hp -= amount;
			GD.Print(Hp);
			HealthBar.UpdateHealthbar(Hp);
			IsInvincible = true;
			InvincibilityFrames.Start();

		}
	}

	internal void OnDeathAnimationTimerTimeout()
	{
		LevelManager levelManager = (LevelManager)GetNode("%LevelManager");
		levelManager.CallDeferred(nameof(levelManager.ReloadScene));
	}
	internal void Die()
	{
		IsAlive = false;
		sprite.Play("Die");
		sprite.AnimationFinished += OnDeathAnimationTimerTimeout;
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
		if(IsInvincible)
		{
			sprite.Play("TakeDamage");
			return;
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
