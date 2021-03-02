using MySql.Data.MySqlClient;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class boton_act_1 : MonoBehaviour
{
    
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "206";
    /// 
    private static string database = "ulearnet_reim_pilotaje";
    MySqlCommand codigo;
    MySqlConnection conn;

    private string Cambio_Actividad;
    public Animator anim;
    //public static Timer_DB_Act_1 timer_DB_Act_1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void Review_de_act_1 (){
        //timer_DB_Act_1.InsertFinActividad();
        SceneManager.LoadScene("MenuActividades");  
        
     } 
     public void Before_Instruc_1 (){
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("MenuActividades");
    }
     public void After_Instruc_1 (){
        Cambio_Actividad = "Actividad_1";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("Actividad_1");
    }
     public void Before_Instruc_2 (){
        SceneManager.LoadScene("Instruccion_1_act_1");

    }
     public void After_Instruc_2 (){
        SceneManager.LoadScene("Actividad_1");
    }

    public void ir_a_menu()
    {
        SceneManager.LoadScene("MenuActividades");
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

    IEnumerator LoadScene(string Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }
}
     

