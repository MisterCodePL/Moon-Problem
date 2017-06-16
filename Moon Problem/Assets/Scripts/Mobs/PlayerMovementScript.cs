using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : Character {

    public float JumpForce = 12.5f;
    private Animator _animator;
    private bool _canJump = true;
    public bool CanControl = true;

    public override void Start()
    {
        base.Start();
        _animator = gameObject.GetComponent<Animator>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && FacingRight && CanControl) Flip();
        if(Input.GetKey(KeyCode.RightArrow) && !FacingRight && CanControl) Flip();
        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && CanControl) _animator.SetBool("move", true);
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("move", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Jump();
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        var collisionDetector = new CollisionDetector(collision);
        if (collisionDetector.CollideOnTheBottom() != null && collisionDetector.CollideOnTheBottom().tag != "Wall")
        {
            _canJump = true;
        }

        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<CyclonScript>()==null)
        {
            if (collisionDetector.CollideOnTheBottom()!=null)
            {
                collision.gameObject.GetComponent<Character>().Die();
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _canJump && CanControl)
        {
            Rigidbody2D.AddForce(transform.up*JumpForce,ForceMode2D.Impulse);
            var velocity = Rigidbody2D.velocity;
            if(velocity.y>10) velocity.y = 10;
            Rigidbody2D.velocity = velocity;
            _canJump = false;
        }

    }

    protected override bool IsMoving()
    {
        return (Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow)) && CanControl;
    }

    public override void Die()
    {
        base.Die();
        CanControl = false;
    }
}
