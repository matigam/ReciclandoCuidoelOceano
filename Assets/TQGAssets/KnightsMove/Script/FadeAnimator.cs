using System.Collections;
using UnityEngine;

public class FadeAnimator : MonoBehaviour {
	
	private CanvasRenderer cr, ccr;
	private float fadeStep = 0.02f;
	private float repeatSeconds = 0.1f;
	
	void Start (){
		cr = GetComponent<CanvasRenderer>();
	}

	public void StartFade(){
		StopAllCoroutines();
		cr.SetAlpha(1f);
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			ccr = gameObject.transform.GetChild(i).GetComponent<CanvasRenderer>();
			if (ccr != null) {
				ccr.SetAlpha(1f);
			}
		}
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn(){
		while (cr.GetAlpha() < 1f) {
			Fade(true);
			yield return null;
		}
		if (cr.GetAlpha() >= 1f) {
			StartCoroutine(FadeOut());
		}
		else {
			yield return new WaitForSeconds(repeatSeconds);
		}
	}

	IEnumerator FadeOut(){
		while (cr.GetAlpha() > 0f) {
			Fade(false);
			yield return null;
		}
		if (cr.GetAlpha() <= 0f) {
			StartCoroutine(FadeIn());
		}
		else {
			yield return new WaitForSeconds(repeatSeconds);
		}
	}

	void Fade(bool fadeIn){
		cr.SetAlpha(cr.GetAlpha() + (fadeIn ? fadeStep : -fadeStep));
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			ccr = gameObject.transform.GetChild(i).GetComponent<CanvasRenderer>();
			if (ccr != null) {
				ccr.SetAlpha(ccr.GetAlpha() + (fadeIn ? fadeStep : -fadeStep));
			}
		}
	}
}
