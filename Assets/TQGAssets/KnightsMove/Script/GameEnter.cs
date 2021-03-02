using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnter : MonoBehaviour {

	public Button nextBut;
	public InputField nameField;
	
	private void Awake(){
		
		STATIC.GetName();
		STATIC.GetGameName();
		
		
		STATIC.PHONE_MODE = Application.platform == RuntimePlatform.Android 
		                    || Application.platform == RuntimePlatform.IPhonePlayer;

		if (STATIC.PHONE_MODE)
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}

	private void Start(){
		nextBut.onClick.AddListener(OnNext);
		nameField.text = STATIC.NAME;
	}

	private void Update(){
		nextBut.interactable = nameField.text != "";
	}

	private void OnNext(){
		STATIC.ChangeName(nameField.text);
		SceneManager.LoadScene("StepLobby", LoadSceneMode.Single);
	}
	
}
