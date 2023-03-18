using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject FurnitureObj;
    public GameObject PlaneFinder;
    public GameObject AnchorPlacement;
    
    private int _objectCount = 0;

    public void SpawnObject()
    {
        if (FurnitureObj != null)
        {
            GameObject furniture = Instantiate(FurnitureObj, PlaneFinder.transform.GetChild(0).localPosition, Quaternion.identity,
                AnchorPlacement.transform);
            _objectCount++;
            furniture.name = FurnitureObj.name + _objectCount;
        }
    }
    
    public void DeleteAll()
    {
        foreach (Transform child in AnchorPlacement.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
