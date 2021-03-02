using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio_Scena : MonoBehaviour
{
    public Animator anim;
    public string SceneName;


    public void CambioBTN()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }
}
