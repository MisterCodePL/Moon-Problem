using UnityEngine;

public class MovementScript : MonoBehaviour {
    public float MovementSpeed = 0.125f;
    public float JumpForce = 500f;
    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private bool _canJump = true;

    public void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
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
        Restart();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collide")
        {
            Vector3 position= _transform.position;
            position.y -= 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.001f);
            if (hit.collider != null) _canJump = true;
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
        if (_transform.position.y < -20)
        {
            Vector3 position = new Vector3(0,0);
            _transform.position = position;
        }
    }
}
