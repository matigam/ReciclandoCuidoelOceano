using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using MySql.Data.MySqlClient;
using System;
using System.Collections;

public class StepPlayer : NetworkBehaviour {

	[SyncVar] public uint pid;
	[SyncVar] public string pname = "ppp";

	public StepGame stepGame;

	/// PERIODO OBTENIDO EN BRUTO:
	public static string id_periodo = "202002";
	/// ID REIM EN BRUTO:
	public static string id_reim = "206";
	MySqlCommand codigo;
	MySqlConnection conn;
	// Start is called before the first frame update
	public static int Id_User;
	public static string actividad = "9010";

	private void Update(){
		if (stepGame == null) {
			stepGame = GameObject.FindGameObjectWithTag("Game").GetComponent<StepGame>();
			SetupGame();
		}
	}

	public void SetupGame(){
		if (!isServer) {
			CmdWhoFirst();
		}
	}

	public void SentMove(int id, int index){
		if (isServer) {
			RpcMove(id, index, "move : " +  index);
		}
		else {
			CmdMove(id, index, "move : " +  index);
		}
	}

	public void YouWin(int id){
		if (isServer) {
			RpcWin(id);
		}
		else {
			CmdWin(id);
		}
	}

	public void Replay(){
		if (isServer) {
			RpcReplay();
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
								  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "300008" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "0" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log("repetir_online");
		}
		else {
			CmdReplay();
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
								  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "300009" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log("repetir_offline");
		}
	}

	public void BackToLobby(){
		if (isServer) {
			GameObject.Find("StepLobbyManager").GetComponent<LobbyManager>().ServerReturnToLobby();
			RpcBack();
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
								  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "290012" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "0" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log("atras_online");
		}
		else {
			CmdBack();
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
								  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad + "," + "300010" + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log("atras_online");
		}
	}
	
	[Command]
	public void CmdWhoFirst(){
		int[] walls = new int[STATIC.WALL_SIZE];
		for (int i = 0; i < STATIC.WALL_SIZE; i++) {
			walls[i] = UnityEngine.Random.Range(0, STATIC.FIELD_SIZE * STATIC.FIELD_SIZE);
		}
		RpcWhoFirst(UnityEngine.Random.Range(0,2), walls);
	}
	[ClientRpc]
	public void RpcWhoFirst(int id, int[] walls){
		stepGame.SetWhoFirst(id, walls);
	}
	
	[Command]
	public void CmdMove(int id, int move, string mess){
		RpcMove(id, move, mess);
	}
	[ClientRpc]
	public void RpcMove(int id, int move, string mess){
		stepGame.SetMove(id, move, mess);
	}
	
	[Command]
	public void CmdWin(int id){
		//Debug.Log("Yo gane");
		RpcWin(id);
		
	}
	[ClientRpc]
	public void RpcWin(int id){
		//Debug.Log("Yo gane");
		stepGame.YouWin(id);
	}
	
	[Command]
	public void CmdReplay(){
		RpcReplay();
	}
	[ClientRpc]
	public void RpcReplay(){
		stepGame.Replay();
	}
	
	[Command]
	public void CmdBack(){
		GameObject.Find("StepLobbyManager").GetComponent<LobbyManager>().ServerReturnToLobby();
		RpcBack();
	}
	[ClientRpc]
	public void RpcBack(){
		
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
