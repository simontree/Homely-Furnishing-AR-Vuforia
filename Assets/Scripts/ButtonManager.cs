using UnityEngine;
using UnityEngine.Serialization;

public class ButtonManager : MonoBehaviour
{
    public GameObject furnitureObj;
    public GameObject planeFinder;
    public GameObject anchorPlacement;
    
    private int _objectCount = 0;

    public void SpawnObject()
    {
        if (furnitureObj != null)
        {
            GameObject furniture = Instantiate(furnitureObj, planeFinder.transform.GetChild(0).localPosition, Quaternion.identity,
                anchorPlacement.transform);
            _objectCount++;
            furniture.name = furnitureObj.name + _objectCount;
        }
    }
    
    public void DeleteAll()
    {
        foreach (Transform child in anchorPlacement.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
