using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public MainManager mainManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl playerControl = collision.GetComponent<PlayerControl>();
        
        if (collision.tag == "Player" && playerControl != null)
        {
            mainManager.playerPosition = playerControl.GetComponent<Rigidbody2D>().position;
        }
    }
}
