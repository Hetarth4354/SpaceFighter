using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceFighter
{
    public class LeaderboardScene : GameScene
    {
        private SpriteBatch spriteBatch;

        private Rectangle rectangle;

        List<LeaderboardEntry> Leaderboard;

        public LeaderboardScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            Leaderboard = LeaderboardManager.Instance.LoadLeaderboard();

            rectangle = new Rectangle(0, 0, g.GraphicsDevice.Viewport.Width, g.GraphicsDevice.Viewport.Height);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Game.Content.Load<Texture2D>("Background"), rectangle, Color.White);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("File"), "Leaderboard", new Vector2(50, 50), Color.White);

            int startY = 100;
            int startX = 50;
            int i = 1;
            foreach (var entry in Leaderboard)
            {
                spriteBatch.DrawString(Game.Content.Load<SpriteFont>("RegularFont"), i + ". " + entry.Date + " - " + entry.Score, new Vector2(startX, startY), Color.White);
                startY += 50;
                i++;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
