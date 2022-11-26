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
	public float ammunition = 10;
	public AudioClip[] gunSound;
	public bool shackIsDestroyed = false;
	public int HP = 20;


	private Animator anim;
	double damageOneHP = 16;
	double damageTwoHP = 10;
	double damageThreeHP = 6;
	double damageFourHP = 0;
	bool damageOne = false;
	bool damageTwo = false;
	bool damageThree = false;
	bool damageFour = false;

	private ParticleSystem explosionOneFX;
	private ParticleSystem explosionDustFX;
	private ParticleSystem explosionFireFX;

	void Awake()
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		//explosionOneFX = GameObject.FindGameObjectWithTag("ShackExplosionOneFX").GetComponent<ParticleSystem>();
		//explosionDustFX = GameObject.FindGameObjectWithTag("ShackExplosionDust").GetComponent<ParticleSystem>();
		//explosionFireFX = GameObject.FindGameObjectWithTag("ShackFireExplosion").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator>();
	}


	void Start()
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();

		InvokeRepeating("Shoot", 2.0f, 0.6f);

	}

	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		//Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the Bullet is with the random rotation.
		//Instantiate(explosion, transform.position, randomRotation);
	}

	public void bombDamage(int damage)
	{
		HP = HP - damage;
		showDamageOnShack();
		if (HP <= 0 && !shackIsDestroyed)
		{
			Explode();
			shackIsDestroyed = true;
		}
	}

	public void pistolDamage(int damage)
	{
		HP = HP - damage;
		showDamageOnShack();

		if (!shackIsDestroyed)
			playParticleSystem(explosionOneFX);

		if (HP <= 0 && !shackIsDestroyed)
		{
			Explode();
			playParticleSystem(explosionFireFX);
			playParticleSystem(explosionDustFX);
			shackIsDestroyed = true;
		}
	}

	public void playParticleSystem(ParticleSystem particleSystem)
	{
		//particleSystem.transform.position = transform.position;
		//particleSystem.Play();
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
			anim.Play("ShootingShackShot");
			if (!this.shackIsDestroyed)
            {
                OnExplode();
            }
        }

    }

	public void Explode()
	{
		shackIsDestroyed = true;
		//Destroy(gameObject);
	}

    public void Update()
    {
        
	}

	public void Shoot()
	{
		// ... set the animator Shoot trigger parameter and play the audioclip.
		//anim.SetTrigger("Shoot");
		//GetComponent<AudioSource>().Play();
		AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
		ammunition--;

		float randomX = Random.Range(0.0f, 10.0f);
		float randomy = Random.Range(-1.0f, 5.0f);
		bulletSpeed = Random.Range(10.0f, 20.0f);

		Vector3 shackPosition = transform.position;
		shackPosition.x = shackPosition.x - randomX;
		shackPosition.y = shackPosition.y + randomy;
		// Otherwise instantiate the rocket facing left and set it's velocity to the left.
		Rigidbody2D bulletInstance = Instantiate(weapon, shackPosition, Quaternion.Euler(new Vector3(0, 0, 180.0f))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(-1 * bulletSpeed, 0);

		Vector2 bulletSpawnPosition = transform.position;
	}

	public void showDamageOnShack()
	{

		if (HP >= damageTwoHP && HP <= damageOneHP)
		{
			anim.SetBool("damageOne", true);
			anim.SetBool("damageTwo", false);
			anim.SetBool("damageThree", false);
			anim.SetBool("damageFour", false);
			damageOne = true;

		}
		else if (HP >= damageThreeHP && HP <= damageTwoHP)
		{
			anim.SetBool("damageOne", false);
			anim.SetBool("damageTwo", true);
			anim.SetBool("damageThree", false);
			anim.SetBool("damageFour", false);
			//damage2Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = true;

		}
		else if (HP >= damageFourHP && HP <= damageThreeHP)
		{
			anim.SetBool("damageOne", false);
			anim.SetBool("damageTwo", false);
			anim.SetBool("damageThree", true);
			anim.SetBool("damageFour", false);
			//damage3Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = false;
			damageThree = true;

		}
		else if (HP <= damageFourHP)
		{
			anim.SetBool("damageOne", false);
			anim.SetBool("damageTwo", false);
			anim.SetBool("damageThree", false);
			anim.SetBool("damageFour", true);
			damageThree = false;
			damageFour = true;
			//damage2Smoke.enableEmission = false;
			//damage3Smoke.enableEmission = false;

		}
	}
}
