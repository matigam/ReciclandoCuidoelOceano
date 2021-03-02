using UnityEngine;
using UnityEngine.UI;

public class SaveTest : MonoBehaviour
{
    public Text text;
    public Counter counter;

    
    void Update()
    {
        text.text = counter.number.ToString();
    }

    public void AddOne()
    {
        counter.number += 1;
        Debug.Log("---------AddOne-------------");
    }

    public void AdddePun()
    {
        counter.number += 1;
        text.text = counter.number.ToString();
        Debug.Log("---------AdddePun-------------");
    }
}
