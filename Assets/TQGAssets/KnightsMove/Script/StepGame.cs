using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using MySql.Data.MySqlClient;

public class StepGame : NetworkBehaviour {

	public Text textField, headText, playerName1, playerName2, scoreText1, scoreText2;
	public GameObject butLock, field, butPrefab, endView, animHorse;
	public Button butBack, butReplay;
	
	private static int maxPlayers = 2;
	private int[] walls;
	private int startPoint = -1;
	private int lastPoint, score1, score2;
	private int personalId = maxPlayers;
	private int compId = 1;
	private int horseAnimStepsDefault = 20;
	private int horseAnimSteps;
	private float fieldWidth;
	private Vector2 animStartPoint, animEndPoint;
	private bool gameStarted = false;
	private bool yourTurn = false;
	private bool isLose = false;
	private bool isHorseAnimated = false;
	private string scoreText = "Puntaje : ";
	private StepPlayer player;
	private List<int> compCanMoveList;

	public Text text;
	public Counter counter;

	private GameObject[] buts;
	private ButModel[] butModels;
	private int fieldSize = STATIC.FIELD_SIZE;
	private float butSize = 150f;
	private string oponente;

	public static int Id_User;
	public static int actividad_id = 9010;
	MySqlCommand codigo;
	MySqlConnection conn;
	public static string id_periodo = "202002";
	/// ID REIM EN BRUTO:
	public static string id_reim = "206";
	public static int movimientos = 0;

	MySqlDataReader reader;

	void Start (){
		movimientos = 0;
		score1 = score2 = 0;
		UpdateScore();
		butBack.onClick.AddListener(OnBackButton);
		butReplay.onClick.AddListener(OnReplayButton);
		endView.SetActive(false);
		lastPoint = startPoint;
		
		fieldWidth = butSize * fieldSize;

		buts = new GameObject[fieldSize * fieldSize];
		butModels = new ButModel[fieldSize * fieldSize];
		int index = 0;
		for (int y = 0; y < fieldSize; y++) {
			for (int x = 0; x < fieldSize; x++) {
				GameObject oneBut = new GameObject();
				oneBut = Instantiate(butPrefab, field.transform);
				Vector3 localPos = new Vector3(butSize * x, butSize * - y, 0);
				oneBut.transform.localPosition = localPos;
				ButModel model = oneBut.GetComponent<ButModel>();
				model.id = index;
				model.x = x;
				model.y = y;
				model.UpdateView();
				oneBut.GetComponent<Button>().onClick.AddListener(delegate{OnButtonClick(model);});
				buts[index] = oneBut;
				butModels[index] = model;
				index++;
			}
		}
		RectTransform rt = butLock.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(fieldWidth, fieldWidth);
		Vector3 fieldPosition = new Vector3(fieldWidth / -2, fieldWidth / 2, 0);
		butLock.transform.localPosition = fieldPosition;
		field.transform.localPosition = fieldPosition;
		
		if (STATIC.SINGLE_MODE) {
			personalId = 0;
			STATIC.OPPONENT_NAME = "Robot";
		}
		else {
			personalId = isServer ? 0 : 1;
			player = GetLocalPlayer();
			Debug.Log("Yo juego con: ", player);
		}

		playerName1.text = STATIC.NAME;
		//playerName1.text = ConexionBD.usuario_nombre;
		//Debug.Log(playerName1);
		playerName2.text = STATIC.OPPONENT_NAME;
	}

	private void StartAnimHorse(int move){
		isHorseAnimated = true;
		horseAnimSteps = 0;
		animHorse.SetActive(true);

		animEndPoint = new Vector2(
			field.transform.localPosition.x + (butModels[move].x * butSize) + (butSize / 2f),
			field.transform.localPosition.y - (butModels[move].y * butSize) - (butSize / 2f));

		if (animHorse.transform.localPosition.z < 0f) {
			animStartPoint = animEndPoint;
			animHorse.transform.localPosition = new Vector3(animEndPoint.x, animEndPoint.y, 0f);
		}

		foreach (ButModel model in butModels) {
			if (model.state == STATIC.STATE_CAN_MOVE) {
				model.state = STATIC.STATE_EMPTY;
			}
			if (model.state == STATIC.STATE_LAST) {
				model.state = STATIC.STATE_BUSY;
			}
			model.UpdateView();
		}

		StartCoroutine(AnimHorseEnumerator());
		
	}
	
	private void StopAnimHorse(){
		isHorseAnimated = false;
		animStartPoint = animEndPoint;
		animHorse.SetActive(false);
		SwitchLock();
	}
	IEnumerator AnimHorseEnumerator(){
		float nextX, nextY, difX, difY, stepPercent, scale;
		stepPercent = horseAnimSteps / (float)horseAnimStepsDefault;
		difX = animEndPoint.x - animStartPoint.x;
		difY = animEndPoint.y - animStartPoint.y;
		nextX = animStartPoint.x + difX * stepPercent;
		nextY = animStartPoint.y + difY * stepPercent;
		scale = 1f + (stepPercent < 0.5f ? stepPercent : 1f - stepPercent) * 3f;
		animHorse.transform.localScale = new Vector3(scale, scale, scale);
		animHorse.transform.localPosition = new Vector3(nextX, nextY, 0f);
		
		horseAnimSteps++;
		
		yield return new WaitForSeconds(0.01f);
		
		if (horseAnimSteps > horseAnimStepsDefault) {
			StopAnimHorse();
		}
		else {
			StartCoroutine(AnimHorseEnumerator());
		}
	}
	
	
	
	// SINGLE PLAYER

	public void SingleGameWalls(){
		int[] walls = new int[STATIC.WALL_SIZE];
		for (int i = 0; i < STATIC.WALL_SIZE; i++) {
			walls[i] = Random.Range(0, STATIC.FIELD_SIZE * STATIC.FIELD_SIZE);
		}
//			SetWhoFirst(Random.Range(0, 2), walls);
		SetWhoFirst(0, walls);
	}

	IEnumerator CompMove(){
		yield return new WaitForSeconds(1f);
		if (compCanMoveList.Count > 0) {
			int compIndex = compCanMoveList[Random.Range(0, compCanMoveList.Count)];
			SetMove(1, compIndex, "");
		}
		else {
			YouWin(personalId);
		}
	}
	// // //

	StepPlayer GetLocalPlayer(){
		StepPlayer sp = null;
		GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < go.Length; i++) {
			if (go[i].GetComponent<StepPlayer>().isLocalPlayer) {
				sp = go[i].GetComponent<StepPlayer>();
			}
		}
		return sp;
	}
	
	void OnBackButton(){
		if (STATIC.SINGLE_MODE) {
			STATIC.SINGLE_MODE = false;
			SceneManager.LoadScene("StepLobby", LoadSceneMode.Single);
		}
		else {
			player.BackToLobby();
		}
	}
	
	void OnReplayButton(){
		if (STATIC.SINGLE_MODE) {
			Debug.Log("Yo repetí");
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
							  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + 300008 + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			//Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
			//                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Replay();
		}
		else {
			player.Replay();
		}
	}
	
	void OnButtonClick(ButModel model){
		Debug.Log("Yo movi");
		Id_User = ConexionBD.idUsuario;
		conn = ConnectDB();
		codigo = conn.CreateCommand();
		codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
						  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + 300017 + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + 1 + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
		//Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
		//                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
		codigo.ExecuteNonQuery();
		CloseBD();
		movimientos = movimientos + 1;
		SendMove(model.id);
	}

	public void SendMove(int index){
		if (yourTurn && butModels[index].state == STATIC.STATE_CAN_MOVE) {
			yourTurn = false;
			if (STATIC.SINGLE_MODE) {
				SetMove(personalId, index, "");
			}
			else {
				player.SentMove(personalId, index);
			}
			
		}
	}

	public void SetMove(int id, int move, string mess){
		lastPoint = move;
		StartAnimHorse(move);
		SwitchMove(id);
	}

	public void YouWin(int id){
		headText.text = id == personalId ? STATIC.YOU_WIN : STATIC.YOU_LOSE;
		if (id == personalId) {
			//Debug.Log("Yo gané");
			//counter.number += 200;
			//text.text = counter.number.ToString();
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
							  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + 300006 + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + 1 + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			//Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
			//                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log(STATIC.OPPONENT_NAME);
			string oponente = STATIC.OPPONENT_NAME;
			Oponente(oponente);
			score1++;

		}
		else {
			//Debug.Log("Yo perdí");
			Id_User = ConexionBD.idUsuario;
			conn = ConnectDB();
			codigo = conn.CreateCommand();
			codigo.CommandText = ("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
							  " VALUES (" + id_periodo + ", " + Id_User + ", " + id_reim + ", " + actividad_id + "," + 300007 + ", " + Convert.ToInt32(Input.mousePosition.x) + ", " + Convert.ToInt32(Input.mousePosition.y) + ", " + 0 + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
			//Debug.Log("INSERT INTO ulearnet_reim_pilotaje.alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+
			//                      " VALUES ("+"202002"+", "+"1162"+", "+"206"+", "+"9001"+","+"1"+", "+Input.mousePosition.x+", "+Input.mousePosition.y+", "+"1"+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"'"+")");
			codigo.ExecuteNonQuery();
			CloseBD();
			Debug.Log(STATIC.OPPONENT_NAME);
			string oponente = STATIC.OPPONENT_NAME;
			Oponente(oponente);
			score2++;
		}
		UpdateScore();
		endView.SetActive(true);
		butLock.SetActive(true);
	}

	public void AddText(string mess){
		textField.text += "\n" + mess;
	}
	public void SetWhoFirst(int id, int[] walls){
		animHorse.transform.localPosition = new Vector3(0f, 0f, -1000f);
		this.walls = walls;
		gameStarted = true;
		yourTurn = id == personalId;
		SwitchLock();
	}

	private void SwitchMove(int id){
		yourTurn = id != personalId;
//		SwitchLock();
	}

	private void SwitchLock(){
		butLock.SetActive(!yourTurn);
		headText.text = yourTurn ? STATIC.YOUR_TURN : STATIC.OPPONENT_TURN;
		isLose = true;
		
		foreach (ButModel model in butModels) {
			CanPutTheWall(model.id);
			if (model.state != STATIC.STATE_WALL) {
				if (lastPoint == startPoint) {
					model.state = yourTurn ? STATIC.STATE_CAN_MOVE : STATIC.STATE_EMPTY;
					isLose = false;
				}
				else {
					if (model.id == lastPoint) {
						model.state = STATIC.STATE_LAST;
					}

					if (model.state == STATIC.STATE_LAST && model.id != lastPoint) {
						model.state = STATIC.STATE_BUSY;
					}

					if (yourTurn) {
						CanMoveState();
					}
					else if (model.state != STATIC.STATE_LAST && model.state != STATIC.STATE_BUSY) {
						model.state = STATIC.STATE_EMPTY;
						isLose = false;
						
					}
					
					if (STATIC.SINGLE_MODE) {
						if (model.state == STATIC.STATE_CAN_MOVE) {
							model.state = STATIC.STATE_EMPTY;
						}
						CanMoveState();
					}
				}
			}

			model.UpdateView();
			
		}
		
		if (isLose) {
			if (STATIC.SINGLE_MODE) {
				YouWin(compId);
				//counter.number += 200;
				//text.text = counter.number.ToString();
				//Debug.Log("yo gané");
				Debug.Log(STATIC.OPPONENT_NAME);
			}
			else {
				player.YouWin(personalId == 0 ? 1 : 0);
				//Debug.Log("yo perdí");
				Debug.Log(STATIC.OPPONENT_NAME);
			}
		}
		else {
			if (STATIC.SINGLE_MODE && !yourTurn) {
				StartCoroutine(CompMove());
			}
		}
	}

	private void CanPutTheWall(int index){
		for (int i = 0; i < walls.Length; i++) {
			if (walls[i] == index) {
				butModels[index].state = STATIC.STATE_WALL;
			}
		}
	}

	private void CanMoveState(){
		int[] schemX = {1, 2, 2, 1, -1, -2, -2, -1};
		int[] schemY = {-2, -1, 1, 2, 2, 1, -1, -2};
		compCanMoveList = new List<int>();
		for (int i = 0; i < schemX.Length; i++) {
			int index = GetIndexByPos(butModels[lastPoint].x + schemX[i], butModels[lastPoint].y + schemY[i]);
			if (index >= 0) {
				if (butModels[index].state != STATIC.STATE_LAST 
				    && butModels[index].state != STATIC.STATE_BUSY
				    && butModels[index].state != STATIC.STATE_WALL) {
						butModels[index].state = STATIC.STATE_CAN_MOVE;
						compCanMoveList.Add(index);
						isLose = false;
				}
			}
		}
	}

	private int GetIndexByPos(int x, int y){
		int index = -1;
		if (x >= 0 && x < fieldSize && y >= 0 && y < fieldSize) {
			index = y * fieldSize + x;
		}
		return index;
	}
	
	public void Replay(){
		endView.SetActive(false);
		lastPoint = startPoint;
		foreach (var model in butModels) {
			model.state = STATIC.STATE_EMPTY;
			model.UpdateView();
		}

		if (STATIC.SINGLE_MODE) {
			SingleGameWalls();
		}
		else {
			player.SetupGame();
		}
		
	}

	private void UpdateScore(){
		scoreText1.text = scoreText + score1;
		scoreText2.text = scoreText + score2;
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

	public void Oponente(string oponente)
	{
		string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
		MySqlConnection conectar = new MySqlConnection(datosConexion);
		try
		{
			Debug.Log(oponente);
			string consulta = " SELECT * FROM usuario where username = "+ oponente;
			MySqlCommand comando = new MySqlCommand(consulta);
			comando.Connection = conectar;
			conectar.Open();
			reader = comando.ExecuteReader();
			Debug.Log(consulta);
			while (reader.Read())
			{
				//Debug.Log(reader.GetString(1));
				//Debug.Log(reader.GetString(4));
				//Pregunta.GetComponent<UnityEngine.UI.Text>().text = reader.GetString(1);
				//Objetivo.GetComponent<UnityEngine.UI.Text>().text = reader.GetString(6);
				Debug.Log(reader.GetString(0));
				//idItem = reader.GetString(0);
				//Debug.Log(idImagen);
				if (!reader.IsDBNull(4))
				{
					//sacarFoto();
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
}
