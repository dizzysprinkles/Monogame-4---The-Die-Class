using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_4___The_Die_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Die die1;

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

            die1 = new Die(dieTextures, new Rectangle(10, 10, 75, 75));
            sumRolls = 0;
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

            if (currentKeyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space))
            {
                die1.RollDie();
                sumRolls = die1.Roll;
                foreach (Die die in dice)
                {
                    die.RollDie();
                    sumRolls = die.Roll + sumRolls;

                }
            }
            if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < dice.Count; i++)
                {
                    if (dice[i].Location.Contains(currentMouseState.Position))
                    {
                        dice.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            prevKeyboardState = currentKeyboardState;
            prevMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            die1.DrawDie(_spriteBatch);

            foreach (Die die in dice)
            {
                die.DrawDie(_spriteBatch);
            }
            _spriteBatch.DrawString(sumFont, $"Total Sum: {sumRolls}", new Vector2(300, 10), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
