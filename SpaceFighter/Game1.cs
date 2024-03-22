using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceFighter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        //change private to public
        public SpriteBatch _spriteBatch;


        private StartScene startScene;
        private ActionScene actionScene;
        private AboutScene aboutScene;
        private HelpScene helpScene;
        private LeaderboardScene leaderboardScene;

        public static Song clickSound;

        private Texture2D background;

        private Song backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);


            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            background = Content.Load<Texture2D>("Background");

            backgroundMusic = Content.Load<Song>("BackgroundTrack");


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            leaderboardScene = new LeaderboardScene(this);
            this.Components.Add(leaderboardScene);


            clickSound = Content.Load<Song>("Click");

            startScene.show();
        }

        private void hideAllScenes()
        {
            MediaPlayer.Stop();
            foreach (GameScene item in Components)
            {
                item.hide();
            }
        }


        protected override void Update(GameTime gameTime)
        {
            if (startScene.Visible)
            {
                if (startScene.Menu.btnStart.isClicked())
                {
                    hideAllScenes();
                    actionScene.show();
                    MediaPlayer.Play(backgroundMusic);
                }
                else if (startScene.Menu.btnAbout.isClicked())
                {
                    hideAllScenes();
                    aboutScene.show();
                    PlayClickSound();
                }
                else if (startScene.Menu.btnHelp.isClicked())
                {
                    hideAllScenes();
                    helpScene.show();
                    PlayClickSound();
                }
                else if (startScene.Menu.btnLeaderboard.isClicked())
                {
                    hideAllScenes();
                    leaderboardScene.show();
                    PlayClickSound();
                }
                else if (startScene.Menu.btnQuit.isClicked())
                {
                    Exit();
                }
            }

            if (actionScene.Enabled || aboutScene.Enabled || helpScene.Enabled || leaderboardScene.Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                    _graphics.ApplyChanges();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);

        }

        private void PlayClickSound()
        {
            MediaPlayer.Play(clickSound);
        }
    }

}
