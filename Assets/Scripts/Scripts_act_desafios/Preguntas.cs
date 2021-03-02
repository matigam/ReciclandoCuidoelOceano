using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preguntas : MonoBehaviour
{
	MySqlCommand cod;
	MySqlDataReader reader;
	public GameObject Imagen;
	public GameObject Imagen_alt_1;
	public GameObject Imagen_alt_2;
	public GameObject Imagen_alt_3;
	public GameObject Imagen_alt_4;
	public GameObject Pregunta;
	public GameObject Objetivo;
	public GameObject Respuesta1;
	public GameObject Respuesta2;
	public GameObject Respuesta3;
	public GameObject Respuesta4;
	public GameObject EsCorrecta1;
	public GameObject EsCorrecta2;
	public GameObject EsCorrecta3;
	public GameObject EsCorrecta4;
	List<string> alt = new List<string>();
	List<string> resp = new List<string>();
	List<string> imag = new List<string>();

	string idItem;
	string idImagen;
	int i = 0;
	int c = 0;
	int y = 0;
	//string Def = "lala";

	void Start()
	{
		i=0;
		pregunta();
		Alternativa();

	}
	public void pregunta()
	{
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		try
		{
		  string consulta = " SELECT * FROM item where reim_id = 206 ORDER BY rand() LIMIT 1 ";
		  MySqlCommand comando = new MySqlCommand(consulta);
		  comando.Connection = conectar;
		  conectar.Open();
		  reader = comando.ExecuteReader();
		  while (reader.Read())
		  {
				//Debug.Log(reader.GetString(1));
				//Debug.Log(reader.GetString(4));
				Pregunta.GetComponent<UnityEngine.UI.Text>().text = reader.GetString(1);
				Objetivo.GetComponent<UnityEngine.UI.Text>().text = reader.GetString(6);
				Debug.Log(reader.GetString(6));
				idItem = reader.GetString(0);
				//Debug.Log(idImagen);
				if (!reader.IsDBNull(4))
				{
					sacarFoto();
				}

				
		  }
		}
		catch (MySqlException ex)
		{
		Debug.Log(ex.Message); 
		}
		finally
		{
		conectar.Close();
		}
	}


    public void Alternativa()
	{
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		try
		{
		  string consulta = " SELECT i.idlaternativa, a.txt_alte, i.escorrecto, a.IMAGEN_idimagen FROM item_alt i, alternativa a where i.idlaternativa = a.idlaternativa AND i.ITEM_IdItem = '" +idItem+"'ORDER BY rand();";
		  MySqlCommand comando = new MySqlCommand(consulta);
		  comando.Connection = conectar;
		  conectar.Open();
		  reader = comando.ExecuteReader();
		  while (reader.Read())
		  {
				alt.Add(reader.GetString(1));
				resp.Add(reader.GetString(2));
				if (!reader.IsDBNull(3))
				{
					imag.Add(reader.GetString(3));
				}
				//imag.Add(reader.GetString(3));

			}
		  foreach(string a in alt)
		  {
		  	//Debug.Log("Estoy?"+ i);
		  	if(i == 0)
		  	{
				Respuesta1.GetComponent<UnityEngine.UI.Text>().text  = a;
			}

		  	if(i == 1)
		  	{
		  		Respuesta2.GetComponent<UnityEngine.UI.Text>().text  = a;
			}

		  	if(i == 2)
		  	{
		  		Respuesta3.GetComponent<UnityEngine.UI.Text>().text  = a;
		  	}

		  	if(i == 3)
		  	{
		  		Respuesta4.GetComponent<UnityEngine.UI.Text>().text  = a;
		  	}

		  	i++;
		  }
		  	foreach(string r in resp)
		  {
		  	if(c == 0)
		  	{
		  		EsCorrecta1.GetComponent<UnityEngine.UI.Text>().text  = r;
		  	}
		  	if(c == 1)
		  	{
		  		EsCorrecta2.GetComponent<UnityEngine.UI.Text>().text  = r;
		  	}
		  	if(c == 2)
		  	{
		  		EsCorrecta3.GetComponent<UnityEngine.UI.Text>().text  = r;
		  	}
		  	if(c == 3)
		  	{
		  		EsCorrecta4.GetComponent<UnityEngine.UI.Text>().text  = r;
		  	}
		  	c++;
		  }

			foreach (string im in imag)
			{
				
				if (y == 0)
				{
					sacarFoto_Alt_1(im);
				}
				if (y == 1)
				{
					sacarFoto_Alt_2(im);
				}
				if (y == 2)
				{
					sacarFoto_Alt_3(im);
				}
				if (y == 3)
				{
					sacarFoto_Alt_4(im);
				}
				y++;
			}





		}
		catch (MySqlException ex)
		{
		Debug.Log(ex.Message); 
		}
		finally
		{
		conectar.Close();
		}

		

	}

	public void sacarFoto()
	{
		var tex = new Texture2D(64, 64);
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		string consulta = " SELECT * FROM imagen where idimagen = " + reader.GetString(4);
		MySqlCommand comando = new MySqlCommand(consulta);
		//Debug.Log(consulta);
		comando.Connection = conectar;
		conectar.Open();
		reader = comando.ExecuteReader();
		//Debug.Log(reader.GetString(2));
		while (reader.Read())
		{
			byte[] img = (byte[])reader[2];
			tex.LoadImage(img);
			// image 266x199
			Imagen.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
		}


		conectar.Close();

	}

	public void sacarFoto_Alt_1(string im)
	{
		var tex = new Texture2D(64, 64);
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		string consulta = " SELECT * FROM imagen where idimagen = " + im;
		MySqlCommand comando = new MySqlCommand(consulta);
		//Debug.Log(consulta);
		comando.Connection = conectar;
		conectar.Open();
		reader = comando.ExecuteReader();
		while (reader.Read())
		{
			byte[] img = (byte[])reader[2];
			tex.LoadImage(img);
			// image 266x199
			Imagen_alt_1.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
			//Debug.Log(Imagen_alt_1);
		}


		conectar.Close();

	}

	public void sacarFoto_Alt_2(string im)
	{
		var tex = new Texture2D(64, 64);
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		string consulta = " SELECT * FROM imagen where idimagen = " + im;
		MySqlCommand comando = new MySqlCommand(consulta);
		//Debug.Log(consulta);
		comando.Connection = conectar;
		conectar.Open();
		reader = comando.ExecuteReader();
		while (reader.Read())
		{
			byte[] img = (byte[])reader[2];
			tex.LoadImage(img);
			// image 266x199
			Imagen_alt_2.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
			//Debug.Log(Imagen_alt_1);
		}


		conectar.Close();

	}

	public void sacarFoto_Alt_3(string im)
	{
		var tex = new Texture2D(64, 64);
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		string consulta = " SELECT * FROM imagen where idimagen = " + im;
		MySqlCommand comando = new MySqlCommand(consulta);
		//Debug.Log(consulta);
		comando.Connection = conectar;
		conectar.Open();
		reader = comando.ExecuteReader();
		while (reader.Read())
		{
			byte[] img = (byte[])reader[2];
			tex.LoadImage(img);
			// image 266x199
			Imagen_alt_3.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
			//Debug.Log(Imagen_alt_1);
		}


		conectar.Close();

	}

	public void sacarFoto_Alt_4(string im)
	{
		var tex = new Texture2D(64, 64);
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		string consulta = " SELECT * FROM imagen where idimagen = " + im;
		MySqlCommand comando = new MySqlCommand(consulta);
		//Debug.Log(consulta);
		comando.Connection = conectar;
		conectar.Open();
		reader = comando.ExecuteReader();
		while (reader.Read())
		{
			byte[] img = (byte[])reader[2];
			tex.LoadImage(img);
			// image 266x199
			Imagen_alt_4.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
			//Debug.Log(Imagen_alt_1);
		}


		conectar.Close();

	}




}
