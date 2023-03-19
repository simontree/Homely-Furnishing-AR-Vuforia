using System;
using UnityEngine;
using UnityEngine.UI;

public class MaterialDropdownHandler : MonoBehaviour
{
    private Dropdown _dropdown;
    public Material[] materials;
    
    public GameObject anchorPlacement;
    public SelectionDropdownHandler selectionDropdownHandler;
    public Dropdown dropdown;
    private void Awake()
    {
        _dropdown = GetComponent<Dropdown>();
    }
    private void Start()
    {
        InstantiateDropdownOptions();
        // _dropdown.onValueChanged.AddListener(delegate { GetSelectedObjectIndex(_dropdown); });
        // GetMaterial();
    }

    public void InstantiateDropdownOptions()
    {
        foreach (var material in materials)
        {
            var option = new Dropdown.OptionData(material.name);
            _dropdown.options.Add(option);
        }
        _dropdown.RefreshShownValue();
    }

    // public int GetSelectedObjectIndex(Dropdown dropdown)
    // {
    //     return dropdown.value;
    // }

    public void GetMaterial()
    {
        Debug.Log("renderer: " + anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(dropdown)).GetChild(0)
            .GetComponent<MeshRenderer>().material);
    }
}