using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFighter
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }


        private SpriteBatch spriteBatch;

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("File");

            // Load button textures
            Texture2D selectedButtonTexture = g.Content.Load<Texture2D>("ButtonClicked");
            Texture2D buttonTexture = g.Content.Load<Texture2D>("Button");

            menu = new MenuComponent(g, spriteBatch, buttonTexture, regularFont);
            this.Components.Add(menu);

        }
    }

}
