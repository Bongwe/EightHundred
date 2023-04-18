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
	public AudioClip reloadSound;                // Array of clips for when the player shoots.
	public AudioClip themeSong;

	public PlayerControl playerCtrl;        // Reference to the PlayerControl script.
											//private Animator anim;					// Reference to the Animator component.

	public Bullets bulletText;
	public bool hasGun = false;
	private int spwanDistance = 1;

	private bool isShooting = false;
	public string shotGunType;
	public bool shotGunReloaded = true;
	public Rigidbody2D shotGunShell;
	public Vector2 shotGunShellForceDirection;
	public ShotGunHolder shotGunHolder;

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

			if (isShooting && shotGunReloaded && weapon.tag == "Bullet")
			{
				AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
				ammunition--;
				bulletText.amoLeft = ammunition;

				if (shotGunType == "Shotgun")
				{
					shootShotgun();
					shotGunReloaded = false;
					applyShotGunRecoil();
					StartCoroutine(ReloadShotgun(2.0f, playerCtrl.facingRight));
				}
				else if (shotGunType == "ShotgunAutomatic")
				{
					//shootAutomaticShotgun();
					shootShotgun();
					shotGunReloaded = false;
					applyShotGunRecoil();
					StartCoroutine(ReloadShotgun(0.5f, playerCtrl.facingRight));
				}
				else if (shotGunType == "ShotgunExplosive")
				{
					shootShotgun();
					shotGunReloaded = false;
					applyShotGunRecoil();
					StartCoroutine(ReloadShotgun(0.2f, playerCtrl.facingRight));
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

	IEnumerator ReloadShotgun(float time, bool facingRight)
	{
		yield return new WaitForSeconds(time);

		AudioSource.PlayClipAtPoint(reloadSound, transform.position);
		Rigidbody2D shotgunShellInstance = Instantiate(shotGunShell, transform.position, Quaternion.Euler(new Vector3(0, 0, 180.0f))) as Rigidbody2D;

		if (playerCtrl.facingRight)
		{
			shotgunShellInstance.GetComponent<Rigidbody2D>().AddForce(shotGunShellForceDirection);
		}
		else
        {
			shotgunShellInstance.GetComponent<Rigidbody2D>().AddForce(shotGunShellForceDirection);
		}
		shotGunReloaded = true;

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

	public void applyShotGunRecoil()
	{
		float yPlaneDistance = 150;
		float xPlaneDistance = 150;

		if (!playerCtrl.facingRight)
			playerCtrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));
		else
			playerCtrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * xPlaneDistance, yPlaneDistance));
	}
	

}
