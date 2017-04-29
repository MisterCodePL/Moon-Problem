using UnityEngine;

public class CyclonScript : Character
{
    public float NormalSpeed = 0.125f;
    public float DeffendModeSpeed = 0.250f;
    public float DeffendModeTime = 60f;
    private float _deffendModeTime = 0;
    public Sprite DefaultSprite;
    public Sprite DeffendSprite;
    private SpriteRenderer _spriteRenderer;
    public override void Start ()
    {
        base.Start();
        MovementSpeed = NormalSpeed;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_deffendModeTime >= DeffendModeTime)
        {
            BackToDefaultMode();
        }
        Die();
    }

    private void BackToDefaultMode()
    {
        _spriteRenderer.sprite = DefaultSprite;
        MovementSpeed = NormalSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            var collisionDetector = new CollisionDetector(Collider2D);
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
            var collisionDetector = new CollisionDetector(Collider2D);
            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") DeffendMove();
        }
    }

    private void DeffendMove()
    {
        _spriteRenderer.sprite = DeffendSprite;
        MovementSpeed = DeffendModeSpeed;
        _deffendModeTime = 0;
    }

    protected override bool IsMoving()
    {
        return true;
    }

    public void Update()
	{
	    _deffendModeTime+=Time.fixedDeltaTime;
	}

    private void Die()
    {
        if (Transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }
}
