using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Rotate();
        DestroyYourself();
    }

    private void DestroyYourself()
    {
        if (PlayerInRange())
        {
            GameObject.Find("RocksCounter").GetComponent<RocksCounter>().AddRock();
            Destroy(gameObject);
        }
    }

    private bool PlayerInRange()
    {
        Collider2D player = null;
        try
        {
            player = Physics2D.OverlapCircleAll(_transform.position, 0.175f)
                .Select(x => x)
                .First(y => y.gameObject.CompareTag("Player"));
        }
        catch (Exception)
        {
            ;
        }

        return player != null;
    }

    private void Rotate()
    {
        _transform.Rotate(Vector3.back, 5);
    }

}
