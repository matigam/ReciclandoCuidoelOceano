
using UnityEngine;
using UnityEngine.UI;

public class ButModel : MonoBehaviour{

	public Image canmove, buzy, lastmove, wall;
	public FadeAnimator fader;

	public int id = 0;
	public int x = 0;
	public int y = 0;
	public int state = STATIC.STATE_EMPTY;

	public void UpdateView(){
		canmove.enabled = state == STATIC.STATE_CAN_MOVE;
		buzy.enabled = state == STATIC.STATE_BUSY;
		lastmove.enabled = state == STATIC.STATE_LAST;
		wall.enabled = state == STATIC.STATE_WALL;

		if (state == STATIC.STATE_CAN_MOVE) {
			fader.StartFade();
		}
	}
}
