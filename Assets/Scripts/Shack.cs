using UnityEngine;
using System.Collections;

public class Shack : MonoBehaviour
{
	//public float bombRadius = 10f;			// Radius within which enemies are killed.
	//public float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1.5f;
	public GameObject explosion;			// Prefab of explosion effect.
	private ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.
	private int HP = 10;							// How many times the enemy can be hit before it dies.
	private static int staticHP = 10;
	private Animator anim;					// Reference to the shack's animator component.
	public bool shackIsDestroyed = false;
	public Sprite damagedShack;			// An optional sprite of the enemy when it's damaged.
	private bool shackNotDestroyed;
	public GameObject Spawner;
	private bool exploded = false;

	//particle systems
	private ParticleSystem explosionOneFX; 
	private ParticleSystem explosionDustFX; 
	private ParticleSystem explosionFireFX;

	void Awake ()
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		//explosionOneFX = GameObject.FindGameObjectWithTag("ShackExplosionOneFX").GetComponent<ParticleSystem>();
		//explosionDustFX = GameObject.FindGameObjectWithTag("ShackExplosionDust").GetComponent<ParticleSystem>();
		//explosionFireFX = GameObject.FindGameObjectWithTag("ShackFireExplosion").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator> ();
	}

	double damageOneHP = staticHP - 2;
	double damageTwoHP = staticHP - 4 ;
	double damageThreeHP = staticHP - 6;
	double damageFourHP = 0;
	bool damageOne =false;
	bool damageTwo =false;
	bool damageThree =false;
	bool damageFour =false;

	public void bombDamage(int damage)
	{
		HP = HP - damage;
		showDamageOnShack();
		if (HP <= 0 && !shackIsDestroyed) {
			Explode ();
			shackIsDestroyed = true;
		}
	}
	
	public void pistolDamage(int damage)
	{
		HP = HP - damage;
		showDamageOnShack();

		if(!shackIsDestroyed)
			playParticleSystem(explosionOneFX);
	
		if (HP <= 0 && !shackIsDestroyed)
		{
			Explode ();
			playParticleSystem(explosionFireFX );
			playParticleSystem(explosionDustFX);
			shackIsDestroyed = true;
		}
	}

	void FixedUpdate ()
	{

	}

	public void playParticleSystem(ParticleSystem particleSystem )
	{
		//particleSystem.transform.position = transform.position;
		//particleSystem.Play();
	}

	public void showDamageOnShack()
	{
	
		if (HP == damageOneHP) {
			anim.SetBool ("damageOne", true);
			anim.SetBool ("damageTwo", false);
			anim.SetBool ("damageThree", false);
			anim.SetBool ("damageFour", false);
			damageOne = true;
			
		} else if (HP == damageTwoHP) {
			anim.SetBool ("damageOne", false);
			anim.SetBool ("damageTwo", true);
			anim.SetBool ("damageThree", false);
			anim.SetBool ("damageFour", false);
			//damage2Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = true;
			
		} else if (HP == damageThreeHP) {
			anim.SetBool ("damageOne", false);
			anim.SetBool ("damageTwo", false);
			anim.SetBool ("damageThree", true);
			anim.SetBool ("damageFour", false);
			//damage3Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = false;
			damageThree = true;
			
		} else if (HP <= damageFourHP) {
			anim.SetBool ("damageOne", false);
			anim.SetBool ("damageTwo", false);
			anim.SetBool ("damageThree", false);
			anim.SetBool ("damageFour", true);
			damageThree = false;
			damageFour = true;
			//damage2Smoke.enableEmission = false;
			//damage3Smoke.enableEmission = false;
			
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Bullet" || col.tag == "Bomb" && !shackIsDestroyed) {
			if (damageOne)
				anim.Play ("Level1ShackDamage1Shot");
			else if (damageTwo)
				anim.Play ("Level1ShackDamage2Shot");
			else if (damageThree)
				anim.Play ("Level1ShackDamage3Shot");
			else if (damageFour)
			{
				//Do nothing
			}
			else
				anim.Play ("shackShot");
		}
	}

	void gunShotExplode()
	{
		explosionFX.transform.position = transform.position;
		explosionFX.Play();
		
		// Instantiate the explosion prefab.
		//Instantiate(explosion,transform.position, Quaternion.identity);
	}
	
	public void Explode()
	{
		
		// Set the explosion effect's position to the bomb's position and play the particle system.
		//explosionFX.transform.position = transform.position;
		//explosionFX.Play();
		
		// Instantiate the explosion prefab.
		//Instantiate(explosion,transform.position, Quaternion.identity);
		
		// Play the explosion sound effect.
		AudioSource.PlayClipAtPoint(boom, transform.position);
		showDamageOnShack();


		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		//Spawner.SetActive(false);
		Destroy (Spawner);
		// Destroy the shack.
		//Destroy (gameObject);
	}
}
