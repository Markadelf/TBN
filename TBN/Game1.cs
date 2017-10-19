using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBN.Gameplay.Characters;
using TBN.Gameplay.Input;

namespace TBN
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static SpriteFont Font;
        Texture2D Boxman;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character BoxMan;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Boxman = Content.Load<Texture2D>("CharacterSpriteSheets/Boxman");
            FrameDrawInfo[][] BoxmanRects= new FrameDrawInfo[9][];
            for (int i = 0; i < Boxman.Height/32; i++)
            {
                FrameDrawInfo[] BoxmanRect = new FrameDrawInfo[2];
                for (int j = 0; j < Boxman.Width/32; j++)
                {
                    
                    BoxmanRect[j] = new FrameDrawInfo(new Rectangle(j*32,i*32,32,32), new Vector2(((j+1) * 32) - 16, (i+1) * 32));
                    
                }
                BoxmanRects[i] = BoxmanRect;
            }
            BoxMan = new BoxMan(new Vector2(200,200), new KeyboardController(),new SpriteSheet(Boxman,BoxmanRects));
            Font = Content.Load<SpriteFont>("Fonts/Debug");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            BoxMan.UpdateAction();
            BoxMan.Move();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            BoxMan.Debug(spriteBatch);
            BoxMan.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
