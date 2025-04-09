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

        public Die(List<Texture2D> faces, Rectangle location)
        {
            _generator = new Random();
            _roll = _generator.Next(1, 7);
            _faces = faces;
            _location = location;
            _frame = 0;
        }


        public int Roll
        {
            get { return _roll; }
            //set 
            //{ 
            //    _roll = value; 
            //}
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
                if (_frame % 10 == 0) //every 10 frames display a random face
                {
                    _randomFace = _generator.Next(_faces.Count());
                    if (_frame == 60) // after 60s, stop animating a roll
                    {
                        _frame = 0; // animation is done!
                    }
                }
                spriteBatch.Draw(_faces[_randomFace], _location, Color.White);
            }
            else
            {
                spriteBatch.Draw(_faces[_roll - 1], _location, Color.White); 
            }
        }

        public override string ToString()
        {
            return _roll.ToString();
        }

    }


}
