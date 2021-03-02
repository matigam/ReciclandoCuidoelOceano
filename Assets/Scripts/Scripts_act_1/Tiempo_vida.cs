using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiempo_vida : MonoBehaviour
{
	public float TiempoDeVida = 3;

    // Update is called once per frame
    void Update()
    {
        TiempoDeVida -= Time.deltaTime;
        if (TiempoDeVida <= 0)
        {
        	Destroy(this.gameObject);
        }
    }
}


