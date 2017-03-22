using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovementScript : MonoBehaviour {

    public float MovementSpeed = 0.125f;
    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public bool _facingRight = false;
    // Use this for initialization
    void Start ()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        Move();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }

    private void Move()
    {
        Vector3 position = _transform.position;
        if (_facingRight)
        {
            position.x += MovementSpeed;
        }
        if (!_facingRight)
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
    }
}
