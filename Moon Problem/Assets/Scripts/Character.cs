using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float MovementSpeed;
    protected Transform Transform;
    protected Collider2D Collider2D;
    protected Rigidbody2D Rigidbody2D;
    protected bool FacingRight;


    public virtual void Start()
    {
        Transform = gameObject.GetComponent<Transform>();
        Collider2D = gameObject.GetComponent<Collider2D>();
        Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        FacingRight = GetFacingRight();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    protected void Flip()
    {
        FacingRight = !FacingRight;
        var scale = Transform.localScale;
        scale.x *= -1;
        Transform.localScale = scale;
    }

    private void Move()
    {
        var position = Transform.position;
        if (IsMoving() && FacingRight)
        {
            position.x += MovementSpeed;
        }
        if (IsMoving() && !FacingRight)
        {
            position.x -= MovementSpeed;
        }
        Transform.position = position;
    }

    private bool GetFacingRight()
    {
        return Transform.localScale.x > 0;
    }

    protected virtual bool IsMoving()
    {
        return true;
    }

    public virtual void Die()
    {
        Collider2D.isTrigger = true;
        Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.AddForce(Vector2.up*4.5f,ForceMode2D.Impulse);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(collision);
            if (collisionDetector.CollideOnTheRight() != null
                || collisionDetector.CollideOnTheLeft() != null
                || collisionDetector.CollideOnTheBottom() != null)
            {
                collision.gameObject.GetComponent<Character>().Die();
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag=="Enemy")
        {
            var collisionDetector = new CollisionDetector(collision);
            if (collisionDetector.CollideOnTheLeft() != null &&
                (collisionDetector.CollideOnTheLeft().gameObject.tag == "Wall"
                || collisionDetector.CollideOnTheLeft().gameObject.tag=="Enemy")) Flip();
            if (collisionDetector.CollideOnTheRight() != null &&
                (collisionDetector.CollideOnTheRight().gameObject.tag == "Wall"
                || collisionDetector.CollideOnTheRight().gameObject.tag == "Enemy")) Flip();
        }
    }

}
