using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Galaga
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle background;
        Rectangle name;
        Rectangle arrow;
        Texture2D galagaNameArt;
        Texture2D space;
        Texture2D pointer;
        Texture2D spaceShipTexture;
        Texture2D spaceShip2Texture;
        SpriteFont homefont;
        KeyboardState old;
        bool mainmenu;
        bool oneplayer;
        bool twoplayer;
        bool highscore;
        Vector2 arrowPos;
        int highscoreNum;
        bool homeScreen = true;
        int[] playerXLocs;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 480;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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
            background = new Rectangle(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            name = new Rectangle(GraphicsDevice.Viewport.Width/2 - 160, 100,320,179);
            arrow = new Rectangle(name.X+50,name.Y+200,25,25);
            arrowPos = new Vector2(arrow.X,arrow.Y);
            mainmenu = true;
            oneplayer = false;
            twoplayer = false;
            highscore = false;
            old = Keyboard.GetState();
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
            space = this.Content.Load<Texture2D>("space");
            galagaNameArt = this.Content.Load<Texture2D>("galaga");
            pointer = this.Content.Load<Texture2D>("Pointer");
            spaceShipTexture = Content.Load<Texture2D>("spaceship");
            spaceShip2Texture = Content.Load<Texture2D>("spaceship2");
            homefont = this.Content.Load<SpriteFont>("HomePlayerSelection");
            highscoreNum = ReadFileOfIntegers(@"Content/high.txt");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        private int ReadFileOfIntegers(string path)
        {
            string line = "";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while(!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        
                    }
                }
            }
            catch(Exception e)
            {
                return 1;
            }
            return Convert.ToInt32(line);
        }
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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if(kb.IsKeyDown(Keys.Down) && !old.IsKeyDown(Keys.Down))
            {
                if (arrow.Y == arrowPos.Y)
                    arrow.Y += 50;
                else
                    arrow.Y = (int)arrowPos.Y;
            }
            if (kb.IsKeyDown(Keys.Up) && !old.IsKeyDown(Keys.Up))
            {
                if (arrow.Y == arrowPos.Y)
                    arrow.Y += 50;
                else
                    arrow.Y = (int)arrowPos.Y;
            }
            if (kb.IsKeyDown(Keys.Enter) && !old.IsKeyDown(Keys.Enter))
            {
                homeScreen = false;
                if (arrow.Y == arrowPos.Y)
                {
                    playerXLocs = new int[1];
                }
                else if (arrow.Y == arrowPos.Y + 50)
                {
                    playerXLocs = new int[2];
                    playerXLocs[1] = 560;
                }
                playerXLocs[0] = 30;
            }
            //The left right controls for both players
            if (kb.IsKeyDown(Keys.A))
            {
                playerXLocs[0] = playerXLocs[0] - 3;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                playerXLocs[0] = playerXLocs[0] + 3;
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                playerXLocs[1] = playerXLocs[1] - 3;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                playerXLocs[1] = playerXLocs[1] + 3;
            }
            //If the game has started,makes sure the players don't go off screen
            if (!homeScreen) {
             for (int x = 0; x < playerXLocs.GetLength(0); x++) { if (playerXLocs[x] < 0) { playerXLocs[x] = 0; } else if (playerXLocs[x] > 590) { playerXLocs[x] = 590; } }
            }
            old = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(space,background,Color.White);
            //Draws the home screen if the player hasn't started a game
            if (homeScreen)
            {
                spriteBatch.Draw(galagaNameArt, name, Color.White);
                spriteBatch.Draw(pointer, arrow, Color.White);
                spriteBatch.DrawString(homefont, "One Player", new Vector2(arrowPos.X + 50, arrowPos.Y), Color.White);
                spriteBatch.DrawString(homefont, "Two Player", new Vector2(arrowPos.X + 50, arrowPos.Y + 50), Color.White);
                spriteBatch.DrawString(homefont, "High Score: ", new Vector2(250, 10), Color.Red);
                spriteBatch.DrawString(homefont, highscoreNum.ToString(), new Vector2(375, 10), Color.White);
            }
            //Otherwise draws the game
            else
            {
                for (int x = 0; x < playerXLocs.GetLength(0); x++)
                {
                    Rectangle shipRectangle = new Rectangle(playerXLocs[x], 400, 50, 50);
                    Texture2D tempShipTexture = spaceShipTexture;
                    if (x == 1)
                    {
                        tempShipTexture = spaceShip2Texture;
                    }
                    spriteBatch.Draw(tempShipTexture, shipRectangle, Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
