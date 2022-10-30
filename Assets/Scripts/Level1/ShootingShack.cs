using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingShack : MonoBehaviour
{
	public GameObject explosion;        // Prefab of explosion effect.
	public Rigidbody2D weapon;              // Prefab of the rocket.
	public float explosionRadius = 5f;          // Radius within which enemies are killed.
	public float explosionForce = 50f;          // Force that enemies are thrown from the blast.
	public AudioClip boom;                  // Audioclip of explosion.
	private ParticleSystem explosionFX;     // Reference to the particle system of the explosion effect.
	public int damage = 4;
	public float bulletSpeed = 20;
	private bool isShooting = true;
	public float ammunition = 10;
	public AudioClip[] gunSound;              

	void Start()
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();

	}

	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		//Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the Bullet is with the random rotation.
		//Instantiate(explosion, transform.position, randomRotation);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        // Otherwise if it hits a Shack...
        if (col.tag == "Bullet")
        {
            // ... find the Enemy script and call the Hurt function.
            //col.gameObject.GetComponent<Shack>().pistolDamage(damage);

            // Call the explosion instantiation.
            //Do not destroy the bullet if the shack has not been destroyed
            if (!col.gameObject.GetComponent<Shack>().shackIsDestroyed)
            {
                OnExplode();
            }
        }

    }

	public void Explode()
	{
		//Destroy(gameObject);
	}

    public void Update()
    {
        // If the fire button is pressed...
        if (isShooting && ammunition > 0)
        {
            // ... set the animator Shoot trigger parameter and play the audioclip.
            //anim.SetTrigger("Shoot");
            //GetComponent<AudioSource>().Play();
            AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
            ammunition--;

            Vector2 bulletSpawnPosition = transform.position;

            if (isShooting)
            {
                Vector3 shackPosition = transform.position;
				//shackPosition.x = shackPosition.x - 100f;
			   // Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(weapon, shackPosition, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2( -1 * bulletSpeed, 0);
            }

        }


        //isShooting = false;
	}

	public void shoot()
	{
		isShooting = true;
	}
}
