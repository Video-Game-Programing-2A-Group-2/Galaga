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
        int score = 0;

        int shipsLeft = 2;

        //Stores info about each enemy
        List<Object> enemyInfo = new List<Object>();
        //current enemy spawn and lokation
        
        List<Rectangle> BossRec = new List<Rectangle>();
        List<Rectangle> RedRec = new List<Rectangle>();
        List<Rectangle> BeeRec = new List<Rectangle>();

        List<bool> brB = new List<bool>();
        List<bool> rrB = new List<bool>();
        List<bool> berB = new List<bool>();
        //List<Rectangle> BossRec = new List<Rectangle>();
        //List<Rectangle> RedRec = new List<Rectangle>();
        //List<Rectangle> BeeRec = new List<Rectangle>();

        List<Rectangle> EnemyRec = new List<Rectangle>();

        List<int[,]> enemyBull = new List<int[,]>();

        Rectangle temprec;

        Texture2D EnemyBullet;

        Rectangle recbg1, recbg2, recbg3, recbg4;
        Texture2D texbg;
        Rectangle recrg1, recrg2, recrg3, recrg4, recrg5, recrg6, recrg7, recrg8, recrg9, recrg10, recrg11, recrg12, recrg13, recrg14, recrg15, recrg16;
        Texture2D texrg;
        Rectangle recbeg1, recbeg2, recbeg3, recbeg4, recbeg5, recbeg6, recbeg7, recbeg8, recbeg9, recbeg10, recbeg11, recbeg12, recbeg13, recbeg14, recbeg15, recbeg16, recbeg17, recbeg18, recbeg19, recbeg20;
        Texture2D texbeg;
        private double timer;
        private int seconds;
        private bool expand;
        private double x1, x2, x3, x4, x5;
        private double y1, y2, y3, y4, y5;
        int randET;

        Random gen = new Random();


        Song intro;
        Song captured;
        Song rescued;
        Song destroyed;
        Song oneup;
        Song diestartup;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 480;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 640;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load` any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initialize all the variables

            //Create 8 of each enemy type
            for (int y = 0; y < 3; y++) {
                for (int x=0;x<8;x++) {
                    List<Object> enemyInfo = new List<Object>();
                    enemyInfo.Add(new Rectangle(x*40+90,y*30+45,25,25)); 
                    enemyInfo.Add(y);
                    this.enemyInfo.Add(enemyInfo);
                }
            }


            background = new Rectangle(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            name = new Rectangle(GraphicsDevice.Viewport.Width/2 - 160, 100,320,179);
            arrow = new Rectangle(name.X+50,name.Y+200,25,25);
            arrowPos = new Vector2(arrow.X,arrow.Y);
            mainmenu = true;
            oneplayer = false;
            twoplayer = false;
            highscore = false;
            old = Keyboard.GetState();
            
            
            BossRec.Add(recbg1 = new Rectangle(208, 50, 16, 16));
            BossRec.Add(recbg2 = new Rectangle(224, 50, 16, 16));
            BossRec.Add(recbg3 = new Rectangle(240, 50, 16, 16));
            BossRec.Add(recbg4 = new Rectangle(256, 50, 16, 16));

            RedRec.Add(recrg1 = new Rectangle(176, 66, 16, 16));
            RedRec.Add(recrg2 = new Rectangle(192, 66, 16, 16));
            RedRec.Add(recrg3 = new Rectangle(208, 66, 16, 16));
            RedRec.Add(recrg4 = new Rectangle(224, 66, 16, 16));
            RedRec.Add(recrg5 = new Rectangle(240, 66, 16, 16));
            RedRec.Add(recrg6 = new Rectangle(256, 66, 16, 16));
            RedRec.Add(recrg7 = new Rectangle(272, 66, 16, 16));
            RedRec.Add(recrg8 = new Rectangle(288, 66, 16, 16));
            RedRec.Add(recrg9 = new Rectangle(176, 82, 16, 16));
            RedRec.Add(recrg10 = new Rectangle(192, 82, 16, 16));
            RedRec.Add(recrg11 = new Rectangle(208, 82, 16, 16));
            RedRec.Add(recrg12 = new Rectangle(224, 82, 16, 16));
            RedRec.Add(recrg13 = new Rectangle(240, 82, 16, 16));
            RedRec.Add(recrg14 = new Rectangle(256, 82, 16, 16));
            RedRec.Add(recrg15 = new Rectangle(272, 82, 16, 16));
            RedRec.Add(recrg16 = new Rectangle(288, 82, 16, 16));

            BeeRec.Add(recbeg1 = new Rectangle(160, 98, 16, 16));
            BeeRec.Add(recbeg2 = new Rectangle(176, 98, 16, 16));
            BeeRec.Add(recbeg3 = new Rectangle(192, 98, 16, 16));
            BeeRec.Add(recbeg4 = new Rectangle(208, 98, 16, 16));
            BeeRec.Add(recbeg5 = new Rectangle(224, 98, 16, 16));
            BeeRec.Add(recbeg6 = new Rectangle(240, 98, 16, 16));
            BeeRec.Add(recbeg7 = new Rectangle(256, 98, 16, 16));
            BeeRec.Add(recbeg8 = new Rectangle(272, 98, 16, 16));
            BeeRec.Add(recbeg9 = new Rectangle(288, 98, 16, 16));
            BeeRec.Add(recbeg10 = new Rectangle(304, 98, 16, 16));
            BeeRec.Add(recbeg11 = new Rectangle(160, 114, 16, 16));
            BeeRec.Add(recbeg12 = new Rectangle(176, 114, 16, 16));
            BeeRec.Add(recbeg13 = new Rectangle(192, 114, 16, 16));
            BeeRec.Add(recbeg14 = new Rectangle(208, 114, 16, 16));
            BeeRec.Add(recbeg15 = new Rectangle(224, 114, 16, 16));
            BeeRec.Add(recbeg16 = new Rectangle(240, 114, 16, 16));
            BeeRec.Add(recbeg17 = new Rectangle(256, 114, 16, 16));
            BeeRec.Add(recbeg18 = new Rectangle(272, 114, 16, 16));
            BeeRec.Add(recbeg19 = new Rectangle(288, 114, 16, 16));
            BeeRec.Add(recbeg20 = new Rectangle(304, 114, 16, 16));

            for (int i = 0; i < 4; i++) { brB.Add(true); }
            for (int i = 0; i < 16; i++) { rrB.Add(true); }
            for (int i = 0; i < 20; i++) { berB.Add(true); }


            timer = 0;
            seconds = 0;

            expand = false;

            x1 = 0;
            x2 = 0;
            x3 = 0;
            x4 = 0;
            x5 = 0;

            y1 = 0;
            y2 = 0;
            y3 = 0;
            y4 = 0;
            y5 = 0;

            randET = 0;

            LoadContent();
            MediaPlayer.Play(intro);
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

            //enemy
            texbg = this.Content.Load<Texture2D>("BossGalaga");
            texrg = this.Content.Load<Texture2D>("RedGalaga");
            texbeg = this.Content.Load<Texture2D>("BeeGalaga");

            EnemyBullet = Content.Load<Texture2D>("eBullet");

            // TODO: use this.Content to load your game content here
            intro = this.Content.Load<Song>("01 Stage Intro");
             captured = this.Content.Load<Song>("02 FIghter Captured");
             rescued = this.Content.Load<Song>("03 Fighter Rescued");
             destroyed = this.Content.Load<Song>("04 Captured Fighter Destroyed");
             oneup = this.Content.Load<Song>("08 1-Up");
             diestartup = this.Content.Load<Song>("09 Die-Start Up Sound");
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
            if (kb.IsKeyDown(Keys.Enter) && !old.IsKeyDown(Keys.Enter) && playerXLocs == null)
            {
                homeScreen = false;
                if (arrow.Y == arrowPos.Y)
                {
                    playerXLocs = new int[1];
                }
                else if (arrow.Y == arrowPos.Y + 50)
                {
                    playerXLocs = new int[2];
                    playerXLocs[1] = 400;
                }
                playerXLocs[0] = 30;
            }
            //The left right controls for both players. A and D for player 1,and Left and Right for player 2
            Console.WriteLine("TEST");
            if (kb.IsKeyDown(Keys.A) && playerXLocs != null)
            {
                playerXLocs[0] = playerXLocs[0] - 3;
            }
            if (kb.IsKeyDown(Keys.D) && playerXLocs != null)
            {
                playerXLocs[0] = playerXLocs[0] + 3;
            }
            if (kb.IsKeyDown(Keys.Left) && playerXLocs != null && playerXLocs.Length >= 2)
            {
                playerXLocs[1] = playerXLocs[1] - 3;
            }
            if (kb.IsKeyDown(Keys.Right) && playerXLocs != null && playerXLocs.Length >= 2)
            {
                playerXLocs[1] = playerXLocs[1] + 3;
            }
            //Loops through the wait times(how long to wait before firing again) for players
            for (int x=0; x<timeForPlayers.Length;x++) {
                //Once halfway through the wait the second bullet is shot
                if (timeForPlayers[x] == 10) {
                    int[,] bulletLocation = new int[,] { { playerXLocs[x] + 19, 570 } };
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
             for (int x = 0; x < playerXLocs.GetLength(0); x++) { if (playerXLocs[x] < 0) { playerXLocs[x] = 0; } else if (playerXLocs[x] > 430) { playerXLocs[x] = 430; } }
                if (kb.IsKeyDown(Keys.Space) && old.IsKeyUp(Keys.Space) && timeForPlayers[0] == 0 && playerXLocs.Length >= 1)
                {
                    int[,] bulletLocation = new int[,] { {playerXLocs[0]+19,570} };
                    bulletLocations.Add(bulletLocation);
                    //The wait time before being able to fire again
                    timeForPlayers[0] = 20;
                }
                //If right shift is pressed a bullet is fired for player two
                if (kb.IsKeyDown(Keys.RightShift) && old.IsKeyUp(Keys.RightShift) && timeForPlayers[1] == 0 && playerXLocs.Length >= 2)
                {
                    int[,] bulletLocation = new int[,] { { playerXLocs[1] + 19, 570 } };
                    bulletLocations.Add(bulletLocation);
                    //The wait time before being able to fire again
                    timeForPlayers[1] = 20;
                }
                
            }
            //Loop through all the bullets
            for (int x=0;x<bulletLocations.Count();x++)
            {
                //BulletCoords is a array with the x and y coord of the current bullet
                int[,] bulletCoords = bulletLocations.ElementAt<int[,]>(x);
                //Remove the bullet if it goes off screen
                if (bulletCoords[0, 0] > 480 || bulletCoords[0, 0] < -12 || bulletCoords[0, 1] > 640 || bulletCoords[0, 1] < -24) { bulletLocations.RemoveAt(x); }
                //Check if a bullet collides with a enemy,and delete the bullet and enemy if it does
                Rectangle tempRecangle = new Rectangle(bulletCoords[0, 0], bulletCoords[0, 1], 12, 24);
                for (int i=0;i<enemyInfo.Count;i++) {
                    List<Object> enemyList = (List<Object>)enemyInfo.ElementAt<Object>(i);
                    Rectangle tempEnemyRectangle = (Rectangle)enemyList.ElementAt<Object>(0);
                    if (tempRecangle.Intersects(tempEnemyRectangle)) {
                        bulletLocations.RemoveAt(x);
                        enemyInfo.RemoveAt(i);
                        int tempAlienType = (int)enemyList.ElementAt<Object>(1);
                        //Award a different amount of points based on which enemy type was killed
                        score += tempAlienType == 0 ? 1000 : tempAlienType == 1?200:100;
                    }
                }
                for (int i = 0; i < RedRec.Count; i++)
                {
                    if (tempRecangle.Intersects(RedRec.ElementAt(i)))
                    {
                        bulletLocations.RemoveAt(x);
                        score += 200;
                        rrB[i] = false;
                        break;
                    }
                }
                for (int i = 0; i < BeeRec.Count; i++)
                {
                    if (tempRecangle.Intersects(BeeRec.ElementAt(i)))
                    {
                        bulletLocations.RemoveAt(x);
                        score += 100;
                        berB[i] = false;
                        break;
                    }
                }
                for (int i = 0; i < BossRec.Count; i++)
                {
                    if (tempRecangle.Intersects(BossRec.ElementAt(i)))
                    {
                        bulletLocations.RemoveAt(x);
                        score += 1000;
                        brB[i] = false;
                        break;
                    }
                }
            }
            //Moves each bullet up
            foreach (int[,] bulletCoord in bulletLocations) {
                bulletCoord[0, 1] -= 7;
            }
            old = kb;



            //enemy spawn and movement
            if(homeScreen == false)
            {
                timer++;
                if (timer >= 240.0) { timer = 0; }
                if (timer < 120.0) { expand = true; }
                else { expand = false; }
                if (timer % 2.5 == 0 && expand)
                {
                    x1 += .125;
                    x2 += .25;
                    x3 += .5;
                    x4 += 1;
                    x5 += 2;

                    y1 += .125;
                    y2 += .25;
                    y3 += .5;
                    y4 += 1;
                    y5 += 2;
                }
                else if (timer % 2.5 == 0 && expand == false)
                {
                    x1 -= .125;
                    x2 -= .25;
                    x3 -= .5;
                    x4 -= 1;
                    x5 -= 2;

                    y1 -= .125;
                    y2 -= .25;
                    y3 -= .5;
                    y4 -= 1;
                    y5 -= 2;
                }

                if (timer % 40 == 0)
                {

                    temprec = BossRec[0];
                    temprec.X = 208 - (int)x2;
                    BossRec[0] = temprec;
                    temprec = BossRec[1];
                    temprec.X = 224 - (int)x1;
                    BossRec[1] = temprec;
                    temprec = BossRec[2];
                    temprec.X = 240 + (int)x1;
                    BossRec[2] = temprec;
                    temprec = BossRec[3];
                    temprec.X = 256 + (int)x2;
                    BossRec[3] = temprec;

                    temprec = BossRec[0];
                    temprec.Y = 50 + (int)y1;
                    temprec = BossRec[1];
                    temprec.Y = 50 + (int)y1;
                    temprec = BossRec[2];
                    temprec.Y = 50 + (int)y1;
                    temprec = BossRec[3];
                    temprec.Y = 50 + (int)y1;
                }
                if (timer % 20 == 0)
                {
                    temprec = RedRec[0];
                    temprec.X = 176 - (int)x4;
                    RedRec[0] = temprec;
                    temprec = RedRec[1];
                    temprec.X = 192 - (int)x3;
                    RedRec[1] = temprec;
                    temprec = RedRec[2];
                    temprec.X = 208 - (int)x2;
                    RedRec[2] = temprec;
                    temprec = RedRec[3];
                    temprec.X = 224 - (int)x1;
                    RedRec[3] = temprec;
                    temprec = RedRec[4];
                    temprec.X = 240 + (int)x1;
                    RedRec[4] = temprec;
                    temprec = RedRec[5];
                    temprec.X = 256 + (int)x2;
                    RedRec[5] = temprec;
                    temprec = RedRec[6];
                    temprec.X = 272 + (int)x3;
                    RedRec[6] = temprec;
                    temprec = RedRec[7];
                    temprec.X = 288 + (int)x4;
                    RedRec[7] = temprec;

                    temprec = RedRec[0];
                    temprec.Y = 66 + (int)y2;
                    RedRec[0] = temprec;
                    temprec = RedRec[1];
                    temprec.Y = 66 + (int)y2;
                    RedRec[1] = temprec;
                    temprec = RedRec[2];
                    temprec.Y = 66 + (int)y2;
                    RedRec[2] = temprec;
                    temprec = RedRec[3];
                    temprec.Y = 66 + (int)y2;
                    RedRec[3] = temprec;
                    temprec = RedRec[4];
                    temprec.Y = 66 + (int)y2;
                    RedRec[4] = temprec;
                    temprec = RedRec[5];
                    temprec.Y = 66 + (int)y2;
                    RedRec[5] = temprec;
                    temprec = RedRec[6];
                    temprec.Y = 66 + (int)y2;
                    RedRec[6] = temprec;
                    temprec = RedRec[7];
                    temprec.Y = 66 + (int)y2;
                    RedRec[7] = temprec;
                }
                if (timer % 10 == 0)
                {
                    temprec = RedRec[8];
                    temprec.X = 176 - (int)x4;
                    RedRec[8] = temprec;
                    temprec = RedRec[9];
                    temprec.X = 192 - (int)x3;
                    RedRec[9] = temprec;
                    temprec = RedRec[10];
                    temprec.X = 208 - (int)x2;
                    RedRec[10] = temprec;
                    temprec = RedRec[11];
                    temprec.X = 224 - (int)x1;
                    RedRec[11] = temprec;
                    temprec = RedRec[12];
                    temprec.X = 240 + (int)x1;
                    RedRec[12] = temprec;
                    temprec = RedRec[13];
                    temprec.X = 256 + (int)x2;
                    RedRec[13] = temprec;
                    temprec = RedRec[14];
                    temprec.X = 272 + (int)x3;
                    RedRec[14] = temprec;
                    temprec = RedRec[15];
                    temprec.X = 288 + (int)x4;
                    RedRec[15] = temprec;

                    temprec = RedRec[8];
                    temprec.Y = 82 + (int)y3;
                    RedRec[8] = temprec;
                    temprec = RedRec[9];
                    temprec.Y = 82 + (int)y3;
                    RedRec[9] = temprec;
                    temprec = RedRec[10];
                    temprec.Y = 82 + (int)y3;
                    RedRec[10] = temprec;
                    temprec = RedRec[11];
                    temprec.Y = 82 + (int)y3;
                    RedRec[11] = temprec;
                    temprec = RedRec[12];
                    temprec.Y = 82 + (int)y3;
                    RedRec[12] = temprec;
                    temprec = RedRec[13];
                    temprec.Y = 82 + (int)y3;
                    RedRec[13] = temprec;
                    temprec = RedRec[14];
                    temprec.Y = 82 + (int)y3;
                    RedRec[14] = temprec;
                    temprec = RedRec[15];
                    temprec.Y = 82 + (int)y3;
                    RedRec[15] = temprec;
                }
                if (timer % 5 == 0)
                {
                    temprec = BeeRec[0];
                    temprec.X = 160 - (int)x5;
                    BeeRec[0] = temprec;
                    temprec = BeeRec[1];
                    temprec.X = 176 - (int)x4;
                    BeeRec[1] = temprec;
                    temprec = BeeRec[2];
                    temprec.X = 192 - (int)x3;
                    BeeRec[2] = temprec;
                    temprec = BeeRec[3];
                    temprec.X = 208 - (int)x2;
                    BeeRec[3] = temprec;
                    temprec = BeeRec[4];
                    temprec.X = 224 - (int)x1;
                    BeeRec[4] = temprec;
                    temprec = BeeRec[5];
                    temprec.X = 240 + (int)x1;
                    BeeRec[5] = temprec;
                    temprec = BeeRec[6];
                    temprec.X = 256 + (int)x2;
                    BeeRec[6] = temprec;
                    temprec = BeeRec[7];
                    temprec.X = 272 + (int)x3;
                    BeeRec[7] = temprec;
                    temprec = BeeRec[8];
                    temprec.X = 288 + (int)x4;
                    BeeRec[8] = temprec;
                    temprec = BeeRec[9];
                    temprec.X = 304 + (int)x5;
                    BeeRec[9] = temprec;

                    temprec = BeeRec[0];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[0] = temprec;
                    temprec = BeeRec[1];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[1] = temprec;
                    temprec = BeeRec[2];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[2] = temprec;
                    temprec = BeeRec[3];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[3] = temprec;
                    temprec = BeeRec[4];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[4] = temprec;
                    temprec = BeeRec[5];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[5] = temprec;
                    temprec = BeeRec[6];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[6] = temprec;
                    temprec = BeeRec[7];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[7] = temprec;
                    temprec = BeeRec[8];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[8] = temprec;
                    temprec = BeeRec[9];
                    temprec.Y = 98 + (int)y4;
                    BeeRec[9] = temprec;
                }
                if (timer % 2.5 == 0)
                {
                    temprec = BeeRec[10];
                    temprec.X = 160 - (int)x5;
                    BeeRec[10] = temprec;
                    temprec = BeeRec[11];
                    temprec.X = 176 - (int)x4;
                    BeeRec[11] = temprec;
                    temprec = BeeRec[12];
                    temprec.X = 192 - (int)x3;
                    BeeRec[12] = temprec;
                    temprec = BeeRec[13];
                    temprec.X = 208 - (int)x2;
                    BeeRec[13] = temprec;
                    temprec = BeeRec[14];
                    temprec.X = 224 - (int)x1;
                    BeeRec[14] = temprec;
                    temprec = BeeRec[15];
                    temprec.X = 240 + (int)x1;
                    BeeRec[15] = temprec;
                    temprec = BeeRec[16];
                    temprec.X = 256 + (int)x2;
                    BeeRec[16] = temprec;
                    temprec = BeeRec[17];
                    temprec.X = 272 + (int)x3;
                    BeeRec[17] = temprec;
                    temprec = BeeRec[18];
                    temprec.X = 288 + (int)x4;
                    BeeRec[18] = temprec;
                    temprec = BeeRec[19];
                    temprec.X = 304 + (int)x5;
                    BeeRec[19] = temprec;

                    temprec = BeeRec[10];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[10] = temprec;
                    temprec = BeeRec[11];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[11] = temprec;
                    temprec = BeeRec[12];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[12] = temprec;
                    temprec = BeeRec[13];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[13] = temprec;
                    temprec = BeeRec[14];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[14] = temprec;
                    temprec = BeeRec[15];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[15] = temprec;
                    temprec = BeeRec[16];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[16] = temprec;
                    temprec = BeeRec[17];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[17] = temprec;
                    temprec = BeeRec[18];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[18] = temprec;
                    temprec = BeeRec[19];
                    temprec.Y = 114 + (int)y5;
                    BeeRec[19] = temprec;
                }


                //Enemy Shooting
                if (timer % 120 == 0)
                {

                    //bullet 1
                    randET = gen.Next(3);
                    if (randET == 0)
                    {
                        randET = gen.Next(4);
                        temprec = BossRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);

                    }
                    else if (randET == 1)
                    {
                        randET = gen.Next(16);
                        temprec = RedRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);
                    }
                    else
                    {
                        randET = gen.Next(20);
                        temprec = BeeRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);
                    }

                    //bullet 2
                    randET = gen.Next(3);
                    if (randET == 0)
                    {
                        randET = gen.Next(4);
                        temprec = BossRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);

                    }
                    else if (randET == 1)
                    {
                        randET = gen.Next(16);
                        temprec = RedRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);
                    }
                    else
                    {
                        randET = gen.Next(20);
                        temprec = BeeRec[randET];
                        int[,] enemyB = new int[,] { { temprec.X, temprec.Y } };
                        enemyBull.Add(enemyB);
                    }
                }

                //moves enemy bullet down
                foreach (int[,] bulletCoord in enemyBull)
                {
                    bulletCoord[0, 1] += 7;
                }

                ////Removes any bullets that are out of the bounds
                //for (int x = 0; x < enemyBull.Count(); x++)
                //{
                //    int[,] bulletCoords = enemyBull.ElementAt<int[,]>(x);
                //    if (bulletCoords[0, 0] > 480 || bulletCoords[0, 0] < -12 || bulletCoords[0, 1] > 640 || bulletCoords[0, 1] < -24) { enemyBull.RemoveAt(x); }
                //    Rectangle tempRecangle = new Rectangle(bulletCoords[0, 0], bulletCoords[0, 1], 12, 24);
                //    for (int i = 0; i < RedRec.Count; i++)
                //    {
                //        if (tempRecangle.Intersects(RedRec.ElementAt(i)))
                //        {
                //            enemyBull.RemoveAt(x);
                //            RedRec.RemoveAt(i);
                //            score += 200;
                //            break;
                //        }
                //    }
                //    for (int i = 0; i < BeeRec.Count; i++)
                //    {
                //        if (tempRecangle.Intersects(BeeRec.ElementAt(i)))
                //        {
                //            enemyBull.RemoveAt(x);
                //            BeeRec.RemoveAt(i);
                //            score += 100;
                //            break;
                //        }
                //    }
                //    for (int i = 0; i < BossRec.Count; i++)
                //    {
                //        if (tempRecangle.Intersects(BossRec.ElementAt(i)))
                //        {
                //            enemyBull.RemoveAt(x);
                //            BossRec.RemoveAt(i);
                //            score += 1000;
                //            break;
                //        }
                //    }
                

            }
            
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
            spriteBatch.DrawString(homefont, "High Score: ", new Vector2(150, 10), Color.Red);
            spriteBatch.DrawString(homefont, highscoreNum.ToString(), new Vector2(275, 10), Color.White);
            //Draws the home screen if the player hasn't started a game
            if (homeScreen)
            {
                spriteBatch.Draw(galagaNameArt, name, Color.White);
                spriteBatch.Draw(pointer, arrow, Color.White);
                spriteBatch.DrawString(homefont, "One Player", new Vector2(arrowPos.X + 50, arrowPos.Y), Color.White);
                spriteBatch.DrawString(homefont, "Two Player", new Vector2(arrowPos.X + 50, arrowPos.Y + 50), Color.White);
                
                
            }
            //Otherwise draws the game
            else 
            {
                spriteBatch.DrawString(homefont, "Score: ", new Vector2(20, 10), Color.Red);
                spriteBatch.DrawString(homefont, score .ToString(), new Vector2(100, 10), Color.White);
                //For each player loop through once and draw ship
                for (int x = 0; x < playerXLocs.GetLength(0); x++)
                {
                    Rectangle shipRectangle = new Rectangle(playerXLocs[x], 570, 50, 50);
                    Texture2D tempShipTexture = spaceShipTexture;
                    //for player 2 change ship color
                    if (x == 1)
                    {
                        tempShipTexture = spaceShip2Texture;
                    }
                    //Draw the ship
                    spriteBatch.Draw(tempShipTexture, shipRectangle, Color.White);
                    for(int i = 0; i < shipsLeft; i++)
                    {
                        //spriteBatch.Draw(tempShipTexture, new Rectangle(10 + (50*i), 600, 40, 40), Color.White);
                    }
                }
                //For each bullet,create a rectangle and draw it
                foreach(int[,] bulletCoords in bulletLocations) {
                    Rectangle tempRecangle = new Rectangle(bulletCoords[0, 0], bulletCoords[0, 1], 12, 24);
                    spriteBatch.Draw(bulletTexture,tempRecangle,Color.White);
                }

                //Enemies
                //boss galaga
                for (int i = 0; i < BossRec.Count(); i++) { spriteBatch.Draw(texbg, BossRec[i], Color.White); }
                //Draw all the enemies
                //for (int i=0;i<enemyInfo.Count();i++) {
                //    //Get details about the enemy(location,texture)
                //    List<Object> enemyDetails = (List<Object>)enemyInfo.ElementAt<Object>(i);
                //    //Create a temporary rectangle for the enemy
                //    Rectangle tempEnemyRectangle = (Rectangle)enemyDetails.ElementAt<Object>(0);
                //    //Get the number which represents the texture
                //    int textureInt = (int)enemyDetails.ElementAt<Object>(1);
                //    //Sets the texture from the info given
                //    Texture2D tempEnemyTexture = textureInt == 0 ? texbg : textureInt == 1 ? texrg : texbeg;
                //    spriteBatch.Draw(tempEnemyTexture,tempEnemyRectangle,Color.White);
                //}

                //Enemy bullet: create a rectangle and draw it
                foreach (int[,] bulletCoords in enemyBull)
                {
                    Rectangle tempRecangle = new Rectangle(bulletCoords[0, 0], bulletCoords[0, 1], 12, 24);
                    spriteBatch.Draw(EnemyBullet, tempRecangle, Color.White);
                }


                    for (int i = 0; i < BossRec.Count(); i++)
                {
                    if(brB[i] == true)
                    {
                        spriteBatch.Draw(texbg, BossRec[i], Color.White);
                    }
                }
                //red galaga
                for (int i = 0; i < RedRec.Count(); i++)
                {
                    if (rrB[i] == true)
                    {
                        spriteBatch.Draw(texrg, RedRec[i], Color.White);
                    }
                }
                //bee galaga
                for (int i = 0; i < BeeRec.Count; i++)
                {
                    if (berB[i] == true)
                    {
                        spriteBatch.Draw(texbeg, BeeRec[i], Color.White);
                    }
                }
            }

            
           


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
