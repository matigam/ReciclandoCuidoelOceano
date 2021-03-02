using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiempo_actividad : MonoBehaviour
{
	public float TimetoDestroy = 10f;//tiempo que durara la actividad
	public Text ContadorText;
	public int TiempoToText;

    //public static Timer_DB_Act_1 timer_DB_Act_1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	TimetoDestroy -= Time.deltaTime;
        TiempoToText = (int)TimetoDestroy;
        ContadorText.text = TiempoToText.ToString();
        if (TimetoDestroy <= 0){
            //timer_DB_Act_1.Salir_act1();
            Application.LoadLevel("MenuActividades");
        }
    }
}
