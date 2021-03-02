
using UnityEngine;

public class STATIC{

    public static string NAME = ConexionBD.nombre;
    public static string OPPONENT_NAME = "";
    public static string GAME_NAME = ConexionBD.nombre;
    public static string JOINED_GAME = "";
    public static string PREF_NAME = "PrefName";
    public static string PREF_TEST = "PrefTestInt";
    public static string PREF_GAME = "PrefGame";
    public static string YOUR_TURN = "Juegas tu";
    public static string OPPONENT_TURN = "Espera tu turno";
    public static string YOU_WIN = "Excelente trabajo";
    public static string YOU_LOSE = "Excelente trabajo";

    

    // 0 empty, 1 can move, 2 busy, 3 last mov
    public static int STATE_EMPTY = 0;
    public static int STATE_CAN_MOVE = 1;
    public static int STATE_BUSY = 2;
    public static int STATE_LAST = 3;
    public static int STATE_WALL = 4;
    
    public static int FIELD_SIZE = 5;
    public static int WALL_SIZE = 5;
    
    public static bool PHONE_MODE = false;
    public static bool SINGLE_MODE = false;

    public static void GetName(){
        //        PlayerPrefs.DeleteAll();
        NAME = "Player " + Random.Range(200, 900);
        NAME = PlayerPrefs.GetString(PREF_NAME, NAME);
        //PREF_NAME = ConexionBD.usuario_nombre;
        //Debug.Log(PREF_NAME);
        //NAME = ConexionBD.usuario_nombre;
        ChangeName(NAME);

    }
    public static void ChangeName(string name){
        NAME = name;
        PlayerPrefs.SetString(PREF_NAME, name);
        PlayerPrefs.Save();
        //Debug.Log("-----------------");
        //Debug.Log(NAME);
        //Debug.Log(name);
        //Debug.Log(PREF_NAME);
        //Debug.Log("------------------");

    }
    public static void ChangeOppName(string name){
        OPPONENT_NAME = name;
    }

    public static void GetGameName(){
        GAME_NAME ="GAME " + Random.Range(200, 900);
        //GAME_NAME = ConexionBD.usuario;
        GAME_NAME = PlayerPrefs.GetString(PREF_GAME, GAME_NAME);
        // Debug.Log(GAME_NAME);
        
        ChangeGameName(GAME_NAME);
    }
    public static void ChangeGameName(string name){
        //Id_User = ConexionBD.usuario_nombre;
        GAME_NAME = name;
        //GAME_NAME = ConexionBD.usuario;
        PlayerPrefs.SetString(PREF_GAME, name);
        PlayerPrefs.Save();
        //GAME_NAME = ConexionBD.usuario_nombre;
        //Debug.Log(GAME_NAME);
    }
}
