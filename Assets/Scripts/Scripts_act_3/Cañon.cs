using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cañon : MonoBehaviour
{

	public GameObject CañonCabeza;
	public Camera CamaraDeJuego;
	public GameObject LataPrefab;
	public float FuerzaDeDisparo;
    public static bool button_control = true;
    public static bool lanzar = true;
    // Start is called before the first frame update
    void Start()
    {
        button_control = true;
        lanzar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (button_control == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 PosicionDelMouse = Input.mousePosition;
                Vector3 PosicionGlobal = CamaraDeJuego.ScreenToWorldPoint(new Vector3(
                    PosicionDelMouse.x,
                    PosicionDelMouse.y,
                    transform.position.z - CamaraDeJuego.transform.position.z));

                CañonCabeza.transform.localEulerAngles = new Vector3(
                    CañonCabeza.transform.localEulerAngles.x,
                    CañonCabeza.transform.localEulerAngles.y,
                    Mathf.Atan2((PosicionGlobal.y - CañonCabeza.transform.position.y),
                                (PosicionGlobal.x - CañonCabeza.transform.position.x)) * Mathf.Rad2Deg
                    );

            }
        }
        
        
        
	}

    public void Lanzar()
    {
        if (lanzar == true)
        {
        button_control = false;
        GameObject ObjetoLata = Instantiate(LataPrefab);
        ObjetoLata.transform.position = CañonCabeza.transform.position;
        ObjetoLata.GetComponent<Rigidbody2D>().AddForce(CañonCabeza.transform.right * FuerzaDeDisparo);
        ObjetoLata.transform.SetParent(this.transform);
        lanzar = false;
        }
    }
	



}
