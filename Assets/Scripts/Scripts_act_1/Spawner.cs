using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject[] elementos;
	public float xBounds, yBound;

    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
    	yield return new WaitForSeconds(Random.Range(1, 2));

    	int randomBasura = Random.Range(0, elementos.Length);

    	if(Random.value <= .6f)
    		Instantiate(elementos[randomBasura],
    			new Vector2(Random.Range(-xBounds, xBounds), yBound), Quaternion.identity);

    	StartCoroutine(SpawnRandomGameObject());

    }

}
