using System;
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

    public void Update()
    {
        try
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
        catch (Exception)
        {
            ;
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

