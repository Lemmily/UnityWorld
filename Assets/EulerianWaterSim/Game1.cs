// Eulerian Water Sim : converted to Unity by http://unitycoder.com/blog
// Original Source: http://www.youtube.com/watch?v=ZXPdI0WIvw0&feature=youtu.be

using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerianWaterSim
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1
    {

        WaterSim sim;

        public Game1()
        {
//            graphics = new GraphicsDeviceManager(this);
//            Content.RootDirectory = "Content";
            //TargetElapsedTime = TimeSpan.FromMilliseconds(33);
//            graphics.SynchronizeWithVerticalRetrace = true;
//            graphics.PreferredBackBufferWidth = 1280;
//            graphics.PreferredBackBufferHeight = 720;
//            IsMouseVisible = true;
//            IsFixedTimeStep = false;
        }

//        protected override void Initialize()
//        {
//            sim = new WaterSim();
//            base.Initialize();
//        }

		/*
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");

            white = new Texture2D(GraphicsDevice, 1, 1);

            white.SetData<Color>(new Color[] { Color.White });
        }*/
/*
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
*/
//        KeyboardState oldKey;

/*
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            var mPosX = Mouse.GetState().X / Cell.RenderWidth;
            var mPosY = Mouse.GetState().Y / Cell.RenderHeight;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (mPosX > 0 && mPosX < sim.Width - 1 && mPosY > 0 && mPosY < sim.Height - 1)
                    sim.Grid[mPosX, mPosY].Level = (byte)Cell.MaxLevel;
            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (mPosX > 0 && mPosX < sim.Width - 1 && mPosY > 0 && mPosY < sim.Height - 1)
                    sim.Grid[mPosX, mPosY].Tile = TileType.Dirt;
            }

            var key = Keyboard.GetState();

            //if (key.IsKeyDown(Keys.Space) && oldKey.IsKeyUp(Keys.Space))
                //sim.UpdateNextCell();
                sim.Update();

            oldKey = key;

            base.Update(gameTime);
        }*/
/*
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            sim.DebugRender(spriteBatch, font, white);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, sim.CountFluid().ToString(), new Vector2(400, 15), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
		*/
    }
}
