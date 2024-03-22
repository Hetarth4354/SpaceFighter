using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceFighter
{
    public class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Rectangle sourceRectangle;


        string buttonText;
        SpriteFont buttonFont;

        public Button(Texture2D newTexture, Rectangle sourceRect, Vector2 newPosition, string text, SpriteFont font)
        {
            texture = newTexture;
            // The sourceRectangle should cover the full texture if you want to draw the whole texture
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            position = newPosition;
            // Make sure the rectangle is the size of the source rectangle for the texture
            rectangle = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
            buttonText = text;
            buttonFont = font;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D? selectedTexture = null)
        {

            if (selectedTexture != null)
            {
                sourceRectangle = new Rectangle(0, 0, selectedTexture.Width, selectedTexture.Height);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            }

            // Draw the texture to fill the rectangle
            spriteBatch.Draw(selectedTexture ?? texture, rectangle, sourceRectangle, Color.White);

            // Calculate the center position for the text
            Vector2 textSize = buttonFont.MeasureString(buttonText);
            Vector2 textPosition = new Vector2(rectangle.Center.X - textSize.X / 2, rectangle.Center.Y - textSize.Y / 2);

            // Draw the text
            spriteBatch.DrawString(buttonFont, buttonText, textPosition, Color.Black);
        }

        public bool isClicked()
        {
            MouseState mouseState = Mouse.GetState();

            // Check if the mouse is over the button
            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                // Check if the left mouse button is pressed
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }

            return false;
        }

    }

}
