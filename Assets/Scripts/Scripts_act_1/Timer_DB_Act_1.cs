using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer_DB_Act_1 : MonoBehaviour
{


    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";

    public int Id_User;

    //////////////TIEMPO///////////////////////
    public float TimetoDestroy = 10f;//tiempo que durara la actividad
    public Text ContadorText;
    private int TiempoToText;
    ///////////////////////////////////////////

    MySqlCommand codigo;
    MySqlConnection conn;

    public static int id_tiempoxActividad_Actual;
    private static string database = "ulearnet_reim_pilotaje";
    /// 

    public static int actividad_id = 9000;
    public GameObject Panel;

    public Animator anim;
    private string Cambio_Actividad;
    //public static int idUsuario;

    //public static  ConexionBD conexionBD;

    public void Start()
    {
        //conexionBD.InsertInicioActividad(actividad_id);
        InsertInicioActividad(actividad_id);
        Debug.Log("Inicio actividad 1");
    }
    public void Update()
    {
        TimetoDestroy -= Time.deltaTime;
        TiempoToText = (int)TimetoDestroy;
        ContadorText.text = TiempoToText.ToString();
        if (TimetoDestroy <= 0)
        {
            InsertFinActividad();
            //Application.LoadLevel("MenuActividades");
            Time.timeScale = 0;
            Panel.SetActive(true);
            
        }
    }

    public void FinActividad()
    {
        //conexionBD.InsertInicioActividad(actividad_id);
        Debug.Log("Fin actividad 1");
    }

    public void InsertInicioActividad(int actividad_id)
    {
        Debug.Log("HolaMundo");
        Id_User = ConexionBD.idUsuario;
        MySqlConnection conn = ConnectDB();
        string SQLQuery = "INSERT INTO " + database + ".tiempoxactividad (inicio, final, causa, usuario_id, reim_id, actividad_id)" + " VALUES ('" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 0,   " + Id_User + ", " + id_reim + ", " + actividad_id + ")";
        MySqlCommand codigo = conn.CreateCommand();
        codigo.CommandText = (SQLQuery);

        codigo.ExecuteNonQuery();

        SQLQuery = "SELECT LAST_INSERT_ID();";
        codigo.CommandText = (SQLQuery);

        MySqlDataReader lectura = codigo.ExecuteReader();
        //GameController.Instance.id_usuario = value;
        if (lectura.HasRows)
        {
            while (lectura.Read())
            {
                lectura.Read();
                id_tiempoxActividad_Actual = lectura.GetInt32(0);
                print(id_tiempoxActividad_Actual);


            }
        }
        CloseBD();
    }

    public void InsertFinActividad()
    {
        
        MySqlConnection conn = ConnectDB();
        string SQLQuery = "UPDATE  " + database + ".tiempoxactividad SET final = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = " + id_tiempoxActividad_Actual + "";
        MySqlCommand codigo = conn.CreateCommand();
        codigo.CommandText = (SQLQuery);
        codigo.ExecuteNonQuery();
        CloseBD();
    }

    public MySqlConnection ConnectDB()
    {
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();
        return conectar;
    }

    
    public void Salir_act1()
    {
        Id_User = ConexionBD.idUsuario;
        InsertFinActividad();
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + 290012 + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //Application.LoadLevel("MenuActividades");
        //SceneManager.LoadScene("MenuActividades");
    }

    IEnumerator LoadScene(string Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }

    public void CloseBD()
    {

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
}
