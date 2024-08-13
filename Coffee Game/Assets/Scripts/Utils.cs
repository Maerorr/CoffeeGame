using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{

    private const string uiTag = "UI";
    public static bool IsAnyUIElementWithTagActive()
    {
        GameObject[] uiBlockers = GameObject.FindGameObjectsWithTag(uiTag);
        foreach (GameObject uiBlocker in uiBlockers)
        {
            if (uiBlocker.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}