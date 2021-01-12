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
        //Declare rectangles for game
        Rectangle background;
        Rectangle name;
        Rectangle arrow;
        //Declare Textures for game
        Texture2D galagaNameArt;
        Texture2D space;
        Texture2D pointer;
        Texture2D spaceShipTexture;
        Texture2D spaceShip2Texture;
        Texture2D bulletTexture;
        //The font for most items
        SpriteFont homefont;
        //Stores old keyboard state
        KeyboardState old;
        //Keeps a list of locations for all bullets
        List<int[,]> bulletLocations = new List<int[,]>();

        int[] timeForPlayers = new int[2];

        bool mainmenu;
        bool oneplayer;
        bool twoplayer;
        bool highscore;
        Vector2 arrowPos;
        int highscoreNum;
        //Keeps track of whether the game has started or not
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
            //Initialize all the variables
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
            bulletTexture = Content.Load<Texture2D>("bullet");
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
            //Down and up are used to switch between single player and two player
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
            //Pressing enter starts the game
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
            //The left right controls for both players. A and D for player 1,and Left and Right for player 2
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
            //Loops through the wait times(how long to wait before firing again) for players
            for (int x=0; x<timeForPlayers.Length;x++) {
                //Once halfway through the wait the second bullet is shot
                if (timeForPlayers[x] == 10) {
                    int[,] bulletLocation = new int[,] { { playerXLocs[x] + 19, 365 } };
                    bulletLocations.Add(bulletLocation);
                }
                //Decrease the wait # if it's greater than 0
                if (timeForPlayers[x]>0) {
                    timeForPlayers[x]--;
                }
            }
            //If the game has started,display everything that is needed
            if (!homeScreen) {
                //If Space is pressed a bullet is fired for player one
             for (int x = 0; x < playerXLocs.GetLength(0); x++) { if (playerXLocs[x] < 0) { playerXLocs[x] = 0; } else if (playerXLocs[x] > 590) { playerXLocs[x] = 590; } }
                if (kb.IsKeyDown(Keys.Space) && old.IsKeyUp(Keys.Space) && timeForPlayers[0] == 0){
                    int[,] bulletLocation = new int[,] { {playerXLocs[0]+19,365} };
                    bulletLocations.Add(bulletLocation);
                    //The wait time before being able to fire again
                    timeForPlayers[0] = 20;
                }
                //If right shift is pressed a bullet is fired for player two
                if (kb.IsKeyDown(Keys.RightShift) && old.IsKeyUp(Keys.RightShift) && timeForPlayers[1] == 0)
                {
                    int[,] bulletLocation = new int[,] { { playerXLocs[1] + 19, 365 } };
                    bulletLocations.Add(bulletLocation);
                    //The wait time before being able to fire again
                    timeForPlayers[1] = 20;
                }
            }
            //Removes any bullets that are out of the bounds
            for (int x=0;x<bulletLocations.Count();x++)
            {
                int[,] bulletCoords = bulletLocations.ElementAt<int[,]>(x);
                if (bulletCoords[0,0] > 640 || bulletCoords[0,0] < -12 || bulletCoords[0,1] > 480 || bulletCoords[0,1] < -24) { bulletLocations.RemoveAt(x); }
            }
            //Moves each bullet up
            foreach (int[,] bulletCoord in bulletLocations) {
                bulletCoord[0, 1] -= 7;
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
                //For each player loop through once and draw ship
                for (int x = 0; x < playerXLocs.GetLength(0); x++)
                {
                    Rectangle shipRectangle = new Rectangle(playerXLocs[x], 400, 50, 50);
                    Texture2D tempShipTexture = spaceShipTexture;
                    //for player 2 change ship color
                    if (x == 1)
                    {
                        tempShipTexture = spaceShip2Texture;
                    }
                    //Draw the ship
                    spriteBatch.Draw(tempShipTexture, shipRectangle, Color.White);
                }
                //For each bullet,create a rectangle and draw it
                foreach(int[,] bulletCoords in bulletLocations) {
                    Rectangle tempRecangle = new Rectangle(bulletCoords[0, 0], bulletCoords[0, 1], 12, 24);
                    spriteBatch.Draw(bulletTexture,tempRecangle,Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
