using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    // Screen
    private const int PerferredScreenWidth = 750;
    private const int PerferredScreenHeight = 450;
    // Play Area
    private const int _playeAreaEdgeLineWidth = 12;
    // Ball
    private const int _ballWidthAndHeight = 21;
    // Textures
    private Texture2D _backgroundTexture, _ballTexture;




    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
    
        _graphics.PreferredBackBufferWidth = PerferredScreenWidth;
        _graphics.PreferredBackBufferHeight = PerferredScreenHeight;
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, PerferredScreenWidth, PerferredScreenHeight), Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
