using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_2 : MonoBehaviour
{
    public GameObject Panel;
    //public GameObject Panel_Pause;
    private string Cambio_Actividad;
    public Animator anim;
    // Start is called before the first frame update
    public void Info()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
        }
    }

    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            //Time.timeScale = 0;
            //Panel_Pause.SetActive(true);
        }
        else
        {
            //Time.timeScale = 1;
            //Panel_Pause.SetActive(false);
        }
    }

    public void Exit_act_2()
    {
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("MenuActividades");
    }

    IEnumerator LoadScene(string Cambio_Actividad)
    {
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }
}
