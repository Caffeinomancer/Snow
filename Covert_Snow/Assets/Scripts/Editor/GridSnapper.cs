using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : MonoBehaviour
{

    public float snapValue = 0.5f;
    public float depth = 0;


    void Start()
    {

    }

    void Update()
    {
        float snapInverse = 1 / snapValue;

        float x, y, z;

        x = Mathf.Round(transform.position.x * snapInverse) / snapInverse;
        y = Mathf.Round(transform.position.y * snapInverse) / snapInverse;
        z = depth;

        transform.position = new Vector3(x, y, z);

        Debug.Log("TEST");
    }
}

