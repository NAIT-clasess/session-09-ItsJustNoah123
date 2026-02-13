using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTemplate;

public class Paddle
{

    private Texture2D _texture;
    private Vector2 _position;
    private Rectangle _playAreaBoundingBox;
    private const float _speed = 300f;

    private Vector2 _direction;
    private Rectangle _rect;

    private const float _width = 15f;
    private const float _height = 100f;
     private Color _color;

     // Rectangle property for drawing and collision
    public Rectangle Rectangle => new Rectangle((int)_position.X, (int)_position.Y, (int)_width, (int)_height);

    internal void Initialize(Vector2 initialPosition, Rectangle playAreaBoundingBox, Color color)
    {
        _position = initialPosition;
        _playAreaBoundingBox = playAreaBoundingBox;
        _color = color;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }

    internal void Update(GameTime gameTime, Vector2 direction)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Move the paddle
        _position += direction * _speed * dt;

        // Clamp within the play area (Top and Bottom only)
        if (_position.Y <= _playAreaBoundingBox.Top)
        {
            _position.Y = _playAreaBoundingBox.Top;
        }
        else if (_position.Y + _height >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _height;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Rectangle, _color);
    }
}