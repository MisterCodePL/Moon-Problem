using UnityEngine;

public class CollisionDetector
{
    private Collider2D _collider2D;

    public CollisionDetector(Collider2D collider2D)
    {
        _collider2D = collider2D;
    }

    public Collider2D CollideOnTheLeft()
    {
        var leftDownCorner = GetLeftDownCorner();
        var size = GetSizeOfObject();

        var oneThird = (size.y-0.2f)/3;
        leftDownCorner.x -= 0.000001f;
        leftDownCorner.y += 0.1f;
        for (var i = 0; i < 3; i++)
        {
            if(i!=0) leftDownCorner.y += oneThird;
            var hit = Physics2D.Raycast(leftDownCorner, Vector2.left, 0.1f);
            if (hit.collider != null) return hit.collider;
        }
        return null;
    }

    public Collider2D CollideOnTheRight()
    {
        var leftDownCorner = GetLeftDownCorner();
        var size = GetSizeOfObject();

        var oneThird = (size.y-0.2f)/3;
        leftDownCorner.x += 0.000001f + size.x;
        leftDownCorner.y += 0.1f;
        for (var i = 0; i < 3; i++)
        {
            if(i!=0)leftDownCorner.y += oneThird;
            var hit = Physics2D.Raycast(leftDownCorner, Vector2.right, 0.1f);
            if (hit.collider != null) return hit.collider;
        }
        
        return null;
    }

    public Collider2D CollideOnTheTop()
    {
        var leftDownCorner = GetLeftDownCorner();
        var size = GetSizeOfObject();

        var oneThird = size.x / 3;
        leftDownCorner.y += 0.1f + size.y;
        for (var i = 0; i < 3; i++)
        {
            if (i != 0) leftDownCorner.x += oneThird;
            var hit = Physics2D.Raycast(leftDownCorner, Vector2.up, 0.1f);
            if (hit.collider != null) return hit.collider;
        }
        return null;
    }

    public Collider2D CollideOnTheBottom()
    {
        var leftDownCorner = GetLeftDownCorner();
        var size = GetSizeOfObject();

        var oneThird = size.x / 3;
        leftDownCorner.y -= 000000.1f;
        for (var i = 0; i < 3; i++)
        {
            leftDownCorner.x += oneThird;
            var hit = Physics2D.Raycast(leftDownCorner, Vector2.down, 0.1f);
            if (hit.collider != null) return hit.collider;
        }
        return null;
    }

    private Vector3 GetLeftDownCorner()
    {
        var leftDownCorner = _collider2D.transform.position;
        leftDownCorner.x -= _collider2D.bounds.extents.x;
        return leftDownCorner;
    }

    private Vector3 GetSizeOfObject()
    {
        return _collider2D.bounds.size;
    }

}
