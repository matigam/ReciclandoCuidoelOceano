using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisionador : MonoBehaviour
{
    void Start()
    {
      
    }


    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D target)
    {
    	if(target.gameObject.tag == "Basura")
    	{
    		Debug.Log("Ouch!!");
    	}
    }
}
