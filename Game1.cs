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
    private const int _ballWidthAndHeight = 21;
    // Textures
    private Texture2D _backgroundTexture, _ballTexture, _paddleTexture;
    // Ball movement
    private float _ballSpeed = 200f;
    private float _paddleSpeed = 300f;
    private Vector2 _ballPosition, _ballDirection, _paddlePosition;
    private Vector2 _paddleDirection;

    private Rectangle _playAreaBoundingBox;
    private Rectangle _ballRect, _paddleRect;

    private const float _paddleWidth = 15f;
    private const float _paddleHeight = 100f;

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
        //paddle position
        _paddlePosition.X = 50;
        _paddlePosition.Y = 175;
        _paddleDirection = Vector2.Zero;
        // Create Rectangles for Ball and Paddle
        _ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        _paddleRect = new Rectangle((int)_paddlePosition.X, (int)_paddlePosition.Y, (int)_paddleWidth, (int)_paddleHeight);
        _playAreaBoundingBox = new(0, 0, _preferredScreenWidth, _preferredScreenHeight);
        // Apply Graphics changes
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kbstate = Keyboard.GetState();
        // Move Paddle
        if (kbstate.IsKeyDown(Keys.W))
        {
            _paddleDirection = new Vector2(0, -1);

        }
        else if (kbstate.IsKeyDown(Keys.S))
        {
            _paddleDirection = new Vector2(0, 1);

        }
        else
        {
            _paddleDirection = Vector2.Zero;
        }
        // Move Paddle
        _paddlePosition += _paddleDirection * _paddleSpeed * dt;
        //Clamp Paddle to play area using the play are bounding box and edge line width to prevent the paddle from going under the edge lines
        _paddlePosition.Y = MathHelper.Clamp(_paddlePosition.Y, _playAreaBoundingBox.Top + _playAreaEdgeLineWidth, _playAreaBoundingBox.Bottom - _playAreaEdgeLineWidth - _paddleHeight);
        // Move Ball
        _ballPosition += _ballDirection * _ballSpeed * dt;
        _ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        _paddleRect = new Rectangle((int)_paddlePosition.X, (int)_paddlePosition.Y, (int)_paddleWidth, (int)_paddleHeight);
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
        _spriteBatch.Draw(_paddleTexture, _paddleRect, Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
