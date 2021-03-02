using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;


public class Timer_DB_Act_2 : MonoBehaviour
{
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    public int Id_User;
    MySqlCommand codigo;
    MySqlConnection conn;
    public static int id_tiempoxActividad_Actual;
    private static string database = "ulearnet_reim_pilotaje";
    public static int actividad_id = 9001;


    public void Start()
    {
        InsertInicioActividad(actividad_id);
        Debug.Log("Inicio actividad 2");
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
    public void Salir_act2()
    {
        InsertFinActividad();
        Application.LoadLevel("MenuActividades");
    }



}
