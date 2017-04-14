using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EndermanScript : MonoBehaviour
{
    public float MovementSpeed = 0.125f;
    private Transform _transform;
    private Animator _animator;
    private Collider2D _collider2D;
    public bool FacingRight = false;
    public float ReloadTime = 0.5f;
    private float _actualReloadTime = 0;
    public GameObject bulletPrefab;
    public float BulletForce = 10f;
    private GameObject _player;

    public void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<Collider2D>();
        _player = GameObject.Find("Player");
    }

    public void FixedUpdate()
    {
        Move();
        PhysicalElementOfDeathAnimation();
    }

    private void Move()
    {
        var position = _transform.position;
        if(FacingRight) position.x += MovementSpeed;
        if (!FacingRight) position.x -= MovementSpeed;
        _transform.position = position;
    }

    private void PhysicalElementOfDeathAnimation()
    {
        RotateGameObject();
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        if (_transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateGameObject()
    {
        if (_collider2D.isTrigger)
        {
            var vector = new Vector2(0.125f / 2, 0.125f / 2);
            _transform.Rotate(vector, 5f);
        }
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

    private void Flip()
    {
        FacingRight = !FacingRight;
        var scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var collisionDetector = new CollisionDetector(_collider2D);
            if (collisionDetector.CollideOnTheTop() != null &&
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") Die();
        }
    }

    private void Die()
    {
        DeathAnimation();
        _collider2D.isTrigger = true;
    }

    private void DeathAnimation()
    {
        var scale = _transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        _transform.localScale = scale;
    }

    public void Update()
    {
        _actualReloadTime += Time.fixedDeltaTime;
        if (_actualReloadTime >= ReloadTime && !_collider2D.isTrigger && IsPlayerInFrontOf())
        {
            _actualReloadTime = 0;
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                StartingBooletPosition(), _transform.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = GetBulletForce(bullet);
            Destroy(bullet, 2.0f);
        }
    }

    private Vector3 StartingBooletPosition()
    {
        var position = _transform.position;
        position.y += _collider2D.bounds.extents.y*1.75f;
        if (FacingRight) position.x += _collider2D.bounds.extents.x + 0.5f;
        else position.x -= _collider2D.bounds.extents.x + 0.5f;
        return position;
    }

    private Vector3 GetBulletForce(GameObject bullet)
    {
        if (FacingRight) return bullet.transform.right * BulletForce;
        return bullet.transform.right * -BulletForce;
    }

    private bool IsPlayerInFrontOf()
    {
        var playerPosition = _player.transform.position;
        var playerBounds = _player.GetComponent<Collider2D>().bounds;
        var endermanPosition = _transform.position;
        var endermanBounds = _collider2D.bounds;
        var isOnTheSameVerticalPosition =
            (playerPosition.y > endermanPosition.y && playerPosition.y < endermanPosition.y + endermanBounds.max.y) ||
            (playerPosition.y + playerBounds.max.y > endermanPosition.y && playerPosition.y + playerBounds.max.y <
             endermanPosition.y + endermanBounds.max.y);
        var isInFrontOf = (FacingRight && playerPosition.x > endermanPosition.x) ||
                           (!FacingRight && playerPosition.x < endermanPosition.x);
        return isOnTheSameVerticalPosition && isInFrontOf;
    }

}

