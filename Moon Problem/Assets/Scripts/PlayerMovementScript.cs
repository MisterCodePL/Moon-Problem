using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour {
    public float MovementSpeed = 0.125f;
    public float JumpForce = 30f;
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
        if (_transform.position.y < -20)
        {
            Restart();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 position= _transform.position;
            position.y -= 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.001f);
            if (hit.collider != null) _canJump = true;
        }

        if (collision.gameObject.tag == "Respawn")
        {
            Restart();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 positionLeft = _transform.position;
            positionLeft.y += 0.5f;
            positionLeft.x -= 0.7f;
            RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, Vector2.left, 0.001f);
            if (hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Enemy") Restart();

            Vector3 positionRight = _transform.position;
            positionRight.y += 0.5f;
            positionRight.x += 0.7f;
            RaycastHit2D hitRight = Physics2D.Raycast(positionRight, Vector2.right, 0.001f);
            if (hitRight.collider != null && hitRight.collider.gameObject.tag == "Enemy") Restart();

            Vector3 positionTop = _transform.position;
            positionTop.y += 2.6f;
            RaycastHit2D hitTop = Physics2D.Raycast(positionTop, Vector2.up, 0.001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Enemy") Restart();
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
