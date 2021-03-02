using MySql.Data.MySqlClient;
using UnityEngine;
using System;

public class Puntaje_mal : MonoBehaviour
{
	//public Text scoreText;

	private int score;
    private int IdElemento;
    //public GameObject fin;

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


    void Update()
    {
        //scoreText.text = score.ToString();

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
    	if(target.tag == "Lata")
    	{
            
    		Destroy(target.gameObject);
            IdElemento = 290009;
            InsertMal(IdElemento);

        }
        if(target.tag == "Caja")
        {
            
            Destroy(target.gameObject);
            IdElemento = 290009;
            InsertMal(IdElemento);

        }
        if(target.tag == "Botella_Vidrio")
        {
            
            Destroy(target.gameObject);
            IdElemento = 290010;
            InsertMal(IdElemento);

        }
        if(target.tag == "Botella_Plastico")
        {
           
            Destroy(target.gameObject);
            IdElemento = 290011;
            InsertMal(IdElemento);
        }
        if (target.tag == "Lata_pequeno")
        {
            Destroy(target.gameObject);
            //score = score + 1;
            //counter.number += 1;
            //text.text = counter.number.ToString();
            IdElemento = 290434;
            InsertMal(IdElemento);
        }

        if (target.tag == "Caja_pequeno")
        {
            Destroy(target.gameObject);
            //score = score + 1;
            //counter.number += 1;
            //text.text = counter.number.ToString();
            IdElemento = 290435;
            InsertMal(IdElemento);


        }

        if (target.tag == "Vidrio_pequeno")
        {
            Destroy(target.gameObject);
            //score = score + 1;
            //counter.number += 1;
            //text.text = counter.number.ToString();
            IdElemento = 290436;
            InsertMal(IdElemento);


        }

        if (target.tag == "Plastico_pequeno")
        {
            Destroy(target.gameObject);
            //score = score + 1;
           // counter.number += 1;
            //text.text = counter.number.ToString();
            IdElemento = 290437;
            InsertMal(IdElemento);
        }

    }

    public void InsertMal(int IdElemento)
    {
        Id_User = ConexionBD.idUsuario;
        conn = ConnectDB();
        codigo = conn.CreateCommand();
        codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                          " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + IdElemento + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "0" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        //Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
        //                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
        codigo.ExecuteNonQuery();
        CloseBD();
        //Debug.Log(IdElemento);
        //Debug.Log("--------mal ok-------------");
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
