using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShackBullet : MonoBehaviour
{
    public int shackBulletDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            // Call the explosion instantiation.
            OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }
        if (col.tag == "Player")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<PlayerControl>().playerHealth.TakeDamage(shackBulletDamage);

            float yPlaneDistance = 250;
            float xPlaneDistance = 250;
            bool facingRight = col.gameObject.GetComponent<PlayerControl>().facingRight;

            if (facingRight != null && !facingRight)
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));
            else
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));

            // Instantiate the explosion and destroy the rocket.
            //OnExplode();
            Destroy(gameObject);
        }
    }

    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        //Instantiate(explosion, transform.position, randomRotation);
    }
}
