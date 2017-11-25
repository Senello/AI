using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollector : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (GameController.Instace.GetNextBlock())
        {
            GameObject[] DetachedCubes = GameObject.FindGameObjectsWithTag("Cube");
            foreach (GameObject cube in DetachedCubes)
            {
                cube.transform.parent = this.transform;
            }
        }
    }

    public void KillChildrenWithPositionY(int y)
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (Convert.ToInt32(child.position.y) == y)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in children)
        {
            if (Convert.ToInt32(child.position.y) > y)
            {
                child.transform.position = new Vector3(child.position.x,child.position.y -1,child.position.z);
            }
        }
    }
}