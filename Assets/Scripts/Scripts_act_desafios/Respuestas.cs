using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;

public class Respuestas : MonoBehaviour
{
	public GameObject Result;
	public GameObject Bien;
	public GameObject Mal;
	public GameObject a1;
	public GameObject a2;
	public GameObject a3;
	public GameObject a4;
	public GameObject Objetivo;

	private int IdElemento;

	public ConexionBD Connect;
	public static int Id_User;
	public static int actividad_id = 9009;

	/// PERIODO OBTENIDO EN BRUTO:
	public static string id_periodo = "202002";
	/// ID REIM EN BRUTO:
	public static string id_reim = "206";
	/// 
	MySqlCommand codigo;
	MySqlConnection conn;

	private static string database = "ulearnet_reim_pilotaje";
	public static int id_tiempoxActividad_Actual;

	public void Start()
	{
		//conexionBD.InsertInicioActividad(actividad_id);
		InsertInicioActividad(actividad_id);
		Debug.Log("Inicio actividad Desafios");
	}
	public void Btn1()
	{
		Preguntas question = GetComponent<Preguntas>();

		if(a1.GetComponent<UnityEngine.UI.Text>().text == "1")
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Correcta";
			Bien.SetActive(true);
			IdElemento = 300015;
			InsertBien(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			SceneManager.LoadScene("Quiz");
			
		}
		else
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Incorrecta";
			Mal.SetActive(true);
			IdElemento = 300016;
			InsertMal(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			InsertFinActividad();
			//SceneManager.LoadScene("MenuActividades");
		}
	}

	public void Btn2()
	{
		Preguntas question = GetComponent<Preguntas>();

		if(a2.GetComponent<UnityEngine.UI.Text>().text == "1")
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Correcta";
			Bien.SetActive(true);
			IdElemento = 300015;
			InsertBien(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			SceneManager.LoadScene("Quiz");
		}
		else
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Incorrecta";
			Mal.SetActive(true);
			IdElemento = 300016;
			InsertMal(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			InsertFinActividad();
			//SceneManager.LoadScene("MenuActividades");

		}

	}

	public void Btn3()
	{
		Preguntas question = GetComponent<Preguntas>();

		if(a3.GetComponent<UnityEngine.UI.Text>().text == "1")
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Correcta";
			Bien.SetActive(true);
			IdElemento = 300015;
			InsertBien(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			SceneManager.LoadScene("Quiz");
		}
		else
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Incorrecta";
			Mal.SetActive(true);
			IdElemento = 300016;
			InsertMal(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			InsertFinActividad();
			//SceneManager.LoadScene("MenuActividades");

		}

	}

	public void Btn4()
	{
		Preguntas question = GetComponent<Preguntas>();

		if(a4.GetComponent<UnityEngine.UI.Text>().text =="1")
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Correcta";
			Bien.SetActive(true);
			IdElemento = 300015;
			InsertBien(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			SceneManager.LoadScene("Quiz");
		}
		else
		{
			Result.GetComponent<UnityEngine.UI.Text>().text = "Incorrecta";
			Mal.SetActive(true);
			IdElemento = 300016;
			InsertMal(IdElemento);
			Debug.Log(Objetivo.GetComponent<UnityEngine.UI.Text>().text);
			InsertFinActividad();
			//SceneManager.LoadScene("MenuActividades");

		}
	}

	public void InsertBien(int IdElemento)
	{
		Id_User = ConexionBD.idUsuario;
		conn = ConnectDB();
		codigo = conn.CreateCommand();
		codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
						  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + Objetivo.GetComponent<UnityEngine.UI.Text>().text + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
		//Debug.Log();
		codigo.ExecuteNonQuery();
		CloseBD();
		//Debug.Log(IdElemento);
		//Debug.Log("--------bien ok-------------");
	}

	public void InsertMal(int IdElemento)
	{
		Id_User = ConexionBD.idUsuario;
		conn = ConnectDB();
		codigo = conn.CreateCommand();
		codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
						  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + Objetivo.GetComponent<UnityEngine.UI.Text>().text + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
		//Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
		//                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
		codigo.ExecuteNonQuery();
		CloseBD();
		//Debug.Log(IdElemento);
		//Debug.Log("--------mal ok-------------");
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
}
