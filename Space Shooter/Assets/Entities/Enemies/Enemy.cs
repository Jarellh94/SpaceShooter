using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    public GameObject explosion;
    public int rangeMax = 25, rangeMin = 1;
    public float projectileSpeed = 5f;
    public float shootInterval = 2f;
    public float health = 150;
    public int scoreValue = 150;
    public AudioClip shoot;
    public AudioClip die;

    float timer = 0;
    ScoreKeeper keeper;

    // Use this for initialization
    void Start () {
        keeper = GameObject.FindObjectOfType<ScoreKeeper>();
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
        AudioSource.PlayClipAtPoint(shoot, transform.position);
        Vector3 startPosition = transform.position + new Vector3(0, -0.5f, 0);
        GameObject beam = (GameObject)Instantiate(projectile, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed); //Initiates projectile movement
    }

    void OnTriggerEnter2D(Collider2D oth)
    {

        Projectile proj = oth.gameObject.GetComponent<Projectile>();

        if (proj)
        {
            Damage(proj.GetDamage());
            proj.Hit();
        }
    }

    void Damage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(die, transform.position);
            keeper.Score(scoreValue);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
