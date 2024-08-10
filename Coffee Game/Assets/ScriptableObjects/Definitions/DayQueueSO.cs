using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayQueue", menuName = "ScriptableObject/DayQueueSO")]
public class DayQueueSO : ScriptableObject
{
    public List<ClientSO> queue;
}