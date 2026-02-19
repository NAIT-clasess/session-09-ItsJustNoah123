using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTemplate;
public class Ball
{
    private const int _widthAndHeight = 21;
    private Vector2 _position, _direction;
    private float _speed = 200f;
    private Texture2D _texture;
    private Color _color;

    internal void Initialize(Vector2 intialPosition, Vector2 intialDirection)
    {
        _position = intialPosition;
        _direction = intialDirection;
    }  
    internal void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += _direction * _speed * dt;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }
    internal bool ProcessCollision(Rectangle otherBoundingBox)
    {
        bool didCollide = false;
        if(_collisionTimerMillis >= _CollisionTimerIntervalMillis && BoundingBox.Intersects(otherBoundingBox))
        {
            didCollide = true;
            _collisionTimerMillis = 0;
            //collision!!
            Rectangle intersection = Rectangle.Intersect(BoundingBox, otherBoundingBox);
            if(intersection.Width > intersection.Height)
            {
                //this is a horizontal rectangle, therefore it's a top or bottom collision
                _direction.Y *= -1;
            }
            else
            {
                //this is a vertical rectangle, therefore it's a side collision
                _direction.X *= -1;
            }
        }
        return didCollide;
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, BoundingBox, _color);
    }
}
