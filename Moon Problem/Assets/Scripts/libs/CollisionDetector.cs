using UnityEngine;

public class CollisionDetector
{
    private Collision2D _collision2D;

    public CollisionDetector(Collision2D collision2D)
    {
        _collision2D = collision2D;
    }

    public Collider2D CollideOnTheLeft()
    {
        Vector3 hit = _collision2D.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 90))
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y > 0)
            {
                return _collision2D.collider;
            }
        }
        return null;
    }

    public Collider2D CollideOnTheRight()
    {
        Vector3 hit = _collision2D.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 90))
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y < 0)
            {
                return _collision2D.collider;
            }
        }
        return null;
    }

    public Collider2D CollideOnTheTop()
    {
        Vector3 hit = _collision2D.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 180))
        {
            return _collision2D.collider;
        }
        return null;
    }

    public Collider2D CollideOnTheBottom()
    {
        Vector3 hit = _collision2D.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);

        if (Mathf.Approximately(angle, 0))
        {
            return _collision2D.collider;
        }
        return null;
    }

}
