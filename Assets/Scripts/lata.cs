using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class lata : MonoBehaviour
{
    public Transform pivot;
    public float springRange;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

  

}
