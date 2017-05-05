using UnityEngine;

public class SpiderMovementScript : Character
{	
	public override void FixedUpdate()
    {
        base.FixedUpdate();
        PhysicalElementOfDeathAnimation();
    }

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


    private void PhysicalElementOfDeathAnimation()
    {
        RotateGameObject();
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        if (Transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateGameObject()
    {
        if (Collider2D.isTrigger)
        {
            var vector = new Vector2(0.125f / 2, 0.125f / 2);
            Transform.Rotate(vector, 5f);
        }
    }

    protected override bool IsMoving()
    {
        return true;
    }

    private void Die()
    {
        DeathAnimation();
        Collider2D.isTrigger = true;
    }

    private void DeathAnimation()
    {
        var scale = Transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        Transform.localScale = scale;
    }
}
