using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Boton_act_2 : MonoBehaviour
{
    public GameObject no_ganaste;
    public GameObject si_ganaste;

    public Text text;
    public Counter counter;
    private int IdElemento;
    public static ConexionBD Connect;
    public static int Id_User;
    public static int actividad_id = 9001;
    private string aplicacion_cambio;
    public Animator anim;

    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;
    /// 
    private static string database = "ulearnet_reim_pilotaje";
    public static int id_tiempoxActividad_Actual;

    // Start is called before the first frame update
    void Start()
    {
        //InsertInicioActividad(actividad_id);
    }

    // Update is called once per frame
    void Update()
    {
        //no_ganaste.SetActive (false);
        //Seleccion_mal();
        //Seleccion_objeto();
    }
     public void Review_de_act_2 (){
    	//Application.LoadLevel("MenuActividades");
        SceneManager.LoadScene("MenuActividades");
    }

    public void Seleccion_objeto (){
        counter.number += 50;
        text.text = counter.number.ToString();
        IdElemento = 290033;
        aplicacion_cambio = "Actividad_2";
        Sumar(aplicacion_cambio, IdElemento);
        StartCoroutine(LoadScene(aplicacion_cambio));
        //InsertFinActividad();
        //Debug.Log("correcta_star_ok");   
        //Application.LoadLevel("Actividad_2");
    }

    public void Seleccion_objeto_lata (){
        counter.number += 50;
        text.text = counter.number.ToString();
        IdElemento = 290032;
        aplicacion_cambio = "Actividad_2_lata";
        Sumar(aplicacion_cambio, IdElemento);
        StartCoroutine(LoadScene(aplicacion_cambio));
        //InsertFinActividad();
        //Debug.Log("correcta_lata_ok");  
        //Application.LoadLevel("Actividad_2_lata");
    }

    public void Seleccion_objeto_botella (){     
        counter.number += 50;
        text.text = counter.number.ToString();
        IdElemento = 290030;
        aplicacion_cambio = "Actividad_2_botella";
        Sumar(aplicacion_cambio, IdElemento);
        StartCoroutine(LoadScene(aplicacion_cambio));
        //InsertFinActividad();
        //Debug.Log("correcta_botella_ok");
        //Application.LoadLevel("Actividad_2_botella");
    }

    public void Seleccion_objeto_moneda (){
        counter.number += 50;
        text.text = counter.number.ToString();
        IdElemento = 290031;
        aplicacion_cambio = "Actividad_2_moneda";
        Sumar(aplicacion_cambio, IdElemento);
        StartCoroutine(LoadScene(aplicacion_cambio));
        //InsertFinActividad();
        //Debug.Log("correcta_moneda_ok"); 
        //Application.LoadLevel("Actividad_2_moneda");
    }

    public void Seleccion_mal_estrella(){
        //yleld return new WaitForSeconds(4f);
        IdElemento = 290033;
        aplicacion_cambio = "seleccion_act_2_estrella";
        Error_act_2(aplicacion_cambio, IdElemento);
        //Debug.Log("incorrecta_estrella_ok"); 
        //Application.LoadLevel("seleccion_act_2_estrella");
    }

    public void Seleccion_mal_botella(){
        IdElemento = 290033;
        aplicacion_cambio = "seleccion_act_2_botella";
        Error_act_2(aplicacion_cambio, IdElemento);
        //Debug.Log("incorrecta_botella_ok"); 
        //Application.LoadLevel("seleccion_act_2_botella");
    }

    public void Seleccion_mal_lata(){
        IdElemento = 290033;
        aplicacion_cambio = "seleccion_act_2_lata";
        Error_act_2(aplicacion_cambio, IdElemento);
        //Debug.Log("incorrecta_lata_ok"); 
        //Application.LoadLevel("seleccion_act_2_lata");
    }

    public void Seleccion_mal_moneda(){
        IdElemento = 290033;
        aplicacion_cambio = "seleccion_act_2_moneda";
        Error_act_2(aplicacion_cambio, IdElemento);
        //Debug.Log("incorrecta_moneda_ok"); 
        //Application.LoadLevel("seleccion_act_2_moneda");
    }

    void Sumar(string aplicacion_cambio, int IdElemento)
    {
        Id_User = ConexionBD.idUsuario;
        si_ganaste.SetActive(true);
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        codigo.ExecuteNonQuery();
        CloseBD();
        Debug.Log("---------bien plastic-------------");
        //Application.LoadLevel(aplicacion_cambio);
        SceneManager.LoadScene(aplicacion_cambio);
    }

    void Error_act_2(string aplicacion_cambio, int IdElemento)
    {
        Id_User = ConexionBD.idUsuario;
        no_ganaste.SetActive(true);
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                              " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + "1" + ", " + "1" + ", " + "0" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        codigo.ExecuteNonQuery();
        CloseBD();
        Debug.Log("---------mal plastic-------------");
        SceneManager.LoadScene(aplicacion_cambio);
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

    ///////tiempo/////////
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
    }

    IEnumerator LoadScene(string aplicacion_cambio)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(aplicacion_cambio);
    }

}
