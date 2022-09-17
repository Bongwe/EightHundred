using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMovingLeft = false;
    public bool isMovingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveLeftPressed()
    {
        isMovingLeft = true;
    }

    public void moveLeftUnPressed()
    {
        isMovingLeft = false;
    }

    public void moveRightPressed()
    {
        isMovingRight = true;
    }

    public void moveRightUnPressed()
    {
        isMovingRight = false;
    }
}
