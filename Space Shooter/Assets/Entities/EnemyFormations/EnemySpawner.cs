using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;

    public float moveSpeed = 2f;

    int dir = 1;
    float xMax;
    float xMin;

	// Use this for initialization
	void Start () {

        SpawnEnemies();

        float distance = transform.position.z - Camera.main.transform.position.z;
        //Calculates the xMin and xMax using the camera. x and y values are according to screen coordinates. Bottom left = 0, 0; Top left = 0, 1; Bottom Right = 1, 0; Top Right = 1, 1;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));


        //Accounting for bounding box that contains formation.
        xMin = leftMost.x + width/2;
        xMax = rightMost.x - width/2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();

        if(AllMembersDead())
        {
            SpawnEnemies();
        }
	}

    void Move()
    {
        transform.position += Vector3.right * moveSpeed * dir * Time.deltaTime;

        if((transform.position.x >= xMax && dir == 1) || (transform.position.x <= xMin && dir == -1))
        {
            dir *= -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    bool AllMembersDead()
    {

        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
                return false;
        }

        return true;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, child, false);
        }
    }
}
