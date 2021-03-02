using System.Collections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using UnityEngine;


public class UrlManager : MonoBehaviour
{

    MySqlCommand cod;
    MySqlDataReader reader;

    public GameObject web1;

    string link;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void sacarUrl()
    {
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		try
		{
			string consulta = " SELECT * FROM URL where id_ulearnet = 1 ";
			MySqlCommand comando = new MySqlCommand(consulta);
			comando.Connection = conectar;
			conectar.Open();
			reader = comando.ExecuteReader();
			while (reader.Read())
			{
				//Debug.Log(reader.GetString(1));
				//Debug.Log(reader.GetString(4));
				//web1.GetComponent<UnityEngine.UI.Text>().text = reader.GetString(1);
				string link = reader.GetString(1);
				Debug.Log(link);
				if (!reader.IsDBNull(1))
				{
					Application.OpenURL(link);
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

    public void Ulearnet(string link)
    {
        
    }

}
