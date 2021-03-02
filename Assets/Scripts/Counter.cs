using UnityEngine;

[CreateAssetMenu(fileName = "New Counter", menuName = "Tools/Counter", order = 0)]
public class Counter : PersistentScriptableObject
{
    public int number = 0;
}
