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
        Instantiate(FurnitureObj, PlaneFinder.transform.GetChild(0).localPosition, Quaternion.identity, AnchorPlacement.transform);
    }
}
