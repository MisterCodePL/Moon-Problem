using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private List<GameObject> _enemies;

    private Transform _playerTransform;

    private void Start()
    {
        FindEnemies();
        _playerTransform = GetComponent<Transform>();
    }

    private void FindEnemies()
    {
        var gameObjectsList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        _enemies = new List<GameObject>();
        foreach (GameObject o in gameObjectsList)
        {
            _enemies.Add(o);
        }
        _enemies = _enemies.Where(o => o.tag == "Enemy")
            .Where(o => o.activeSelf==false)
            .ToList();
    }

    private void Update()
    {
        foreach(GameObject o in _enemies.ToList())
        {
            if(Vector2.Distance(_playerTransform.position,o.transform.position)<15)
            {
                o.SetActive(true);
                _enemies.Remove(o);
            }
        }
    }
}
