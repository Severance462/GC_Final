using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GC_Final
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera camera;
        Arena arena;
        Maze maze;
        Skybox skybox;
        BasicEffect effect;
        SamplerState clampTextureAddressMode;
        Texture2D texture;
        ModelManager modelManager;
        Texture2D crosshairTexture;
        Texture2D controlsOverlayTexture;

        public Random rnd { get; protected set; }
        float moveScale = 4.5f;
        float rotateScale = MathHelper.PiOver2 + 1;

        float shotSpeed = 1;
        int shotDelay = 300;
        int shotCountdown = 0;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            rnd = new Random();

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            #if !DEBUG
                graphics.IsFullScreen = True;
            #endif
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera(new Vector3(0.5f, 0.5f, 0.5f), 0, GraphicsDevice.Viewport.AspectRatio, 0.05f, 100f);
            effect = new BasicEffect(GraphicsDevice);
            // TODO: Add your initialization logic here

            maze = new GC_Final.Maze(GraphicsDevice, Content.Load<Texture2D>(@"tex\MARBLE3"));
            //arena = new Arena(GraphicsDevice, Content.Load<Texture2D>(@"tex\skyboxPlaza"), 100, 0);
            skybox = new Skybox(GraphicsDevice, Content.Load<Texture2D>(@"tex\skyboxHell2"));

            modelManager = new ModelManager(this);
            Components.Add(modelManager);

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

            clampTextureAddressMode = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp
            };

            texture = Content.Load<Texture2D>(@"tex\FLOOR6_2");
            crosshairTexture = Content.Load<Texture2D>(@"textures\crosshair");
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

            // TODO: Add your update logic here

            FireShots(gameTime);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            float moveAmount = 0;


            if (keyState.IsKeyDown(Keys.Q))
            {
                camera.Rotation = MathHelper.WrapAngle(camera.Rotation + (rotateScale * elapsed));
                //moveAmount = moveScale * elapsed;
            }

            if (keyState.IsKeyDown(Keys.E))
            {
                camera.Rotation = MathHelper.WrapAngle(camera.Rotation - (rotateScale * elapsed));
                //moveAmount = moveScale * elapsed;
            }


            if (keyState.IsKeyDown(Keys.D))
            {
                //camera.Rotation = MathHelper.WrapAngle(camera.Rotation - (rotateScale * elapsed));
                moveAmount = moveScale * elapsed;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                //camera.Rotation = MathHelper.WrapAngle(camera.Rotation + (rotateScale * elapsed));
                moveAmount = moveScale * elapsed;
            }
            if (keyState.IsKeyDown(Keys.W))
            {
                //camera.MoveForward(moveScale * elapsed);
                moveAmount = moveScale * elapsed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                //camera.MoveForward(-moveScale * elapsed);
                moveAmount = -moveScale * elapsed;
            }

            if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.W))
            {
                moveAmount = moveScale * elapsed;
            }


            //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    modelManager.AddShot(camera.cameraPosition + new Vector3(0, -5, -1), camera.GetCameraDirection * shotSpeed);
            //    //PlayCue("Shot");

            //    shotCountdown = shotDelay;
            //}
            if (moveAmount != 0)
            {
                Vector3 newLocation = camera.PreviewMove(moveAmount);

                if (keyState.IsKeyDown(Keys.A) || (keyState.IsKeyDown(Keys.D)))
                {
                    newLocation = camera.PreviewStrafe(moveAmount);
                }
                
                bool moveOk = true;
                if (newLocation.X < 0 || newLocation.X > Maze.mazeWidth)
                    moveOk = false;
                if (newLocation.Z < 0 || newLocation.Z > Maze.mazeHeight)
                    moveOk = false;

                foreach (BoundingBox box in maze.GetBoundsForCell((int)newLocation.X, (int)newLocation.Z))
                {
                    if (box.Contains(newLocation) == ContainmentType.Contains)
                        moveOk = false;
                }

                if (moveOk)
                {
                    if (keyState.IsKeyDown(Keys.A))
                    {
                        //camera.Rotation = MathHelper.WrapAngle(camera.Rotation + (rotateScale * elapsed));
                        camera.MoveStrafe(moveAmount);
                    }
                    else if (keyState.IsKeyDown(Keys.D))
                    {
                        //camera.Rotation = MathHelper.WrapAngle(camera.Rotation - (rotateScale * elapsed));
                        camera.MoveStrafe(-moveAmount);
                    }
                    else if (keyState.IsKeyDown(Keys.Q))
                    {
                        camera.Rotation = MathHelper.WrapAngle(camera.Rotation + (rotateScale * elapsed));
                        //camera.MoveForward(moveAmount);
                    }
                    else if (keyState.IsKeyDown(Keys.E))
                    {
                        camera.Rotation = MathHelper.WrapAngle(camera.Rotation - (rotateScale * elapsed));
                        camera.MoveForward(moveAmount);
                    }
                    else if (keyState.IsKeyDown(Keys.Space))
                    {
                        camera.MoveJump(moveAmount);
                    }
                    else
                        camera.MoveForward(moveAmount);
                }
            }

            //if (cube.Bounds.Contains(camera.Position) == ContainmentType.Contains)
            //{
            //    cube.PositionCube(camera.Position, 5f);
            //    float thisTime = (float)gameTime.TotalGameTime.TotalSeconds;
            //    float scoreTime = thisTime - lastScoreTime;
            //    score += 1000;
            //    if (scoreTime < 120)
            //    {
            //        score += (120 - (int)scoreTime) * 100;
            //    }
            //    lastScoreTime = thisTime;
            //}

            //cube.Update(gameTime);

            //if (gift.Bounds.Contains(camera.Position) == ContainmentType.Contains)
            //{
            //    gift.PositionGift(camera.Position, 5f);
            //    float thisTime = (float)gameTime.TotalGameTime.TotalSeconds;
            //    float scoreTime = thisTime - lastScoreTime;
            //    score += 1000;
            //    if (scoreTime < 120)
            //    {
            //        score += (120 - (int)scoreTime) * 100;
            //    }
            //    lastScoreTime = thisTime;
            //}

            //gift.Update(gameTime, camera, lastScoreTime, score);
            //FireShots(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            maze.Draw(camera, effect);
            //arena.Draw(camera, effect);
            skybox.Draw(camera, effect);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected void FireShots(GameTime gameTime)
        {
            //If enough time (delay) has passed
            if (shotCountdown <= 0)
            {
                // Did player press space bar or left mouse button?
                if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                    Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    // Add a shot to the model manager
                    modelManager.AddShot(
                        camera.cameraPosition + new Vector3(0, -5, 0),
                        camera.GetCameraDirection * shotSpeed);

                    // Play shot audio
                    //PlayCue("Shot");

                    // Reset the shot countdown to the delay value
                    shotCountdown = shotDelay;
                }
            }
            else
                shotCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
