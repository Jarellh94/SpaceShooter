﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float fireRate = .2f;
    public float health = 150f;

    float xMin, xMax, yMin, yMax;
    float padding = 0.5f;

    private Vector3 MoveVector;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;

        //Calculates the xMin and xMax using the camera. x and y values are according to screen coordinates. Bottom left = 0, 0; Top left = 0, 1; Bottom Right = 1, 0; Top Right = 1, 1;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, distance));

        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;

        yMin = leftMost.y + padding;
        yMax = rightMost.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
        Move();
    }

    //Function for moving player
    void Move()
    {
        MoveVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveVector.y += moveSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveVector.x += moveSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveVector.y -= moveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveVector.x -= moveSpeed;
        }

        //Adds movement vector to the current position
        transform.position += MoveVector * Time.deltaTime;

        //Restrict movement to playspace
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.001f, fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 0.5f, 0);
        GameObject beam = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); //Initiates projectile movement
    }

    void OnTriggerEnter2D(Collider2D oth)
    {

        Projectile proj = oth.gameObject.GetComponent<Projectile>();

        if (proj)
        {
            Damage(proj.GetDamage());
            proj.Hit();
            Debug.Log("Hit by Projectile");
        }
    }

    void Damage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
