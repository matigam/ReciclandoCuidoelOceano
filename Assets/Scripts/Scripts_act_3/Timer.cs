using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;

public class Timer : MonoBehaviour
{
    private static string database = "ulearnet_reim_pilotaje";
    public static int Id_User;
    public static int id_tiempoxActividad_Actual;
    public static int actividad_id = 9002;
    public static string id_reim = "206";
    //private int causa = 0;

    MySqlCommand codigo;
    MySqlConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        InsertInicioActividad(actividad_id);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Actividad_3"))
        {
            //SceneManager.LoadScene("scene2");
            //InsertFinActividad();
            //Debug.Log("Siiiiiiiiiiiiiiiii");
            //Debug.Log(SceneManager.GetActiveScene());
            //Debug.Log("Siiiiiiiiiiiiiiiii");
        }

        else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Actividad_3"))
        {
            //SceneManager.LoadScene("scene1");
            //Debug.Log("Nooooooooooooooooo");
            //Debug.Log(SceneManager.GetActiveScene());
            //Debug.Log("Nooooooooooooooooo");
        }
    }

    public void InsertInicioActividad(int actividad_id)
    {
        Debug.Log("HolaMundo");
        Id_User = ConexionBD.idUsuario;
        MySqlConnection conn = ConnectDB();
        string SQLQuery = "INSERT INTO " + database + ".tiempoxactividad (inicio, final, causa, usuario_id, reim_id, actividad_id)" + " VALUES ('" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + System.DateTime.Now.AddSeconds(5).ToString("yyyy-MM-dd HH:mm:ss") + "', 0,   " + Id_User + ", " + id_reim + ", " + actividad_id + ")";
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
        Debug.Log(id_tiempoxActividad_Actual);
        Debug.Log(System.DateTime.Now);
        Debug.Log(System.DateTime.Now.AddSeconds(5)); 
        Debug.Log("ChaoMundo");
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
}
