using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    public class Obstacle : DrawableGameComponent
    {
        public Texture2D Texture;
        public Vector2 Position;
        public float Speed;

        private SpriteBatch spriteBatch;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public Obstacle(Game game, SpriteBatch spriteBatch, Vector2 position, float speed) : base(game)
        {
            Texture = game.Content.Load<Texture2D>("Asteroid");
            Position = position;
            Speed = speed;
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }



        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }

}
