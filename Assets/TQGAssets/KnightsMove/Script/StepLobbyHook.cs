using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class StepLobbyHook : LobbyHook {

	public GameObject game;
	
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer){
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		StepPlayer localPlayer = gamePlayer.GetComponent<StepPlayer>();

		localPlayer.pid = lobby.netId.Value - 1;
		localPlayer.pname = lobby.playerName;
		
		print("NOMBRE : " + lobby.playerName);
		
//		game = GameObject.Find("Game");
//		game.GetComponent<StepGame>().personalId = 0;
	}
}
