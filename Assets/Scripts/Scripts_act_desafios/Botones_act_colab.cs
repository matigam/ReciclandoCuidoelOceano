﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones_act_colab : MonoBehaviour
{
    private string Cambio_Actividad;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Siguiente()
    {
        Cambio_Actividad = "Quiz";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("Quiz");
    }

    public void Menu()
    {
        Cambio_Actividad = "MenuActividades";
        StartCoroutine(LoadScene(Cambio_Actividad));
        //SceneManager.LoadScene("MenuActividades");
    }

    IEnumerator LoadScene(string Cambio_Actividad)
    {
        //anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Cambio_Actividad);
    }
}
