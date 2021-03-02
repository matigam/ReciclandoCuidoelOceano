using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float speed=0.1f;
	private Rigidbody2D rb2d;
    //private Rigidbody2D rb2d-black;

    void Start()
    {
        //var random = new Random(System.DateTime.Now.Millisecond);
        rb2d = GetComponent<Rigidbody2D>();
        var number = Random.Range(0,100);
        //public System.Random ran = new System.Random();
        //public int power = ran.Next(0, 10);
        transform.Rotate(new Vector3(0f, 0f, 60f)* Time.deltaTime*number);
    }

    void Update()
    {

    }


    public void RotateRigth()
    {
    	transform.Rotate(new Vector3(0f, 0f, 60f)* Time.deltaTime*1);
    }
    
    public void RotateLeft()
    {
    	transform.Rotate(new Vector3(0f, 0f, -60f)* Time.deltaTime*1);
    }

    public void Subir()
    {
    	Vector2 move = Vector2.zero;
    	move.y=1;
    	rb2d.MovePosition((Vector2)this.transform.position + (move*speed*Time.deltaTime)/4);
    }

    public void Bajar()
    {
    	Vector2 move = Vector2.zero;
    	move.y=-1;
    	rb2d.MovePosition((Vector2)this.transform.position + (move*speed*Time.deltaTime)/4);
    }

    public void Darecha()
    {
    	Vector2 move = Vector2.zero;
    	move.x=1;
    	rb2d.MovePosition((Vector2)this.transform.position + (move*speed*Time.deltaTime)/4);
    }

    public void Izquierda()
    {
    	Vector2 move = Vector2.zero;
    	move.x=-1;
    	rb2d.MovePosition((Vector2)this.transform.position + (move*speed*Time.deltaTime)/4);
    }




}
