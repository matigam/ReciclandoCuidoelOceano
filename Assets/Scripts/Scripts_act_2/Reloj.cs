using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{
    
    public int Id_User;
    
    
    public Text contador;
	public Text fin;
	private float tiempo = 60f;
    


    // Start is called before the first frame update
    void Start()
    {
        contador.text = " " + tiempo;
        fin.enabled = false;

        //nsertInicioActividad(actividad_id);
        //Debug.Log("Inicio actividad 2");
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        contador.text = " " + tiempo.ToString("f0");

        if(tiempo <= 0){
        	contador.text = "0";
        	fin.enabled = true;
        	if(tiempo <= -3)
            {
                //InsertFinActividad();
                //timer_DB_Act_2.InsertFinActividad();
                Application.LoadLevel("MenuActividades");
        	}
        }
    }

    public void FinActividad()
    {
        //conexionBD.InsertInicioActividad(actividad_id);
        Debug.Log("Fin actividad 1");
    }

}
