using UnityEngine;

public class SpiderMovementScript : Character
{	

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(collision);
            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            var collisionDetector = new CollisionDetector(collision);
            if (collisionDetector.CollideOnTheLeft() != null &&
                collisionDetector.CollideOnTheLeft().gameObject.tag == "Wall") Flip();
            if (collisionDetector.CollideOnTheRight() != null && 
                collisionDetector.CollideOnTheRight().gameObject.tag == "Wall") Flip();
        }
    }

    protected override bool IsMoving()
    {
        return true;
    }
}
