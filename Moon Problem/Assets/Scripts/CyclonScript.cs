using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclonScript : MonoBehaviour
{

    private float _actualSpeed;
    public float MovementSpeed = 0.125f;
    public float DeffendModeSpeed = 0.250f;
    public float DeffendModeTime = 60f;
    private float _deffendModeTime = 0;
    public Sprite DefaultSprite;
    public Sprite DeffendSprite;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    public bool FacingRight = false;
    public void Start ()
    {
        _actualSpeed = MovementSpeed;
        _transform = gameObject.GetComponent<Transform>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }

    public void FixedUpdate()
    {
        Move();
        if (_deffendModeTime >= DeffendModeTime)
        {
            BackToDefaultMode();
        }
        Die();
    }

    private void BackToDefaultMode()
    {
        _spriteRenderer.sprite = DefaultSprite;
        _actualSpeed = MovementSpeed;
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") DeffendMove();
        }
    }

    private void DeffendMove()
    {
        _spriteRenderer.sprite = DeffendSprite;
        _actualSpeed = DeffendModeSpeed;
        _deffendModeTime = 0;
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
            position.x += _actualSpeed;
        }
        if (!FacingRight)
        {
            position.x -= _actualSpeed;
        }
        _transform.position = position;
    }

	void Update ()
	{
	    _deffendModeTime+=Time.fixedDeltaTime;
	}

    private void Die()
    {
        if (_transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }
}
