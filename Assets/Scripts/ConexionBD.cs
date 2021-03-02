using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;

public class ConexionBD : MonoBehaviour
{
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    public static int id_tiempoxActividad_Actual;
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    public InputField userInput;
    public InputField passwordInput;
    public Text usuariopassfail;
    bool activador;

    public static int idUsuario;
    public static string usuario_nombre;
    public static string idSesion;
    public static string usuario;
    private string password;
    private int id_session;
    public string sceneToChange;
    private static string idButtonPressed = null;
    private static string database = "ulearnet_reim_pilotaje";
    public Animator anim;
    public static bool inActivity = false;
    public static string nombre;
    //public static string usuario;


    public static List<string> Querys = new List<string>();
    bool isDone = true;
    /*
    public void Login()
    {
        Usuario usuario;
        usuario = new Usuario();
        usuario.loginname = username.text;
        usuario.password = password.text;
        StartCoroutine(Post(usuario));
    }
    */

    // Update is called once per frame
    void Update()
    {


        if (isDone)
        {
            EnviadorQuerys(Querys);

        }

    }


    private async void EnviadorQuerys(List<string> Querys)
    {

        isDone = false;
        await Task.Run(() =>
        {
            foreach (var query in Querys.ToList())
            {
                conn = ConnectDB();

                codigo = conn.CreateCommand();
                codigo.CommandText = (query);
                codigo.ExecuteNonQuery();
                CloseBD();
                Querys.Remove(query);
            }
        }
        );

        isDone = true;
    }


    public void InsertTouch(int id_elemento)
    {
        Querys.Add("INSERT INTO " + database + ".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + idUsuario + ", " + id_reim + ", " + "1" + "," + id_elemento + ", " + System.Convert.ToInt32(Input.mousePosition.x) + ", " + System.Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
        
    }

    public void ElementoReciclado(int id_elemento, int actividad, int correcta, int zona, int tipo)
    {
        string Query =("INSERT INTO " + database + ".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + idUsuario + ", " + id_reim + ", " + actividad + "," + id_elemento + ", "+ zona + ", "+ tipo + ", " + correcta + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");

        Querys.Add(Query);
    }

    public void SendSesionOpen(int idUsuario){

    	idSesion = idUsuario+"-"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Querys.Add("INSERT INTO " +database+".asigna_reim_alumno (sesion_id, usuario_id, periodo_id, reim_id, datetime_inicio, datetime_termino)"+" VALUES ('"+idSesion+"', "+idUsuario+",   "+id_periodo+", "+id_reim+", '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"' "+")");
    	
        
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

    public void OnApplicationQuit()
    {
        
    	conn = ConnectDB();
    	string SQLQuery = "UPDATE "+database+".asigna_reim_alumno SET  datetime_termino = '"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' WHERE sesion_id = '"+idSesion+"'";
    	codigo = conn.CreateCommand();
    	codigo.CommandText = (SQLQuery);
    	
    	codigo.ExecuteNonQuery();
    	CloseBD();
        if (inActivity)
        {
            InsertFinActividad();
        }
    	
    }

	
    public MySqlConnection ConnectDB()
    {
    	string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database="+database+";";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();

       
        return conectar;
    }

    public string getNombre()
    {
        return nombre;
    }
    
    
    public void inicioSesion()
    {
        ////StartCoroutine(LoadLevel(sceneToChange));
        usuario = userInput.GetComponent<InputField>().text;
        password = passwordInput.GetComponent<InputField>().text;
        string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database="+database+";";
        MySqlConnection conectar = new MySqlConnection(datosConexion);
        conectar.Open();

        MySqlCommand codigo = new MySqlCommand();
        codigo.Connection = conectar;
        
        codigo.CommandText = ("SELECT id, nombres, apellido_paterno FROM "+database+".usuario WHERE username = '" + usuario + "' AND password = '" + password + "'");
        
  

        MySqlDataReader leer = codigo.ExecuteReader();
        bool returned = false;
        if (leer.HasRows)
        {
	        while (leer.Read()){
                idUsuario = leer.GetInt32(0);
                nombre = leer.GetString(1) + " " + leer.GetString(2);

                returned = true;
	        
	        }
    	}
       
	    if (returned)
	    {
	        Debug.Log("Bienvenido a Ulearnet "+usuario);
            SendSesionOpen(idUsuario);
            StartCoroutine(LoadLevel(sceneToChange));
        }
	    else
	    {
	            
	        usuariopassfail.gameObject.SetActive(true);
	        Invoke("activarmensaje", 3f);
	            //Debug.Log("Usuario o contraseña incorrectos");
	    }

        conectar.Close();
        
    }

    

    public void activarmensaje()
    {
        usuariopassfail.gameObject.SetActive(false);
    }

    public static void SetidButtonPressed(string value) 
    {
    	idButtonPressed = value;
    }

    public static int GetIdUsuario()
    {
        return idUsuario;
    }

    public static string GetIdReim()
    {
        return id_reim;
    }

    public void InsertInicioActividad(int actividad_id)
    {

        inActivity = true;
        InsertInicioActividadAync("INSERT INTO " +database+".tiempoxactividad (inicio, final, causa, usuario_id, reim_id, actividad_id)"+" VALUES ('"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', 0,   "+idUsuario+", "+id_reim+", "+actividad_id+")");
        
    }

    public async void InsertInicioActividadAync(string query)
    {
        await Task.Run(() =>
        {

            MySqlConnection conn = ConnectDB();
            MySqlCommand codigo = conn.CreateCommand();
            codigo.CommandText = (query);

            codigo.ExecuteNonQuery();

            query = "SELECT LAST_INSERT_ID();";
            codigo.CommandText = (query);

            MySqlDataReader lectura = codigo.ExecuteReader();
            if (lectura.HasRows)
            {
                while (lectura.Read())
                {
                    lectura.Read();
                    id_tiempoxActividad_Actual = lectura.GetInt32(0);


                }
            }
            CloseBD();

        });
        
    }

    public void InsertFinActividad()
    {
        MySqlConnection conn = ConnectDB();
        string SQLQuery = "UPDATE  "+database+".tiempoxactividad SET final = '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"' WHERE id = "+id_tiempoxActividad_Actual+"";
        MySqlCommand codigo = conn.CreateCommand();
        codigo.CommandText = (SQLQuery);
        codigo.ExecuteNonQuery();
    }

    public void InsertFinActividadAsync()
    {
        
        Querys.Add("UPDATE  " + database + ".tiempoxactividad SET final = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = " + id_tiempoxActividad_Actual + "");
        inActivity = false;
    }

    public async void InsertMovement(int MovementX, int MovementY, int Correcta, int IDActividad, int IDElemento, string time){
        await Task.Run(() =>
        { 
    	MySqlConnection conn = ConnectDB();
    	string SQLQuery = "INSERT INTO "+database+".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+" VALUES ("+id_periodo+", "+idUsuario+", "+id_reim+", "+IDActividad+","+IDElemento+", "+MovementX*1000+", "+MovementY*1000+", "+Correcta+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"' )";
   		//string SQLQuery = "INSERT INTO "+database+".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+" VALUES (202002, 1160, 201, 1,"+IDElemento+", "+MovementX*1000+", "+MovementY*1000+", "+Correcta+", "+"'"+time+"' )";
        MySqlCommand codigo = conn.CreateCommand();
   		codigo.CommandText = (SQLQuery);
    	codigo.ExecuteNonQuery();
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
        );
    }

    IEnumerator LoadLevel (string leveltoload)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(leveltoload, LoadSceneMode.Single);

    }
    /*
    public IEnumerator Post(Usuario usuario)
    {
        string url = "192.168.1.7" + "/login";
        var jsonData = JsonUtility.ToJson(usuario);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if (result != null)
                    {
                        var usuarios = JsonUtility.FromJson<Usuario>(result);
                        texto.text = "Bienvenido : " + usuario.nombre;
                    }
                }
            }
        }

    }*/
}