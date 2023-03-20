using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionDropdownHandler : MonoBehaviour
{
    private Dropdown _dropdown;
    public int _objCount = 0;

    private void Awake()
    {
        _dropdown = GetComponent<Dropdown>();
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
        if (_dropdown.options.Count == 0)
        {
            _objCount = 0;
        }
        _objCount++;
        var option = new Dropdown.OptionData("Object "+_objCount+": "+furnitureObj.name);
        _dropdown.options.Add(option);
        _dropdown.value = _objCount-1;
        _dropdown.RefreshShownValue();
    }

    public int GetSelectedObjectIndex(Dropdown dropdown)
    {
        return dropdown.value;
    }
}