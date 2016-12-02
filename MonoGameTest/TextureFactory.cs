using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
    public static class TextureFactory
    {
        public static Texture2D Rectangle(GraphicsDevice graphics, int width, int height, Color color, bool filled)
        {
            var t = new Texture2D(graphics, width, height);

            var data = new Color[width * height];
            if (filled)
            {
                for (var i = 0; i < data.Length; i++)
                {
                    data[i] = color;
                }
            }
            else
            {
                //Top and Bottom.
                for (var i = 0; i < width; i++)
                {
                    data[i] = color;
                    data[width*(height - 1) + i] = color;
                }
                //Left and Right.
                for (var i = 0; i < height; i++)
                {
                    data[i*width] = color;
                    data[i*width + width - 1] = color;
                }
            }
            t.SetData(data);
            return t;
        }
    }
}