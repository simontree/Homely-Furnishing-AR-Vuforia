using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionDropdownHandler : MonoBehaviour
{
    private Dropdown _dropdown;
    private int _objCount = 0;

    private void Awake()
    {
        _dropdown = GetComponent<Dropdown>();
        _dropdown.options.Add(new Dropdown.OptionData( "No Objects yet."));
    }
    private void Start()
    {   
        
        _dropdown.onValueChanged.AddListener(delegate
        {
            GetSelectedObjectIndex(_dropdown);
        });
    }

    public void AddObject(GameObject furnitureObj)
    {
        _objCount++;
        var option = new Dropdown.OptionData("Object "+_objCount+": "+furnitureObj.name);
        _dropdown.options.Add(option);
        _dropdown.RefreshShownValue();
    }

    public int GetSelectedObjectIndex(Dropdown dropdown)
    {
        return dropdown.value-1;
    }
}