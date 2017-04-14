using UnityEngine;

public class SpiderMovementScript : MonoBehaviour {

    public float MovementSpeed = 0.125f;
    private Transform _transform;
    private Animator _animator;
    private Collider2D _collider2D;
    public bool FacingRight = false;
    public void Start ()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }
	
	public void FixedUpdate()
    {
        Move();
        PhysicalElementOfDeathAnimation();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheLeft() != null &&
                collisionDetector.CollideOnTheLeft().gameObject.tag == "Ground") Flip();
            if (collisionDetector.CollideOnTheRight() != null && 
                collisionDetector.CollideOnTheRight().gameObject.tag == "Ground") Flip();
        }
    }


    private void PhysicalElementOfDeathAnimation()
    {
        RotateGameObject();
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        if (_transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateGameObject()
    {
        if (_collider2D.isTrigger)
        {
            var vector = new Vector2(0.125f / 2, 0.125f / 2);
            _transform.Rotate(vector, 5f);
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        var scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }

    private void Move()
    {
        var position = _transform.position;
        if (FacingRight)
        {
            position.x += MovementSpeed;
        }
        if (!FacingRight)
        {
            position.x -= MovementSpeed;
        }
        _transform.position = position;
    }

    private void Die()
    {
        DeathAnimation();
        _collider2D.isTrigger = true;
    }

    private void DeathAnimation()
    {
        var scale = _transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        _transform.localScale = scale;
    }
}
