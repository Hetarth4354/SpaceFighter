using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;

        private readonly string aboutText = "This game is made by:\n\n" +
            "Hetarth Anilkumar Patel - 8884500\n" +
            "Pandya Deep - 8866572\n" +
            "Ansh Saileshbhai Chakrani - 8866756\n" +
            "Pranay Arvindkumar Khokhar - 8870262\n\n" +
            "This game is made for Computer Programming - Game Programming\n";

        private Rectangle rectangle;

        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            rectangle = new Rectangle(0, 0, g.GraphicsDevice.Viewport.Width, g.GraphicsDevice.Viewport.Height);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Game.Content.Load<Texture2D>("Background"), rectangle, Color.White);


            // Add title using TitleFont
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("File"), "About", new Vector2(50, 50), Color.White);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("RegularFont"), aboutText, new Vector2(50, 100), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
