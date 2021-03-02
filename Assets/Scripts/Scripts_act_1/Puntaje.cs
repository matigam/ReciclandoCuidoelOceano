using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Puntaje : MonoBehaviour
{
	public Text scoreText;

	public int score;

    public Text text;
    public Counter counter;
    private int IdElemento;

    public ConexionBD Connect;
    public static int Id_User;
    public static int actividad_id = 9000;

    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    

    void Start()
    {
        
        //fin.SetActive(false);        
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        scoreText.text = score.ToString();
    }

    //void OnTriggerExit2D(Collider2D target)
    //{
    //	if (target.tag == "Bomb")
    //		SceneManagement.LoadScene(SceneManagement.GetActiveScene().buildIndex);
    
    void Activar()
    {
        //fin.SetActive(true);
        //Start();
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Lata")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290008;
            InsertBien(IdElemento);
        }

        if (target.tag == "Caja")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290009;
            InsertBien(IdElemento);


        }

        if (target.tag == "Botella_Vidrio")
        {
            Destroy(target.gameObject);
            score = score + 40; 
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290010;
            InsertBien(IdElemento);


        }

        if (target.tag == "Botella_Plastico")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290011;
            InsertBien(IdElemento);
        }

        if (target.tag == "Lata_pequeno")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290434;
            InsertBien(IdElemento);
        }

        if (target.tag == "Caja_pequeno")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290435;
            InsertBien(IdElemento);


        }

        if (target.tag == "Vidrio_pequeno")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290436;
            InsertBien(IdElemento);


        }

        if (target.tag == "Plastico_pequeno")
        {
            Destroy(target.gameObject);
            score = score + 40;
            counter.number += 40;
            text.text = counter.number.ToString();
            IdElemento = 290437;
            InsertBien(IdElemento);
        }
    }

    public void InsertBien(int IdElemento)
    {
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                          " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log();
        codigo.ExecuteNonQuery();
        CloseBD();
        //Debug.Log(IdElemento);
        //Debug.Log("--------bien ok-------------");
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
