using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_4___The_Die_Class
{
    public class Die
    {
        private int _roll, _randomFace, _frame;
        private Random _generator;
        private List<Texture2D> _faces;
        private Rectangle _location;  //Might get ambigious error if importing System.Draw and the Microsoft Framework...
        private float _rotation;

        public Die(List<Texture2D> faces, Rectangle location)
        {
            _generator = new Random();
            _roll = _generator.Next(1, 7);
            _faces = faces;
            _location = location;
            _frame = 0;
            _rotation = 0f;
        }


        public int Roll
        {
            get { return _roll; }
            //set 
            //{ 
            //    _roll = value; 
            //}
        }

        public override string ToString() 
        {
            return _roll + "";
        }

        public Rectangle Location
        {
            get { return _location; }
        }

        public int RollDie()
        {
            _roll = _generator.Next(1, 7);
            _frame = 1;
            return _roll;
        }

        public void DrawDie(SpriteBatch spriteBatch)
        {
            
            if (_frame > 0) // sec > 0, animating roll
            {
               _frame++;
                _rotation += 0.1f;
                if (_frame % 10 == 0) //every 10 frames display a random face
                {
                    _randomFace = _generator.Next(_faces.Count());
                    if (_frame == 60) // after 60s, stop animating a roll
                    {
                        _frame = 0; // animation is done!
                        _rotation = 0f;
                    }
                }
                spriteBatch.Draw(_faces[_randomFace], new Rectangle(_location.X + _location.Width / 2, _location.Y + _location.Height / 2, _location.Width, _location.Height), null, Color.White, _rotation, new Vector2(_faces[0].Width / 2, _faces[0].Height / 2), SpriteEffects.None, 0f);
                //Vector2 = Center of rectangle (so texture width and height / 2); Rectangle = offset to fix rotation (which is basically center of rectangle coordinate) - w/out, it rotates off the left
                
            }
            else
            {
                spriteBatch.Draw(_faces[_roll - 1], _location, Color.White); 
            }
        }

    
    }


}
