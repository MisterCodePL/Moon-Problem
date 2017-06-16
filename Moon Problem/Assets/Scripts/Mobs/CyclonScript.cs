using System;
using System.Runtime.InteropServices;
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
    }

    private void BackToDefaultMode()
    {
        _spriteRenderer.sprite = DefaultSprite;
        MovementSpeed = NormalSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" 
            || collision.gameObject.tag == "Enemy" && !IsInDeffendMode())
        {
            IfRightConditionsFlip(collision);
        }
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(collision);
            if (collisionDetector.CollideOnTheTop() != null && 
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") DeffendMove();
        }
        if (collision.gameObject.tag == "Enemy" && IsInDeffendMode())
        {
            IfRightConditionsDie(collision);
        }
    }
    private void DeffendMove()
    {
        _spriteRenderer.sprite = DeffendSprite;
        MovementSpeed = DeffendModeSpeed;
        _deffendModeTime = 0;
    }

    public void Update()
	{
	    _deffendModeTime+=Time.fixedDeltaTime;
	}

    private bool IsInDeffendMode()
    {
        return Math.Abs(MovementSpeed - DeffendModeSpeed) < 0.1f;
    }

}
