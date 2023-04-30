using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShellExplosive : MonoBehaviour
{

    public AudioSource explosionSound;
    public ShotgunShellExplosionAnimation ShotgunShellExplosionAnimation;
    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplodeCoroutine(2.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            Explode();
        }   

    }

    void Explode()
    {
        explosionSound.Play();
        Instantiate(ShotgunShellExplosionAnimation, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(gameObject, 0.5f);

    }

    IEnumerator ExplodeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }
}

