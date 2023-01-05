using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolCannonBall : MonoBehaviour
{
	public int damage = 4;

	// Start is called before the first frame update
	void Start()
	{
		Destroy(gameObject, 2.0f);
	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		Enemy enemy = col.gameObject.GetComponent<Enemy>();
		EnemyCircle enemyCircle = col.gameObject.GetComponent<EnemyCircle>();

		// If it hits an enemy...
		if (col.tag == "Enemy" && enemy != null && !enemy.dead)
		{
			/// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);

			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy(gameObject);
		}
		if (col.tag == "EnemyBomber" && enemy != null && !enemy.dead)
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyBomber>().TakeDamage(damage);

			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy(gameObject);
		}
		else if (col.tag == "Enemy" && enemyCircle != null && !enemyCircle.dead)
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyCircle>().TakeDamage(damage);

			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy(gameObject);
		}
		// Otherwise if it hits a Shack...
		else if (col.tag == "Shack")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Shack>().pistolDamage(damage);

			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if (!col.gameObject.GetComponent<Shack>().shackIsDestroyed)
			{
				OnExplode();
				// Destroy the bullets	.
				Destroy(gameObject);
			}
		}
		else if (col.tag == "ShootingShack")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<ShootingShack>().pistolDamage(damage);

			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if (!col.gameObject.GetComponent<ShootingShack>().shackIsDestroyed)
			{
				OnExplode();
				// Destroy the bullets	.
				Destroy(gameObject);
			}
		}
		else if (col.tag == "door")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Level3Door>().pistolDamage(damage);

			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if (!col.gameObject.GetComponent<Level3Door>().doorIsDestroyed)
			{
				OnExplode();
				// Destroy the bullets	.
				Destroy(gameObject);
			}

		}
	}

	public void OnExplode()
    {

    }
}
