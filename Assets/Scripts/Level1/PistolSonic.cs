using UnityEngine;
using System.Collections;

public class PistolSonic : MonoBehaviour
{
	public GameObject explosion;        // Prefab of explosion effect.

	public float explosionRadius = 5f;          // Radius within which enemies are killed.
	public float explosionForce = 50f;          // Force that enemies are thrown from the blast.
	public AudioClip boom;                  // Audioclip of explosion.
	private ParticleSystem explosionFX;     // Reference to the particle system of the explosion effect.
	public int damage = 4;
	public float bulletSpeed = 20;
	void Start()
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();

		// Destroy the rocket after 8 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2.0f);
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
		// If it hits an enemy...
		if (col.tag == "Ground")
		{
			BounceBullet();
			/// ... find the Enemy script and call the Hurt function.

			// Call the explosion instantiation.
			OnExplode();
		}
        if (col.tag == "Enemy")
		{
			BounceBullet();
			/// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            // Call the explosion instantiation.
            OnExplode();

        }
        if (col.tag == "EnemyBomber")
		{
			BounceBullet();

			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyBomber>().TakeDamage(damage);

			// Call the explosion instantiation.
			OnExplode();

		}
		// Otherwise if it hits a Shack...
		else if (col.tag == "Shack")
		{
			BounceBullet();
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Shack>().pistolDamage(damage);

			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if (!col.gameObject.GetComponent<Shack>().shackIsDestroyed)
			{
				OnExplode();
			}
		}

	}

	public void BounceBullet()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * -1, GetComponent<Rigidbody2D>().velocity.y);
	}

	public void Explode()
	{
		//Destroy(gameObject);
	}
}
