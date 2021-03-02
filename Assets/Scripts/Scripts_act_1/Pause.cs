using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Pause : MonoBehaviour
{
    bool active;
    Canvas canvas;
    public GameObject Panel;
    public GameObject Panel_Pause;
    public static int Id_User;

    MySqlCommand codigo;
    MySqlConnection conn;

    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";

    // Start is called before the first frame update

    public static string actividad = "9000";


    public void Info()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            Id_User = ConexionBD.idUsuario;
            conn = ConnectDB();
            codigo = conn.CreateCommand();
            codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "300003" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
            //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
            //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
            codigo.ExecuteNonQuery();
            CloseBD();
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
        }
    }

 

    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Panel_Pause.SetActive(true);
            Id_User = ConexionBD.idUsuario;
            conn = ConnectDB();
            codigo = conn.CreateCommand();
            codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "300002" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
            //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
            //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
            codigo.ExecuteNonQuery();
            CloseBD();
        }
        else
        {
            Time.timeScale = 1;
            Panel_Pause.SetActive(false);
        }
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

    public MySqlConnection ConnectDB()
    {
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();


        return conectar;
    }




}
