using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShellExplosive : MonoBehaviour
{

    public AudioSource explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            explosionSound.Play();
            Destroy(gameObject);
        }   

    }

   /* IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time);
      
    }*/
}

