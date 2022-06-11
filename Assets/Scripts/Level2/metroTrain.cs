 using UnityEngine;
using System.Collections;

public class metroTrain : MonoBehaviour
{
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1.5f;
	public GameObject explosion;			// Prefab of explosion effect.
	private int HP = 10;							// How many times the enemy can be hit before it dies.
	private static int staticHP = 10;
	private Animator anim;					// Reference to the shack's animator component.
	public Sprite damagedShack;			// An optional sprite of the enemy when it's damaged.
	public bool trainDestroyed = false;
	public GameObject Spawner;
	private bool exploded = false;

	//particle systems
	private ParticleSystem explosionShotFX; 
	private ParticleSystem explosionSmokeFX; 
	private ParticleSystem explosionFX; 
	
	void Awake ()
	{
		// Setting up references.
		explosionShotFX = GameObject.FindGameObjectWithTag("trainShotExplosion").GetComponent<ParticleSystem>();
		explosionSmokeFX = GameObject.FindGameObjectWithTag("trainSmokeExplosion").GetComponent<ParticleSystem>();
		//explosionFX = GameObject.FindGameObjectWithTag("trainExplosion").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator>();
	}
	
	public void bombDamage(int damage)
	{
		HP = HP - damage;
		if (HP <= 0 && !trainDestroyed) {
			Explode ();
			trainDestroyed = true;
		}
	}
	
	public void pistolDamage(int damage)
	{
		HP = HP - damage;
		showDamageOnTrain();

		if(!trainDestroyed)
			playParticleSystem(explosionShotFX);

		if (HP <= 0 && !trainDestroyed) {
			Explode ();
			//playParticleSystem(explosionFX );
			playParticleSystem(explosionSmokeFX);
			trainDestroyed = true;
		}
	}
	
	double damageOneHP = staticHP - 2;
	double damageTwoHP = staticHP - 4 ;
	double damageThreeHP = staticHP - 6;
	double damageFourHP = 0;
	bool damageOne =false;
	bool damageTwo =false;
	bool damageThree =false;
	bool damageFour =false;

	public void showDamageOnTrain()
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
		}
	}

	void FixedUpdate ()
	{
		
	}

	public void playParticleSystem(ParticleSystem particleSystem )
	{
		particleSystem.transform.position = transform.position;
		particleSystem.Play();
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		/*if (col.tag == "shotgunBullet" || col.tag == "Bomb" && !shackIsDestroyed) {
			anim.Play ("Level2MetroTrainShot");
		}*/

		if (col.tag == "shotgunBullet" || col.tag == "Bomb" && !trainDestroyed) {
			if (damageOne)
			{
				anim.Play ("Level2MetroTrainDamage1Shot");
				Debug.Log ("Level2MetroTrainDamage1Shot");
			}
			else if (damageTwo)
			{
				anim.Play ("Level2MetroTrainDamage2Shot");
				Debug.Log ("Level2MetroTrainDamage2Shot");
			}
			else if (damageThree)
			{
				anim.Play ("Level2MetroTrainDamage3Shot");
				Debug.Log ("Level2MetroTrainDamage3Shot");
			}
			else if (damageFour || HP < 0)
			{
				//Do nothing
			}
			else
			{
				anim.Play ("Level2MetroTrainShot");
				Debug.Log ("Level2MetroTrainShot");
			}
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
		// Play the explosion sound effect.
		AudioSource.PlayClipAtPoint(boom, transform.position);

		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}
		
		Destroy (Spawner);

	}
}
