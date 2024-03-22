using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;

        private readonly string helpText = "Do You Need Help?\n\n" +
            "1. Press Start to start the game\n" +
            "2. Press About to see the about page\n" +
            "3. Press Help to see the help page\n" +
            "4. Press Leaderboard to see the leaderboard\n" +
            "5. Press Credit to see the credit\n" +
            "6. Press Quit to quit the game\n" +
            "7. Press Esc to pause the game\n" +
            "8. Press Esc again to resume the game\n" +
            "9. Press Esc to go back to the main menu\n" +
            "10. Press Esc again to quit the game\n" +
            "11. Press Space to shoot the bubble\n";

        private Rectangle rectangle;

        public HelpScene(Game game) : base(game)
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
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("File"), "Help", new Vector2(50, 50), Color.White);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("RegularFont"), helpText, new Vector2(50, 100), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
