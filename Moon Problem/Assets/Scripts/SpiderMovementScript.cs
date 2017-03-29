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
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 position = _transform.position;
            position.y += 1f;
            RaycastHit2D hitTop = Physics2D.Raycast(position, Vector2.up, 0.0001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
            position.x += 1f;
            hitTop = Physics2D.Raycast(position, Vector2.up, 0.0001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
            position.x -= 2f;
            hitTop = Physics2D.Raycast(position, Vector2.up, 0.0001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 position = _transform.position;
            position.y += 1f;
            position.x -= 2f;
            RaycastHit2D hitLeft = Physics2D.Raycast(position, Vector2.left, 0.001f);
            if (hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Ground") Flip();
            position.x += 4f;
            RaycastHit2D hitRight = Physics2D.Raycast(position, Vector2.right, 0.001f);
            if (hitRight.collider != null && hitRight.collider.gameObject.tag == "Ground") Flip();
        }
    }


    private void PhysicalElementOfDeathAnimation()
    {
        RotateSpiderGameObject();
        SpiderGameobjectDestroy();
    }

    private void SpiderGameobjectDestroy()
    {
        if (_transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateSpiderGameObject()
    {
        if (_collider2D.isTrigger)
        {
            Vector2 vector = new Vector2(0.125f / 2, 0.125f / 2);
            _transform.Rotate(vector, 5f);
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }

    private void Move()
    {
        Vector3 position = _transform.position;
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
        _animator.SetTrigger("isDeath");
        Vector3 scale = _transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        _transform.localScale = scale;
    }
}
