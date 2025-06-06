﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_4___The_Die_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState currentKeyboardState, prevKeyboardState;
        MouseState currentMouseState, prevMouseState;

        List<Texture2D> dieTextures;
        List<Die> dice;

        SpriteFont sumFont;
        int sumRolls;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            dieTextures = new List<Texture2D>();
            dice = new List<Die>();

            for (int i = 10; i <= 410; i += 80)
            {
                dice.Add(new Die(dieTextures, new Rectangle(100, 0 + i, 75, 75)));
            }

            for (int i = 0; i < dice.Count; i++)
            {
                sumRolls += dice[i].Roll;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 1; i <= 6; i++)
            {
                dieTextures.Add(Content.Load<Texture2D>("Images/white_die_" + i));
            }

            sumFont = Content.Load<SpriteFont>("Fonts/SumFont");

        }

        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Re-rolling dice
            if (currentKeyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space))
            {
                sumRolls = 0;
                foreach (Die die in dice)
                {
                    die.RollDie();
                    sumRolls = die.Roll + sumRolls;

                }
            }
            //LEFT Click check - Reroll if clicked
            if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                sumRolls = 0;
                for (int i = 0; i < dice.Count; i++)
                {
                    if (dice[i].Location.Contains(currentMouseState.Position))
                    {
                        dice[i].RollDie();
                        
                    }
                    sumRolls = dice[i].Roll + sumRolls;

                }

            }

            //RIGHT Click check - Remove if clicked
            if (currentMouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
            {
                for (int i = 0; i < dice.Count; i++)
                {
                    if (dice[i].Location.Contains(currentMouseState.Position))
                    {
                        sumRolls -= dice[i].Roll;
                        dice.RemoveAt(i);
                        i--;

                    }
                }

            }

            if (dice.Count == 0)
            {
                Exit();
            }

            prevKeyboardState = currentKeyboardState;
            prevMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (Die die in dice)
            {
                die.DrawDie(_spriteBatch);
            }
            _spriteBatch.DrawString(sumFont, "Press the SPACEBAR to roll all the dice!", new Vector2(200, 10), Color.Black);
            _spriteBatch.DrawString(sumFont, "Left click a die to reroll it!", new Vector2(200, 80), Color.Black);
            _spriteBatch.DrawString(sumFont, "Right click a die to remove it!", new Vector2(200, 160), Color.Black);
            _spriteBatch.DrawString(sumFont, "Out of dice? Program ends!", new Vector2(200, 240), Color.Black);

            _spriteBatch.DrawString(sumFont, $"Total Sum: {sumRolls}", new Vector2(300, 300), Color.Crimson);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
