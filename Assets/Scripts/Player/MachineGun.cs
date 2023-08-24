using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
	public Rigidbody2D weapon;              // Prefab of the rocket.
	public float speed = 20f;               // The speed the rocket will fire at.
	public float cannonBallSpeed = 10f;
	public int ammunition = 1000;
	public AudioClip[] gunSound;                // Array of clips for when the player shoots.
	public AudioClip reloadSound;                // Array of clips for when the player shoots.
	public AudioClip themeSong;

	public PlayerControl playerCtrl;        // Reference to the PlayerControl script.
											//private Animator anim;					// Reference to the Animator component.

	public Bullets bulletText;
	public bool hasGun = false;
	private int spwanDistance = 1;

	private bool isShooting = false;
	private bool triggerIsPressed = false;
	public string machineGunType;
	public Rigidbody2D shotGunShell;
	public Vector2 shotGunShellForceDirection;
	public ShotGunHolder shotGunHolder;

	private IEnumerator coroutine;

	private float shootingRate = 0.3f;

	void Awake()
	{
		// Setting up the references.
		//anim = transform.root.gameObject.GetComponent<Animator>();
		//playerCtrl = transform.root.GetComponent<PlayerControl>();
		bulletText.amoLeft = ammunition;
		coroutine = shootGunCoroutine();
	}

	void Update()
	{
		// If the fire button is pressed...
		if (isShooting && hasGun && ammunition > 0)
		{

			if (isShooting && weapon.tag == "Bullet")
			{

				if (machineGunType == "MachineGun")
				{
					shootingRate = 0.3f;
					shootMachineGun();
					applyGunRecoil();
					//StartCoroutine(ReloadShotgun(2.0f, playerCtrl.facingRight));
				}
				else if (machineGunType == "SpreadMachineGun")
				{
					shootMachineGun();
					applyGunRecoil();
					//StartCoroutine(ReloadShotgun(0.5f, playerCtrl.facingRight));
				}
				else if (machineGunType == "IncreasedCapacityMachineGun")
				{
					shootingRate = 0.2f;
					shootMachineGun();
					applyGunRecoil();
					//StartCoroutine(ReloadShotgun(0.2f, playerCtrl.facingRight));
				}
				else if (machineGunType == "ExplosiveMachineGun")
				{
					shootingRate = 0.1f;
					StartCoroutine(coroutine);
					//applyGunRecoil();
				}

			}
		}
		/*else if (Input.GetButtonDown("Fire1") && hasGun && ammunition == 0)
			AudioSource.PlayClipAtPoint(gunSound[0], transform.position);*/

		isShooting = false;

	}

	IEnumerator ReloadShotgun(float time, bool facingRight)
	{
		yield return new WaitForSeconds(time);

		//AudioSource.PlayClipAtPoint(reloadSound, transform.position);
		Rigidbody2D shotgunShellInstance = Instantiate(shotGunShell, transform.position, Quaternion.Euler(new Vector3(0, 0, 180.0f))) as Rigidbody2D;

		if (playerCtrl.facingRight)
		{
			shotgunShellInstance.GetComponent<Rigidbody2D>().AddForce(shotGunShellForceDirection);
		}
		else
		{
			shotgunShellInstance.GetComponent<Rigidbody2D>().AddForce(shotGunShellForceDirection);
		}

	}

	public void shootClicked()
	{
		isShooting = true;
		triggerIsPressed = true;
	}

	public void shoot()
	{
		isShooting = true;
		triggerIsPressed = true;
	}

	public void notShoot()
	{
		isShooting = false;
		triggerIsPressed = false;
		StopCoroutine(coroutine);
	}

	public void shootMachineGun()
	{
		StartCoroutine(shootGunCoroutine());
	}


	IEnumerator shootGunCoroutine()
    {
		while(triggerIsPressed)
        {
			if (playerCtrl.facingRight)
			{
				Rigidbody2D bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);

				AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
				ammunition--;
				bulletText.amoLeft = ammunition;
			}
			else
			{
				Rigidbody2D bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-1 * speed, 0);

				AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
				ammunition--;
				bulletText.amoLeft = ammunition;
			}
		yield return new WaitForSeconds(shootingRate);
			
		}

		

	}

	IEnumerator spreadMachineGun()
	{
		// you need to make this shoot bullets and bombs, make these bombs 
		while (triggerIsPressed)
		{
			if (playerCtrl.facingRight)
			{
				Rigidbody2D bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);

				AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
				ammunition--;
				bulletText.amoLeft = ammunition;
			}
			else
			{
				Rigidbody2D bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-1 * speed, 0);

				AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
				ammunition--;
				bulletText.amoLeft = ammunition;
			}
			yield return new WaitForSeconds(shootingRate);

		}



	}

	public void applyGunRecoil()
	{
		float yPlaneDistance = 150;
		float xPlaneDistance = 150;

		if (!playerCtrl.facingRight)
			playerCtrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));
		else
			playerCtrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * xPlaneDistance, yPlaneDistance));
	}


}
