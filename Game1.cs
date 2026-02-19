using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    // Screen
    private const int _preferredScreenWidth = 750;
    private const int _preferredScreenHeight = 450;
    // Play Area
    private const int _playAreaEdgeLineWidth = 12;
    // Ball
    private Ball _ball;
    // Textures
    private Texture2D _backgroundTexture, _ballTexture;
    private Rectangle _playAreaBoundingBox;
    private Rectangle _ballRect;
    private Paddle _leftPaddle;
    private Paddle _rightPaddle;
    private Vector2 _rightPaddleDirecton;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // Screen Size
        _graphics.PreferredBackBufferWidth = _preferredScreenWidth;
        _graphics.PreferredBackBufferHeight = _preferredScreenHeight;
        // Initial Ball Position and Direction
        _ballPosition.X = 150;
        _ballPosition.Y = 195;
        _ballDirection.X = 1;
        _ballDirection.Y = -1;
        _playAreaBoundingBox = new(0, 0, _preferredScreenWidth, _preferredScreenHeight);
        _leftPaddle = new Paddle();
        _leftPaddle.Initialize(new Vector2(100, 500), _playAreaBoundingBox, Color.White);
        _rightPaddle = new Paddle();
        _rightPaddle.Initialize(new Vector2(600, 500), _playAreaBoundingBox, Color.White);
        // Create Rectangles for Ball and Paddle
        _ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        
        
        // Apply Graphics changes
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _leftPaddle.LoadContent(Content);
        _rightPaddle.LoadContent(Content);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kbstate = Keyboard.GetState();
        _rightPaddleDirecton = Vector2.Zero;
        if (kbstate.IsKeyDown(Keys.Up))
        {
            _rightPaddleDirecton = new Vector2(0, -1);
        }
        else if (kbstate.IsKeyDown(Keys.Down))
        {
            _rightPaddleDirecton = new Vector2(0, 1);
        
        }
        _rightPaddle.Update(gameTime, _rightPaddleDirecton);
        Vector2 _leftPaddleDirection = Vector2.Zero;
        // Move Paddle
        if (kbstate.IsKeyDown(Keys.W))
        {
            _leftPaddleDirection = new Vector2(0, -1);

        }
        else if (kbstate.IsKeyDown(Keys.S))
        {
            _leftPaddleDirection = new Vector2(0, 1);

        }
        else
        {
            _leftPaddleDirection = Vector2.Zero;
        }
        // Move Paddle
        _leftPaddle.Update(gameTime, _leftPaddleDirection);
        // Move Ball
        Ball
        _ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        // Bounce Ball off walls by inverting direction
        if (_ballRect.Left <= _playAreaBoundingBox.Left || _ballRect.Right >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }   
        if (_ballRect.Top <= _playAreaBoundingBox.Top || _ballRect.Bottom >= _playAreaBoundingBox.Bottom)
        {
            _ballDirection.Y *= -1;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _preferredScreenWidth, _preferredScreenHeight), Color.White);
        _spriteBatch.Draw(_ballTexture, _ballRect, Color.White);
        _rightPaddle.Draw(_spriteBatch);
        _leftPaddle.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
