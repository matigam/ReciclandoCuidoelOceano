using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Player : MonoBehaviour
{
	Rigidbody2D RB;
	float direcX;
	float vel = 35f;



    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    public static int Id_User;

    public static int actividad_id = 9000;



    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direcX = Input.acceleration.x * vel;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -11.5f, 7.5f), transform.position.y);
        if((Input.acceleration.x)>1)
        {
            InsertMovRigth();
        }
        if((Input.acceleration.x)<-1)
        {
            InsertMovLeft();
        }
        //Debug.Log(Input.acceleration.x);
        //Debug.Log(Input.acceleration.y);

    }

    void FixedUpdate()
    {
    	RB.velocity = new Vector2(direcX, 0f);
        
    }

    public void InsertMovRigth(){

        conn = ConnectDB();
        Id_User = ConexionBD.idUsuario;
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES ("+ id_periodo + ", "+ Id_User + ", "+ id_reim + ", "+ actividad_id + ","+"290006"+", "+Convert.ToInt32(Input.acceleration.x)+", "+Convert.ToInt32(Input.acceleration.y)+", "+"4"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202001"+", "+"25794"+", "+"1"+", "+"1"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        
        codigo.ExecuteNonQuery(); 
        CloseBD();        
    }

    public void InsertMovLeft(){

        conn = ConnectDB();
        Id_User = ConexionBD.idUsuario;
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + "290007"+", "+Convert.ToInt32(Input.acceleration.x)+", "+Convert.ToInt32(Input.acceleration.y)+", "+"3"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202001"+", "+"25794"+", "+"1"+", "+"1"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log(Input.acceleration.x);
        //Debug.Log(Input.acceleration.y);
        codigo.ExecuteNonQuery(); 
        CloseBD();        
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
}
