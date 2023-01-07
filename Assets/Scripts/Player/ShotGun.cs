using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotGun : MonoBehaviour
{
	public Rigidbody2D weapon;              // Prefab of the rocket.
	public float speed = 20f;               // The speed the rocket will fire at.
	public float cannonBallSpeed = 10f;
	public int ammunition = 0;
	public AudioClip[] gunSound;                // Array of clips for when the player shoots.
	public AudioClip themeSong;

	public PlayerControl playerCtrl;        // Reference to the PlayerControl script.
											//private Animator anim;					// Reference to the Animator component.

	public Bullets bulletText;
	public bool hasGun = false;
	private int spwanDistance = 1;

	private bool isShooting = false;
	public string shotGunType;

	void Awake()
	{
		// Setting up the references.
		//anim = transform.root.gameObject.GetComponent<Animator>();
		//playerCtrl = transform.root.GetComponent<PlayerControl>();
		bulletText.amoLeft = ammunition;
	}

	void Update()
	{
		// If the fire button is pressed...
		if (isShooting && hasGun && ammunition > 0)
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			//anim.SetTrigger("Shoot");
			//GetComponent<AudioSource>().Play();
			AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
			ammunition--;
			bulletText.amoLeft = ammunition;

			if (isShooting && weapon.tag == "Bullet")
			{
				if (shotGunType == "Shotgun")
				{
					shootShotgun();

				}
				else if (shotGunType == "ShotgunAutomatic")
				{
					shootAutomaticShotgun();
				}
				else if (shotGunType == "ShotgunExplosive")
				{
					shootExplosiveShotgun();
				}
				else if (shotGunType == "ShotgunMachine")
				{
					shootMachineShotgun();
				}
			}
		}
		else if (Input.GetButtonDown("Fire1") && hasGun && ammunition == 0)
			AudioSource.PlayClipAtPoint(gunSound[0], transform.position);

		isShooting = false;

	}

	public void shoot()
	{
		isShooting = true;
	}

	public void shootShotgun()
	{
		Rigidbody2D[] bulletInstance = new Rigidbody2D[4];
		Vector2[] bulletPositions = new Vector2[4];

		for (int i = 0; i < bulletInstance.Length; i++)
		{
			float bulletYPosition = Random.Range(0.0f, 0.9f);
			float bulletXPosition = Random.Range(0.0f, 4.0f);
			bulletPositions[i] = new Vector2(bulletXPosition, bulletYPosition);
		}

		if (playerCtrl.facingRight)
		{
			for (int i = 0; i < bulletInstance.Length; i++)
			{
				Vector2 newBulletPosition = new Vector2(transform.position.x + bulletPositions[i].x, transform.position.y + bulletPositions[i].y);
				bulletInstance[i] = Instantiate(weapon, newBulletPosition, Quaternion.Euler(new Vector3(0, 0, 180.0f))) as Rigidbody2D;
				bulletInstance[i].velocity = new Vector2(speed, 0);
			}

		}
		else
		{
			for (int i = 0; i < bulletInstance.Length; i++)
			{
				Vector2 newBulletPosition = new Vector2(transform.position.x + (bulletPositions[i].x * -1), transform.position.y + bulletPositions[i].y);
				bulletInstance[i] = Instantiate(weapon, newBulletPosition, Quaternion.Euler(new Vector3(0, 0, 180.0f))) as Rigidbody2D;
				bulletInstance[i].velocity = new Vector2(-1 * speed, 0);
			}
		}
	}
	public void shootAutomaticShotgun()
	{
	}

	public void shootExplosiveShotgun()
	{
	}

	public void shootMachineShotgun()
	{
	}

}
