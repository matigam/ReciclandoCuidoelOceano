using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco_Mov : MonoBehaviour
{
    
    private Rigidbody2D myBody;
    public float speed;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h>0)
        	myBody.velocity = Vector2.right * speed;
        else if (h < 0)
        	myBody.velocity = Vector2.left * speed;
        else
        	myBody.velocity = Vector2.zero;
    }
}
