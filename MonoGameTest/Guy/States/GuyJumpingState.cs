﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Guy.States
{
    public class GuyJumpingState : IGuyState
    {
        public string Name => "Jumping";
        private readonly EasySprite _sprite;
        private readonly EasyTimer _entryTimeout = new EasyTimer(TimeSpan.FromSeconds(0.25));
        public GuyJumpingState(EasySprite sprite)
        {
            _sprite = sprite;
        }

        public void Enter(Guy guy, GameTime gameTime)
        {
            guy.Physics.SetYVelocity(-22);
            _entryTimeout.Start(gameTime);
        }

        public void Exit(Guy guy) { }

        public void Draw(Guy guy, SpriteBatch spriteBatch, GameTime gameTime, SpriteEffects spriteEffects)
        {
            _sprite.Draw(spriteBatch, guy.Physics.Position, spriteEffects);
        }

        public IGuyState Update(Guy guy, KeyboardState keyboardState, GameTime gameTime)
        {
            var maybeDirection = guy.Physics.HorizontalMovementDirection;
            if (maybeDirection.HasValue)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    guy.Facing = XDirection.Right;
                } 
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    guy.Facing = XDirection.Left;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                return guy.States.Cannonball;
            }
            if (keyboardState.IsKeyDown(Keys.Up) && _entryTimeout.IsFinished(gameTime))
            {
                return guy.States.Hooray;
            }
            if (!guy.Physics.IsOnGround)
            {
                return null;
            }
            if (guy.Physics.IsMovingHorizontally)
            {
                return guy.States.Running;
            }
            return guy.States.Idle;
        }
    }
}