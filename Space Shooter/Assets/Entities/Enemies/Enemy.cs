using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    public int rangeMax = 25, rangeMin = 1;
    public float projectileSpeed = 5f;
    public float shootInterval = 2f;
    public float health = 150;

    float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(timer >= 0)
            timer -= Time.deltaTime;

        int ranNum = Random.Range(rangeMin, rangeMax);

        if(ranNum == 10 && timer <= 0)
        {
            Fire();

            timer = shootInterval;
        }
	}

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -0.5f, 0);
        GameObject beam = (GameObject)Instantiate(projectile, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed); //Initiates projectile movement
    }

    void OnTriggerEnter2D(Collider2D oth)
    {

        Projectile proj = oth.gameObject.GetComponent<Projectile>();

        if (proj && proj.gameObject.tag == "PlayerProjectile")
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
