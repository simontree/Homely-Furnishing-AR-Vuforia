using UnityEngine;
using UnityEngine.UI;

public class SelectionDropdownHandler : MonoBehaviour
{
    private Dropdown _selectionDropdown;
    public int objCount = 0;

    private void Awake()
    {
        _selectionDropdown = GetComponent<Dropdown>();
    }
    public void Start()
    {
        _selectionDropdown.onValueChanged.AddListener(delegate
        {
            GetSelectedObjectIndex(_selectionDropdown);
        });
    }

    public void AddObject(GameObject furnitureObj)
    {
        if (_selectionDropdown.options.Count == 0)
        {
            objCount = 0;
        }
        objCount++;
        var option = new Dropdown.OptionData("Object "+objCount+": "+furnitureObj.name);
        _selectionDropdown.options.Add(option);
        _selectionDropdown.RefreshShownValue();
        _selectionDropdown.value = objCount-1;
    }

    public int GetSelectedObjectIndex(Dropdown dropdown)
    {
        return dropdown.value;
    }
}