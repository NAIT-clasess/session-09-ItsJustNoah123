namespace MonoTemplate;
public class Ball
{
    private const int






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
}
