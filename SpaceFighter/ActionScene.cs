using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;

namespace SpaceFighter
{

    public class ActionScene : GameScene
    {

        Game1 g;
        SpriteBatch spriteBatch;

        private float timeElapsed;

        SpriteFont defaultFont;


        Vector2 spaceshipPosition;
        Vector2 backgroundPosition;

        float spaceshipSpeed;
        float backgroundSpeed;
        private float playerHealth = 100f; // starting health, adjust as needed


        List<Obstacle> obstacles;
        Texture2D obstacleTexture;
        Random random;
        float obstacleSpawnTimer;

        // Game-specific properties
        Texture2D spaceshipTexture;
        Texture2D backgroundTexture;

        GameState currentGameState = GameState.Playing;
        public ActionScene(Game game) : base(game)
        {

                 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            defaultFont = Game.Content.Load<SpriteFont>("RegularFont");

            spaceshipTexture = Game.Content.Load<Texture2D>("PlayerShip");
            backgroundTexture = Game.Content.Load<Texture2D>("Background");

            spaceshipPosition = new Vector2(
                g.GraphicsDevice.Viewport.Width / 2, 
                g.GraphicsDevice.Viewport.Height - 100);
            backgroundPosition = new Vector2(0, 0);

            spaceshipSpeed = 200f;
            backgroundSpeed = 30f;

            obstacles = new List<Obstacle>();
            obstacleTexture = Game.Content.Load<Texture2D>("Asteroid");
            random = new Random();
            obstacleSpawnTimer = 0f;

        }

        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Stop();
                }
                currentGameState = GameState.GameOver;
            }

            if (currentGameState == GameState.GameOver)
            {
                // Show time elapsed to user and on click ok game should be closed
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    RestartGame();
                }
            }
            else
            {
                try
                {

                    foreach (var obstacle in obstacles)
                    {
                        if (CheckCollision(obstacle))
                        {
                            if (playerHealth <= 0)
                            {
                                currentGameState = GameState.GameOver;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }


                // Update the background position for scrolling
                backgroundPosition.Y += backgroundSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (backgroundPosition.Y >= backgroundTexture.Height)
                {
                    backgroundPosition.Y -= backgroundTexture.Height;
                }

                // Increment the time elapsed
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Increase the background speed over time
                // For example, increase speed by 1 every second
                backgroundSpeed += float.Parse(gameTime.ElapsedGameTime.TotalSeconds.ToString("0.00"));

                // Handle spaceship movement based on keyboard input
                var keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Right))
                    spaceshipPosition.X += spaceshipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (keyboardState.IsKeyDown(Keys.Left))
                    spaceshipPosition.X -= spaceshipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                obstacleSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (obstacleSpawnTimer > 1.5f) // Spawn an obstacle every 1.5 seconds, adjust as needed
                {
                    obstacleSpawnTimer = 0;
                    Vector2 pos = new Vector2(random.Next(0, g.GraphicsDevice.Viewport.Width - obstacleTexture.Width), -obstacleTexture.Height);
                    Obstacle newObstacle = new Obstacle(g, spriteBatch, pos, 100f); // Adjust speed as needed
                    obstacles.Add(newObstacle);
                }

                for (int i = obstacles.Count - 1; i >= 0; i--)
                {
                    obstacles[i].Update(gameTime);
                    if (obstacles[i].Position.Y > g.GraphicsDevice.Viewport.Height) // Remove off-screen obstacles
                    {
                        obstacles.RemoveAt(i);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, g.GraphicsDevice.Viewport.Width, g.GraphicsDevice.Viewport.Height), Color.White);

            switch (currentGameState)
            {
                case GameState.Playing:
                    DrawPlaying(spriteBatch, gameTime);
                    break;
                case GameState.GameOver:
                    DrawGameOver(spriteBatch);
                    break;
            }

            spriteBatch.End();
        }

        private void DrawPlaying(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle((int)backgroundPosition.X, (int)backgroundPosition.Y - backgroundTexture.Height, g.GraphicsDevice.Viewport.Width, backgroundTexture.Height), Color.White);
            // The second draw call draws the first part of the background

            // Draw the spaceship
            spriteBatch.Draw(spaceshipTexture, spaceshipPosition, Color.White);

            // Draw the obstacles
            foreach (var obstacle in obstacles)
            {
                obstacle.Draw(gameTime);
            }


            var healthText = $"Health: {playerHealth}%";
            spriteBatch.DrawString(defaultFont, healthText, new Vector2(10, 10), Color.White); // Adjust position as needed


        }


        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            var gameOverText = "Game Over";
            var gameOverTextSize = defaultFont.MeasureString(gameOverText);
            var gameOverTextPosition = new Vector2(g.GraphicsDevice.Viewport.Width / 2 - gameOverTextSize.X / 2, g.GraphicsDevice.Viewport.Height / 2 - gameOverTextSize.Y / 2);

            var timeElapsedText = $"Time Elapsed: {timeElapsed.ToString("0.00")} seconds";
            var timeElapsedTextSize = defaultFont.MeasureString(timeElapsedText);
            var timeElapsedTextPosition = new Vector2(g.GraphicsDevice.Viewport.Width / 2 - timeElapsedTextSize.X / 2, gameOverTextPosition.Y + gameOverTextSize.Y + 20);

            var restartText = "Press R to restart";
            var restartTextSize = defaultFont.MeasureString(restartText);
            var restartTextPosition = new Vector2(g.GraphicsDevice.Viewport.Width / 2 - restartTextSize.X / 2, timeElapsedTextPosition.Y + timeElapsedTextSize.Y + 20);

            spriteBatch.DrawString(defaultFont, gameOverText, gameOverTextPosition, Color.White);
            spriteBatch.DrawString(defaultFont, timeElapsedText, timeElapsedTextPosition, Color.White);
            spriteBatch.DrawString(defaultFont, restartText, restartTextPosition, Color.White);
        }


        private bool CheckCollision(Obstacle obstacle)
        {
            Rectangle spaceshipRect = new Rectangle((int)spaceshipPosition.X, (int)spaceshipPosition.Y, spaceshipTexture.Width, spaceshipTexture.Height);

            if (spaceshipRect.Intersects(obstacle.BoundingBox))
            {
                playerHealth -= 30;

                obstacles.Remove(obstacle);



                return true;
            }

            return false;
        }

        private void RestartGame()
        {
            LeaderboardManager.Instance.SaveLeaderboard(timeElapsed);

            playerHealth = 100f; // Reset health


            currentGameState = GameState.Playing;
            backgroundSpeed = 30f;
            timeElapsed = 0f;
            spaceshipPosition = new Vector2(g.GraphicsDevice.Viewport.Width / 2, g.GraphicsDevice.Viewport.Height - 100);

            obstacles.Clear();
        }


        public void OnExit()
        {
            if (currentGameState == GameState.GameOver)
            {
                LeaderboardManager.Instance.SaveLeaderboard(timeElapsed);
            }
        }

    }

}
