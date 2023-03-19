using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MaterialDropdownHandler : MonoBehaviour
{
    public Dropdown materialDropdown;
    public Dropdown selectionDropdown;
    public Material[] chairMaterials; 
    public Material[] tableMaterials;
    public Material[] sofaMaterials;
    
    public GameObject anchorPlacement;
    public SelectionDropdownHandler selectionDropdownHandler;

    private Dictionary<int, Material> _savedMaterials;
    private int _objectType;

    private void Start()
    {
        Debug.Log("materialdropdownhandler started.");

        //First selected object --> retrieve materials to it
        // var firstSelectedObject = selectionDropdown.value;
        
        
        
        // GetDropdownOptions(GetObjectType());
        
        // Debug.Log("selectionDropdown.value: "+selectionDropdown.value);
        
        
        selectionDropdown.onValueChanged.AddListener(delegate
        {
            GetDropdownOptions(GetObjectType());
        });
        materialDropdown.onValueChanged.AddListener(delegate
        {
            var material = anchorPlacement.transform
                .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)).GetChild(0)
                .GetComponent<MeshRenderer>().material;
            // SaveMaterial(selectionDropdown.value, material);
            // Debug.Log("saved material with index: "+selectionDropdown.value+" and value: "+material);
        });
    }

    // private void Update()
    // {
    //     Debug.Log("objectType: " + getObjectType());
    // }

    private int GetObjectType()
    {
        if (anchorPlacement.transform.childCount > 0)
        {
            var objectName = anchorPlacement.transform
                .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)).name;
            if (objectName.Contains("Table"))
            {
                return 0;
            }
            if (objectName.Contains("sofa"))
            {
                return 1;
            }
            if (objectName.Contains("Chair"))
            {
                return 2;
            }
        }
        return -1; // no object type found
    }

    private void SaveMaterial(int index, Material material)
    {
        _savedMaterials[index] = material;
        Debug.Log("savedMaterials: "+_savedMaterials.Count);
    }
    public void GetSavedMaterial()
    {
        // Debug.Log("renderer: " + anchorPlacement.transform
        // .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)).GetChild(0)
        // .GetComponent<MeshRenderer>().material);
    }
    public void GetDropdownOptions(int _objectType)
    {
        materialDropdown.ClearOptions();
        switch (_objectType)
        {
            case 0:
            {
                foreach (var material in tableMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    materialDropdown.options.Add(option);
                }
                break;
            }
            case 1:
            {
                foreach (var material in sofaMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    materialDropdown.options.Add(option);
                }
                break;
            }
            case 2:
            {
                foreach (var material in chairMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    materialDropdown.options.Add(option);
                }
                break;
                }
            }
        materialDropdown.RefreshShownValue();
    }
}