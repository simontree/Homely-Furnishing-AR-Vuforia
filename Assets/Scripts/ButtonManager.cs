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
    public SelectionDropdownHandler selectionDropdownHandler;

    private int _objectCount = 0;
    
    private Vector3 _rotation;
    [SerializeField] private float speed;

    private bool _rotated;

    public bool rotateRightButtonPressed = false;
    public bool rotateLeftButtonPressed = false;
    
    public void SpawnObject()
    {
        if (furnitureObj != null)
        {
            GameObject furniture = Instantiate(furnitureObj, planeFinder.transform.GetChild(0).localPosition, Quaternion.identity,
                anchorPlacement.transform);
            _objectCount++;
            furniture.name = furnitureObj.name + _objectCount;
            _rotated = false;
        }
    }

    private void Update()
    {
        while (rotateLeftButtonPressed || rotateRightButtonPressed)
        {
            RotateObject();
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
        _rotated = false;
    }

    public bool WasRotated()
    {
        return _rotated;
    }

    private void RotateObject()
    {
        if (anchorPlacement.transform.childCount > 0)
        {
            if (rotateLeftButtonPressed)
            {
                _rotation = Vector3.down * 10;
                rotateLeftButtonPressed = false;
            }

            if (rotateRightButtonPressed)
            {
                _rotation = Vector3.up * 10;
                rotateRightButtonPressed = false;
            }
            
            GetFurnitureObjectTransform().Rotate(speed * Time.deltaTime * _rotation);
            _rotated = true;
        }
    }
    public void RotateObjectLeft()
    {
        rotateLeftButtonPressed = true;
    }
    public void RotateObjectRight()
    {
        rotateRightButtonPressed = true;
    }

    private Transform GetFurnitureObjectTransform()
    {
        return anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown));
    }
}
