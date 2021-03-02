using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class SalirTimeMulti : MonoBehaviour
{
    private static string database = "ulearnet_reim_pilotaje";
    private int causa;
    public static ConexionBD Connect;
    public static int Id_User;
    public static int actividad_id = 9010;
    public static int id_tiempoxActividad_Actual;
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";

    MySqlCommand codigo;
    MySqlConnection conn;


    //public Text text;
    //public Counter counter;

    void Start()
    {
        InsertInicioActividad(actividad_id);
        Debug.Log("Inicio actividad multi");
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void InsertFinActividad(int causa)
    {
        MySqlConnection conn = ConnectDB();
        string SQLQuery = "UPDATE  " + database + ".tiempoxactividad SET final = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', causa =  '" + causa + "'  WHERE id = " + id_tiempoxActividad_Actual + "";
        MySqlCommand codigo = conn.CreateCommand();
        Debug.Log(SQLQuery);
        codigo.CommandText = (SQLQuery);
        codigo.ExecuteNonQuery();
        CloseBD();
    }
    /// tiempo /////
    /// 
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

    public void Salir_act2()
    {
        causa = 1;
        InsertFinActividad(causa);
        Destroy(this.gameObject);
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
        SceneManager.LoadScene("MenuActividades");
        //Application.LoadLevel("MenuActividades");
    }
}
