using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovementScript : MonoBehaviour {

    public float MovementSpeed = 0.125f;
    private Transform _transform;
    private Animator _animator;
    private Collider2D _collider2D;
    public bool FacingRight = false;
    // Use this for initialization
    void Start ()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        Move();
        if (_collider2D.isTrigger)
        {
            Vector2 vector = new Vector2(0.125f/2,0.125f/2);
            _transform.Rotate(vector,5f);
        }
        if (_transform.position.y <= -5f)
        {
            Destroy(gameObject);
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collide")
        {
            Vector3 position = _transform.position;
            position.y += 1f;
            position.x -= 2f;
            RaycastHit2D hitLeft = Physics2D.Raycast(position, Vector2.left, 0.001f);
            if (hitLeft.collider != null && hitLeft.collider.gameObject.tag != "Player") Flip();
            position.x += 4f;
            RaycastHit2D hitRight = Physics2D.Raycast(position, Vector2.right, 0.001f);
            if (hitRight.collider != null && hitRight.collider.gameObject.tag != "Player") Flip();
        }

        if (collision.gameObject.tag == "Player")
        {
            Vector3 position = _transform.position;
            position.y += 3f;
            RaycastHit2D hitTop = Physics2D.Raycast(position, Vector2.up, 0.001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
            position.x += 1f;
            hitTop = Physics2D.Raycast(position, Vector2.up, 0.001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
            position.x -= 2f;
            hitTop = Physics2D.Raycast(position, Vector2.up, 0.001f);
            if (hitTop.collider != null && hitTop.collider.gameObject.tag == "Player") Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("isDeath");
        Vector3 scale = _transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        _transform.localScale = scale;
        _collider2D.isTrigger = true;
    }
}
