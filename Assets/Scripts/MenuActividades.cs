using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random=UnityEngine.Random;

public class MenuActividades : MonoBehaviour
{
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";

    private static string database = "ulearnet_reim_pilotaje";
    public Text text;
    public Counter counter;
    public static int numero;
    public GameObject Panel;

    public int score;

    private string Cambio_Actividad;

    /// 
    MySqlCommand codigo;
    MySqlConnection conn;
    // Start is called before the first frame update

    public static int Id_User;
    public static int maximo = 1000;

    //public Animator anim;
    public static string actividad = "9004";

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            //InsertTouch();
            
        }
       
    }

    public void LoadRandomScene ()
    {
    	//usa este metodo para cargar una scena de manera random
    	int index = Random.Range(1, 6);
    	SceneManager.LoadScene(index);
    	Debug.Log("Scene Loaded");
    }
    public void Actividad_1 ()
    {
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES ("+ id_periodo + ", "+ Id_User + ", "+id_reim+", "+ actividad + ","+"290002"+", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery(); 
        CloseBD();
        Cambio_Actividad = "Instruccion_1_act_1";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //Debug.Log("Act_1 Loaded");
    }

    public void Actividad_2 (){
        //Application.LoadLevel("seleccion_act_2");
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();

        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "290003" +", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery(); 
        CloseBD();
        int index = Random.Range(12, 16);
    	SceneManager.LoadScene(index);
    	Debug.Log("Act_2 Loaded");

    }

    public void Actividad_3 (){
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "290004" +", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery(); 
        CloseBD();
        Cambio_Actividad = "Instruccion_1_act_3";
        StartCoroutine(LoadScene(Cambio_Actividad));
    }
    public void Actividad_Multijugador()
    {
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "290400" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
        Cambio_Actividad = "StepLobby";
        StartCoroutine(LoadScene(Cambio_Actividad));
    }
    public void Actividad_Desafio () 
    {
        Id_User = ConexionBD.idUsuario;
        Debug.Log(counter.number.ToString());
        numero = counter.number;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "290401" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
        if (numero >= maximo)
        {
            score = score - 1000;
            counter.number -= 1000;
            text.text = counter.number.ToString();
            Cambio_Actividad = "Quiz";
            StartCoroutine(LoadScene(Cambio_Actividad));
        }
            
        else
            Panel.SetActive(true);
           //SceneManager.LoadScene("Instruccion_1_act_1");
        //Debug.Log("Act_1 Loaded");
    }

    public void ok()
    {
        Panel.SetActive(false);
    }
    public void Atras_Colab()
    {

        //conn = ConnectDB();

        //codigo = conn.CreateCommand();
        //codigo.CommandText = ("INSERT INTO ulearnet_reim_pruebas.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
        //                      " VALUES (" + "202002" + ", " + "1163" + ", " + "206" + ", " + "9004" + "," + "290002" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //codigo.ExecuteNonQuery();
        //CloseBD();
        //Application.LoadLevel("Instruccion_1_act_1");
        Destroy(this.gameObject);
        //Destroy(gameObject);
        //Destroy(gameObject, .5f);
        //Destroy(this);
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
        Destroy(o);
        }
        SceneManager.LoadScene("MenuActividades");
        //Debug.Log("Act_1 Loaded");
    }

    

public void Review_Menu (){
    	
        conn = ConnectDB();

        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES ("+"202002"+", "+"1163"+", "+"206"+", "+"9004"+","+"290005"+", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery(); 
        CloseBD();
        //Application.LoadLevel("MenuPrincipal");
        SceneManager.LoadScene("MenuPrincipal");
        Debug.Log("Act_Menu Loaded");
    }




    //public void InsertTouch(){
//
    //    conn = ConnectDB();
//
    //    codigo = conn.CreateCommand();
    //    codigo.CommandText = ("INSERT INTO ulearnet_reim_pruebas.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
     //                         " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"290002"+", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
    //    //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
    //    codigo.ExecuteNonQuery(); 
    //    CloseBD();
    //    
    //}

    public void CloseBD() {

        if (codigo != null)
        {
            codigo.Dispose();
        }
 
        if (conn != null)
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public MySqlConnection ConnectDB()
    {
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();

       
        return conectar;
    }


    IEnumerator LoadScene(string Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }

}

