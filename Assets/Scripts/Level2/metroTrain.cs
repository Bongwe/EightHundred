 using UnityEngine;
using System.Collections;

public class MetroTrain : MonoBehaviour
{
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1.5f;
	public GameObject explosion;            // Prefab of explosion effect.
	public int HP = 20;							// How many times the enemy can be hit before it dies.
	private static int staticHP = 20;
	private Animator anim;					// Reference to the shack's animator component.
	public Sprite damagedShack;			// An optional sprite of the enemy when it's damaged.
	public bool trainDestroyed = false;
	public GameObject Spawner;
	private bool exploded = false;

	//particle systems
	private ParticleSystem explosionShotFX;
	public ParticleSystem explosionSmokeFX; 
	private ParticleSystem explosionFX;

	SpriteRenderer m_SpriteRenderer;
	private float gunShotTime = 0.1f;

	void Awake ()
	{
		// Setting up references.
		//explosionShotFX = GameObject.FindGameObjectWithTag("smoke").GetComponent<ParticleSystem>();
		//explosionSmokeFX = GameObject.FindGameObjectWithTag("TrainSmoke").GetComponent<ParticleSystem>();
		//explosionFX = GameObject.FindGameObjectWithTag("trainExplosion").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		//Set the GameObject's Color quickly to a set Color (blue)
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

		/*if(!trainDestroyed)
			playParticleSystem(explosionShotFX);*/

		if (HP <= 4 && !trainDestroyed) {
			Explode ();
			//playParticleSystem(explosionFX );
			playParticleSystem(explosionSmokeFX);
			trainDestroyed = true;
		}
	}

	public void playParticleSystem(ParticleSystem particleSystem)
	{
        ParticleSystem particleSystem1 = Instantiate(particleSystem, transform.position, Quaternion.Euler(new Vector3(0, 0, 180.0f)));
		particleSystem1.Play();
	}

	double damageOneHP = staticHP - 6;
	double damageTwoHP = staticHP - 12;
	double damageThreeHP = staticHP - 18;
	double damageFourHP = 0;
	bool damageOne =false;
	bool damageTwo =false;
	bool damageThree =false;
	bool damageFour =false;

	public void showDamageOnTrain()
	{
		if (HP > 12 && HP < 20) {
			anim.SetBool ("damage_1", true);
			anim.SetBool ("damage_2", false);
			anim.SetBool ("damage_3", false);
			anim.SetBool ("damage_4", false);
			damageOne = true;
			
		} else if (HP >= 8 && HP <= 12) {
			anim.SetBool ("damage_1", false);
			anim.SetBool ("damage_2", true);
			anim.SetBool ("damage_3", false);
			anim.SetBool ("damage_4", false);
			//damage2Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = true;
			
		} else if (HP >= 4 && HP < 8) {
			anim.SetBool ("damage_1", false);
			anim.SetBool ("damage_2", false);
			anim.SetBool ("damage_3", true);
			anim.SetBool ("damage_4", false);
			//damage3Smoke.enableEmission = true;
			damageOne = false;
			damageTwo = false;
			damageThree = true;
			
		} else if (HP <= 0) {
			anim.SetBool ("damage_1", false);
			anim.SetBool ("damage_2", false);
			anim.SetBool ("damage_3", false);
			anim.SetBool ("damage_4", true);
			damageThree = false;
			damageFour = true;
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		/*if (col.tag == "shotgunBullet" || col.tag == "Bomb" && !shackIsDestroyed) {
			anim.Play ("Level2MetroTrainShot");
		}*/

		if (col.tag == "Bullet" || col.tag == "Bomb" && !trainDestroyed) {
			if (damageOne)
			{
				//anim.Play ("TrainShot");
				m_SpriteRenderer.color = Color.red;
				StartCoroutine(TrainShot(gunShotTime));

			}
			else if (damageTwo)
			{
				//anim.Play ("TrainShot");
				m_SpriteRenderer.color = Color.red;
				StartCoroutine(TrainShot(gunShotTime));
			}
			else if (damageThree)
			{
				//anim.Play ("TrainShot");
				m_SpriteRenderer.color = Color.red;
				StartCoroutine(TrainShot(gunShotTime));
			}
			else if (damageFour || HP < 0)
			{
				//Do nothing
			}
			else
			{
				/*m_SpriteRenderer.color = Color.red;
				StartCoroutine(TrainShot(gunShotTime));*/
			}
			showDamageOnTrain();
		}
	}
	
	void gunShotExplode()
	{
		//explosionSmokeFX.transform.position = transform.position;
		//explosionSmokeFX.Play();
		
		 //Instantiate the explosion prefab.
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

	IEnumerator TrainShot(float time)
	{
		yield return new WaitForSeconds(time);
		m_SpriteRenderer.color = Color.white;
	}

}
