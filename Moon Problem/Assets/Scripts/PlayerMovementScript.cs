using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : Character {

    public float JumpForce = 12.5f;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _canJump = true;

    public override void Start()
    {
        base.Start();
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && FacingRight) Flip();
        if(Input.GetKey(KeyCode.RightArrow) && !FacingRight) Flip();
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("move", true);
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("move", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Jump();
        if (Transform.position.y < -20)
        {
            Restart();
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {

        var collisionDetector = new CollisionDetector(Collider2D);
        if (collisionDetector.CollideOnTheBottom() != null) _canJump = true;

        if (collision.gameObject.tag == "Respawn")
        {
            Restart();
        }

        if (collision.gameObject.tag == "Enemy")
        {
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
            var velocity = _rigidbody2D.velocity;
            if(velocity.y>10) velocity.y = 10;
            _rigidbody2D.velocity = velocity;
            //TODO: FixBugWithToBigJumps
            _canJump = false;
        }

    }

    protected override bool IsMoving()
    {
        return Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Demo");
    }
}
