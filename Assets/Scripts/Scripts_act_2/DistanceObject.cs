using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class DistanceObject : MonoBehaviour
{

    public Text scoreText;
    private int causa;
    public int score;
    private static string database = "ulearnet_reim_pilotaje";
    public Text text;
    public Counter counter;
    private int IdElemento = 290029;
    public static ConexionBD Connect;
    public static int Id_User;
    public static int actividad_id = 9001;
    public static int id_tiempoxActividad_Actual;
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    public static string actividad_1 = "9000";

    MySqlCommand codigo;
    MySqlConnection conn;


    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    private float distance;
    private float distance2;
    public Text distancetxt;
    public Text distancetxt2;
    //public Text text;
    //public Counter counter;
    ////////////////// reloj///////////////////
    public Text contador;
    public Text fin;
    private float tiempo = 60f;
    /// //////////////////////////////////////
    private string Cambio_Actividad;
    public Animator anim;
    void Start()
    {
        SetDistance();
        contador.text = " " + tiempo;
        fin.enabled = false;
        InsertInicioActividad(actividad_id);
        Debug.Log("Inicio actividad 2");
    }


    void Update()
    {
        tiempo -= Time.deltaTime;
        contador.text = " " + tiempo.ToString("f0");

        if (tiempo <= 0)
        {
            contador.text = "0";
            fin.enabled = true;
            if (tiempo <= -3)
            {
                causa = 0;
                InsertFinActividad(causa);
                //Application.LoadLevel("MenuActividades");
                SceneManager.LoadScene("MenuActividades");
            }
        }
        distance = Vector2.Distance(object1.transform.position, object2.transform.position);
        distance2 = Vector2.Distance(object3.transform.position, object4.transform.position);
        SetDistance();
        if (distance<1)
        {
            if (distance2<1)
            {
                //Instantiate(object1, object2.transform.position);
                //counter.number += 5;
                //text.text = counter.number.ToString();
                SceneManager.LoadScene("buen_trabajo");
                causa = 2;
                InsertFinActividad(causa);
                Sumar();
                Cambio_Actividad = "buen_trabajo";
                StartCoroutine(LoadScene(Cambio_Actividad));
                //SceneManager.LoadScene("buen_trabajo");
            }
                      
        }       
            
    }

    void SetDistance()
    {
        distancetxt.text = "Distancia:" + distance.ToString();
        distancetxt2.text = "Distancia2:" + distance2.ToString();
    }

    void Sumar()
    {
        //Debug.Log("---------lalalal-------------");
        counter.number += 100;
        text.text = counter.number.ToString();
        InsertBien();        
    }

    public void InsertBien()
    {
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                          " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log();
        codigo.ExecuteNonQuery();
        CloseBD();
        Debug.Log(IdElemento);
        //Debug.Log("--------bien ok-------------");
    }

    /// tiempo /////
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
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_1 + "," + "290402" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
        //SceneManager.LoadScene("MenuActividades");
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //Application.LoadLevel("MenuActividades");
    }

    IEnumerator LoadScene(string Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }

}
