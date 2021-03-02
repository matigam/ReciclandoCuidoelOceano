using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MenuPrincipal : MonoBehaviour
{
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    private static string database = "ulearnet_reim_pilotaje";

    public ConexionBD Connect;
    public int Id_User;
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    public Animator anim;
    public string SceneName;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.touches.Length<=0){
    		//si no hay ningun dedo, no hacemos nada
    	}else{
    		Debug.Log("Me tocaste en MenuPrincipal");
    	}

        if (Input.GetMouseButtonDown(0))
        {            
            //InsertTouch();           
        }
        
    }
    public void Jugar (){
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();

        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + "9003" + "," + "290001" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pruebas.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+id_periodo+", "+"25794"+", "+id_reim+", "+"1"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        //Debug.Log("Inert Touch " + Id_User);
        CloseBD();
        StartCoroutine(LoadScene());
        //SceneManager.LoadScene("MenuActividades");
    }
    public void Salir (){
    	Application.Quit();
    }
    
    public void InsertTouch(){

        //Id_User = ConexionBD.idUsuario;
        //conn = ConnectDB();

        //codigo = conn.CreateCommand();
        //codigo.CommandText = ("INSERT INTO ulearnet_reim_pruebas.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
         //                     " VALUES ("+id_periodo+", "+ Id_User + ", "+id_reim+", "+"9003"+","+ "290001" + ", "+Convert.ToInt32(Input.mousePosition.x)+", "+Convert.ToInt32(Input.mousePosition.y)+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log("INSERT INTO ulearnet_reim_pruebas.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+id_periodo+", "+"25794"+", "+id_reim+", "+"1"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        //Debug.Log(Input.mousePosition.x);
        //Debug.Log(Input.mousePosition.y);
        //codigo.ExecuteNonQuery();
       //Debug.Log("Inert Touch "+Id_User);
        //CloseBD();        
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
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=" + database + ";";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();


        return conectar;
    }

    IEnumerator LoadScene()
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }

}
