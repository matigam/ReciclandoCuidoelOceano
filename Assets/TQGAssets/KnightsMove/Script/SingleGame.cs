using UnityEngine;

public class SingleGame : MonoBehaviour {

	public GameObject game;
	private int index = 0;

	private void Awake(){
		STATIC.SINGLE_MODE = true;
	}

	private void Update(){
		if (index > 2 || !STATIC.SINGLE_MODE) {
			return;
		}
		if (index == 1) {
			game.SetActive(true);
		}
		if (index == 2) {
			game.GetComponent<StepGame>().SingleGameWalls();
		}
		index++;
	}
}
