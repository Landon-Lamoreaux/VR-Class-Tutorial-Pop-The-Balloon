using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public int number = 4;
    public GameObject sphere;
    public GameObject Cylinder;
    public GameObject Box;
    public GameObject RedBall;

    private Bounds bounds;
    private GameObject[] spawnedObjects;

    public GameEvent game;

    void Start()
    {
        // Cannot spawn if not item is set.
        if (sphere != null)
        {
            // Set up bounding box using coluder if availible, and transform if not.
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                bounds = collider.bounds;
            }
            else
            {
                bounds = new Bounds(transform.position, transform.localScale);
            }

            // Make a new object.
            spawnedObjects = new GameObject[number + number + (number/2) + (number/4)];
            for (int i = 0; i < number; i++)
            {
                spawnedObjects[i] = Instantiate(sphere, new Vector3(0, 0, 0), Quaternion.identity);

                spawnedObjects[i] = game.GetScript(spawnedObjects[i]);
            }


            for (int i = number; i < number*2; i++)
            {
                spawnedObjects[i] = Instantiate(Cylinder, new Vector3(0, 0, 0), Quaternion.identity);

                spawnedObjects[i] = game.GetScriptCylinder(spawnedObjects[i]);
            }


            for (int i = number*2; i < number*2 + number/2; i++)
            {
                spawnedObjects[i] = Instantiate(Box, new Vector3(0, 0, 0), Quaternion.identity);

                spawnedObjects[i] = game.GetScriptBox(spawnedObjects[i]);
            }


            for (int i = number * 2 + number / 2; i < spawnedObjects.Length; i++)
            {
                spawnedObjects[i] = Instantiate(RedBall, new Vector3(0, 0, 0), Quaternion.identity);

                spawnedObjects[i] = game.GetScriptBall(spawnedObjects[i]);
            }
            placeRandomly();

        }
        else
        {
            Debug.Log("No spawn item set");
        }
    }

    public void placeRandomly()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            // Get a random location.
            float radius = spawnedObjects[i].transform.localScale.x / 2;
            float x = Random.Range(bounds.min.x + radius, bounds.max.x - radius);
            float y = Random.Range(bounds.min.y + radius, bounds.max.y - radius);
            float z = Random.Range(bounds.min.z + radius, bounds.max.z - radius);

            // Place as a inside of the play area.
            spawnedObjects[i].transform.position = new Vector3(x, y, z);
            spawnedObjects[i].transform.parent = transform;
        }
    }

    public void Reset()
    {
        if (spawnedObjects != null)
        {
            for (int i = 0; i < spawnedObjects.Length; i++)
            {
                spawnedObjects[i].SetActive(true);
            }

            placeRandomly();
        }
    }


}
