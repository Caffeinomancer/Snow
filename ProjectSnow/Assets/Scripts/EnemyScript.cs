using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector3 spawnLocation;
    private Vector3 destinationOne;
    private Vector3 destinationTwo;

    private bool movingToOne = true;

    private float startTime;
    private float journeyLength;

    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = transform.position;
        destinationOne = new Vector3(spawnLocation.x + 20, spawnLocation.y, spawnLocation.z);
        destinationTwo = new Vector3(spawnLocation.x - 20, spawnLocation.y, spawnLocation.z);

        startTime = Time.time;
        journeyLength = Vector3.Distance(spawnLocation, destinationOne);
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    private void Travel()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        if(movingToOne)
        {
            if (transform.position.x >= destinationOne.x)
            {
                movingToOne = false;
                spawnLocation = destinationOne;
                journeyLength = Vector3.Distance(spawnLocation, destinationTwo);
                startTime = Time.time;
            }

            else
            {
                transform.position = Vector3.Lerp(spawnLocation, destinationOne, fractionOfJourney);
                Debug.Log("Moving Right");
            }
        }

        else
        {
            if(transform.position.x <= destinationTwo.x)
            {
                movingToOne = true;
                spawnLocation = destinationTwo;
                journeyLength = Vector3.Distance(spawnLocation, destinationOne);
                startTime = Time.time;

            }

            else
            {
                transform.position = Vector3.Lerp(spawnLocation, destinationTwo, fractionOfJourney);
                Debug.Log("Moving Left");
            }
        }
    }
}
