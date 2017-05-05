using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawnerScript : MonoBehaviour
{
    public int NumberOfMines = 0;
    public float DelayBetweenMines = 1.0f;
    public GameObject MinePrefab;
    private float _actualDelay = 0;

	void Start ()
    {
		
	}

    void Update()
    {
        var playerPosition = FindPlayerPosition();
        _actualDelay += Time.fixedDeltaTime;
        if (_actualDelay > DelayBetweenMines && NumberOfMines>0 && IsInRange(playerPosition))
        {
            NumberOfMines--;
            _actualDelay = 0;
            Instantiate(MinePrefab,GetSpawnPosition(), gameObject.transform.rotation);
        }
    }

    private Vector2 FindPlayerPosition()
    {
        return GameObject.Find("Player").transform.position;
    }

    private bool IsInRange(Vector2 playerPosition)
    {
        var position = gameObject.transform.position;
        var x = playerPosition.x - position.x;
        if (x < 0) x = -x;
        return x < 7.5f;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 vector2 = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y+1.25f);
        return vector2;
    }

}
