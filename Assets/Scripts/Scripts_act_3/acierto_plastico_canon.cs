﻿using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class acierto_plastico_canon : MonoBehaviour
{

    public int score;
    private static string database = "ulearnet_reim_pilotaje";
    public Text text;
    public Counter counter;
    private int IdElemento = 290037;
    public static ConexionBD Connect;
    public static int Id_User;
    public static int actividad_id = 9002;
    public static int id_tiempoxActividad_Actual;
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";

    MySqlCommand codigo;
    MySqlConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D target)
    {
    	if(target.tag == "Botella_Plastico")
    	{
            Destroy(target.gameObject);
            //InsertBien();
            Sumar();
            SceneManager.LoadScene("Instruccion_1_act_3");
        }        
        
    }
    void Sumar()
    {
        //Debug.Log("---------lalalal-------------");
        counter.number += 200;
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
        //Debug.Log(IdElemento);
        //Debug.Log("--------bien ok-------------");
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
