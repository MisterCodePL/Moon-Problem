using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour {
    public float MovementSpeed = 0.125f;
    public float JumpForce = 12.5f;
    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private bool _canJump = true;
    private Collider2D _collider2D;

    public void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && _facingRight) Flip();
        if(Input.GetKey(KeyCode.RightArrow) && !_facingRight) Flip();
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("move", true);
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("move", false);
    }

    public void FixedUpdate()
    {
        Move();
        Jump();
        if (_transform.position.y < -20)
        {
            Restart();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheBottom() != null) _canJump = true;
        }

        if (collision.gameObject.tag == "Respawn")
        {
            Restart();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheLeft() != null && 
                collisionDetector.CollideOnTheLeft().gameObject.tag == "Enemy") Restart();

            if (collisionDetector.CollideOnTheRight() != null && 
                collisionDetector.CollideOnTheRight().gameObject.tag == "Enemy") Restart();

            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Enemy") Restart();
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _canJump)
        {
            _rigidbody2D.AddForce(transform.up*JumpForce,ForceMode2D.Impulse);
            _canJump = false;
        }

    }

    private void Move()
    {
        Vector3 position = _transform.position;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += MovementSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= MovementSpeed;
        }
        _transform.position = position;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }

    private void Restart()
    {
        SceneManager.LoadScene("Demo");
    }
}
