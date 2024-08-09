using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "ScriptableObject/ClientSO")]
public class ClientSO : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public String order;
    public String explanation;
    //public smth expectedOrder;

}
