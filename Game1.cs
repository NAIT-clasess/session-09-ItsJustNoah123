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
    private Texture2D _backgroundTexture, _ballTexture;
    // Ball movement
    private float _ballSpeed = 10;
    private Vector2 _ballPosition, _ballDirection;

    private Rectangle _playAreaBoundingBox;

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
        _ballSpeed = 400;
        _ballDirection.X = 1;
        _ballDirection.Y = -1;

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

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
             // Move Ball
            _ballPosition += _ballDirection * _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        // Bounce Ball off walls by inverting direction
        if (_ballPosition.X <= _playAreaBoundingBox.Left || _ballPosition.X >= _playAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }   
        if (_ballPosition.Y <= _playAreaBoundingBox.Top || _ballPosition.Y >= _playAreaBoundingBox.Bottom)
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
        var ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        
        _spriteBatch.Draw(_ballTexture, ballRect, Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
