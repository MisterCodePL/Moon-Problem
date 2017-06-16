using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GameObjectDestroyer : MonoBehaviour
{
    void LateUpdate()
    {
        DestroyDeathGameObject();
    }

    private void DestroyDeathGameObject()
    {
        var deathGameObjects = FindAllDeathGameObjects();
        foreach (var o in deathGameObjects)
        {
            Destroy(o);
        }
    }

    private List<GameObject> FindAllDeathGameObjects()
    {
        List<GameObject> list = new List<GameObject>();
        try
        {
            list = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            list.Add(GameObject.Find("Player"));
            list = list.Select(x => x)
                .Where(t => t.transform.position.y < -7.5f)
                .ToList();
        }
        catch (NullReferenceException)
        {
            ;
        }
        return list;
    }
}
