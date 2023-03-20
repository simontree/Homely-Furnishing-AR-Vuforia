using System;
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
    
    private Vector3 rotation;
    [SerializeField] private float speed;

    private bool _rotated;

    public bool rotateRightButttonPressed = false;
    public bool rotateLeftButtonPressed = false;
    
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

    private void Update()
    {
        while (rotateLeftButtonPressed || rotateRightButttonPressed)
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
    }

    public bool WasRotated()
    {
        return _rotated;
    }

    void RotateObject()
    {
        if (anchorPlacement.transform.childCount > 0)
        {
            if (rotateLeftButtonPressed)
            {
                rotation = Vector3.down * 10;
                rotateLeftButtonPressed = false;
            }

            if (rotateRightButttonPressed)
            {
                rotation = Vector3.up * 10;
                rotateRightButttonPressed = false;
            }
            
            GetFurnitureObjectTransform().Rotate(speed * Time.deltaTime * rotation);
            _rotated = true;
        }
    }
    public void RotateObjectLeft()
    {
        rotateLeftButtonPressed = true;
    }
    public void RotateObjectRight()
    {
        rotateRightButttonPressed = true;
    }

    private Transform GetFurnitureObjectTransform()
    {
        return anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown));
    }
}
