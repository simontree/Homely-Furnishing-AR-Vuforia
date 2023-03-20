using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject furnitureObj;
    public GameObject planeFinder;
    public GameObject anchorPlacement;
    
    public Dropdown materialDropdown;
    public Dropdown selectionDropdown;
    
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
        _objectCount = 0;
        selectionDropdown.ClearOptions();
        materialDropdown.ClearOptions();
    }
}
