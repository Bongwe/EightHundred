using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health;			// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	//private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	//private Animator anim;					// Reference to the Animator on the player

	public MyPlayerHealth myPlayerHealth;
	public GameObject life1;
	public GameObject life2;
	public GameObject life3;

	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		//healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		//anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		//healthScale = healthBar.transform.localScale;
		myPlayerHealth.healthLeft = health;
	}
	
	public void UpDateHealth()
	{
		life1.SetActive(true);
		life2.SetActive(true);
		life3.SetActive(true);
	}

	public void TakeDamage (int damage,Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;
		
		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 20f;
		hurtVector.x = hurtVector.x * 3;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
		
		// Reduce the player's health by 10.
		health -= damage;

		if (life1.activeSelf)
			life1.SetActive(false);
		else if (life2.activeSelf)
			life2.SetActive(false);
		else if (life3.activeSelf)
			life3.SetActive(false);

		myPlayerHealth.healthLeft = health;

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

	public void TakeDamage (int damage)
	{
		// Reduce the player's health by 10.
		health -= damage;

		if (life1.activeSelf)
			life1.SetActive(false);
		else if (life2.activeSelf)
			life2.SetActive(false);
		else if (life3.activeSelf)
			life3.SetActive(false);

		myPlayerHealth.healthLeft = health;
	}
}
