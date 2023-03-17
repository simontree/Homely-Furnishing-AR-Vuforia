using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ButtonManager : MonoBehaviour
{
    public GameObject FurnitureObj;
    public GameObject PlaneFinder;
    public GameObject AnchorPlacement;

    public void SpawnObject()
    {
        if (FurnitureObj != null)
        {
            Instantiate(FurnitureObj, PlaneFinder.transform.GetChild(0).localPosition, Quaternion.identity,
                AnchorPlacement.transform);
        }
    }
    
    public void DeleteAll()
    {
        while (AnchorPlacement.transform.childCount > 0)
        {
            DestroyImmediate(AnchorPlacement.transform.GetChild(0));
        }
    }
}
