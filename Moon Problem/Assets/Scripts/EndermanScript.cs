using UnityEngine;

public class EndermanScript : Character
{
    public float ReloadTime = 0.5f;
    private float _actualReloadTime = 0;
    public GameObject BulletPrefab;
    public float BulletForce = 10f;
    private GameObject _player;

    public override void Start()
    {
        base.Start();
        _player = GameObject.Find("Player");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        PhysicalElementOfDeathAnimation();
    }

    protected override bool IsMoving()
    {
        return true;
    }

    private void PhysicalElementOfDeathAnimation()
    {
        RotateGameObject();
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        if (Transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateGameObject()
    {
        if (Collider2D.isTrigger)
        {
            var vector = new Vector2(0.125f / 2, 0.125f / 2);
            Transform.Rotate(vector, 5f);
        }
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
                collisionDetector.CollideOnTheTop().gameObject.tag == "Player") Die();
        }
    }

    private void Die()
    {
        DeathAnimation();
        Collider2D.isTrigger = true;
    }

    private void DeathAnimation()
    {
        var scale = Transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        Transform.localScale = scale;
    }

    public void Update()
    {
        _actualReloadTime += Time.fixedDeltaTime;
        if (_actualReloadTime >= ReloadTime && !Collider2D.isTrigger && IsPlayerInFrontOf())
        {
            _actualReloadTime = 0;
            var bullet = (GameObject)Instantiate(
                BulletPrefab,
                StartingBooletPosition(), Transform.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = GetBulletForce(bullet);
            Destroy(bullet, 2.0f);
        }
    }

    private Vector3 StartingBooletPosition()
    {
        var position = Transform.position;
        position.y += Collider2D.bounds.extents.y*1.75f;
        if (FacingRight) position.x += Collider2D.bounds.extents.x + 0.5f;
        else position.x -= Collider2D.bounds.extents.x + 0.5f;
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
        var endermanPosition = Transform.position;
        var endermanBounds = Collider2D.bounds;
        var isOnTheSameVerticalPosition =
            (playerPosition.y > endermanPosition.y && playerPosition.y < endermanPosition.y + endermanBounds.max.y) ||
            (playerPosition.y + playerBounds.max.y > endermanPosition.y && playerPosition.y + playerBounds.max.y <
             endermanPosition.y + endermanBounds.max.y);
        var isInFrontOf = (FacingRight && playerPosition.x > endermanPosition.x) ||
                           (!FacingRight && playerPosition.x < endermanPosition.x);
        return isOnTheSameVerticalPosition && isInFrontOf;
    }

}

