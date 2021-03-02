using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random=UnityEngine.Random;

public class boton_act_3 : MonoBehaviour
{

    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    public static int Id_User;
    public static string actividad_3 = "9002";

    private string Cambio_Actividad;
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Review_de_act_3 (){
        //Application.LoadLevel("MenuActividades");
        SceneManager.LoadScene("MenuActividades");
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_3 + "," + "290404" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
    }

     public void Before_menu_act_3 (){
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("MenuActividades");
        //Application.LoadLevel("MenuActividades");
    }
     public void After_menu_act_3 (){
        int index = Random.Range(16, 20);
        //SceneManager.LoadScene(index);
        SceneManager.LoadScene(index);
        int Cambio_Actividad = index;
        StartCoroutine(LoadScene2(Cambio_Actividad));
        //Debug.Log("Act_3 Loaded");
        //Application.LoadLevel("Actividad_3");
    }


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

    IEnumerator LoadScene2(int Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }
}
