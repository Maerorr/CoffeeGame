using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{

    private const string uiTag = "UI";
    public static bool IsAnyUIElementWithTagActive()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            var obj = EventSystem.current.currentSelectedGameObject;
            if (obj is not null)
            {
                return true;
            }
        }

        return false;
    }

    public static void SelectFistUIObject()
    {
        GameObject[] uiBlockers = GameObject.FindGameObjectsWithTag(uiTag);
        EventSystem.current.SetSelectedGameObject(uiBlockers[0]);
    }
}