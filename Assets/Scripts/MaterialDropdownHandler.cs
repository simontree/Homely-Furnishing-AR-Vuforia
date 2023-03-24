using UnityEngine;
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

    private int _objectType;

    private void Start()
    {
        materialDropdown.onValueChanged.AddListener(delegate
        {
            switch (GetObjectType())
            {
                case 0:
                {
                    SetMaterial(tableMaterials[materialDropdown.value]);
                    break;
                }
                case 1:
                {
                    SetMaterial(sofaMaterials[materialDropdown.value]);
                    break;
                }
                case 2:
                {
                    SetMaterial(chairMaterials[materialDropdown.value]);
                    break;
                }
            }
        });
    }

    private void Update()
    {
        if (anchorPlacement.transform.childCount > 0)
        {
            GetDropdownOptions(GetObjectType());
        }

        if (anchorPlacement.transform.childCount == 0)
        {
            materialDropdown.ClearOptions();
        }
    }

    private void SetMaterial(Material materialToSet)
    {
        anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)).GetChild(0)
            .GetComponent<MeshRenderer>().material = materialToSet;
    }

    public int GetObjectType()
    {
        if (anchorPlacement.transform.childCount > 0)
        {
            var objectName = anchorPlacement.transform
                .GetChild(
                    selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)
                    ).name;
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
                    materialDropdown.RefreshShownValue();
                }
                break;
            }
            case 1:
            {
                foreach (var material in sofaMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    materialDropdown.options.Add(option);
                    materialDropdown.RefreshShownValue();
                }
                break;
            }
            case 2:
            {
                foreach (var material in chairMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    materialDropdown.options.Add(option);
                    materialDropdown.RefreshShownValue();
                }
                break;
                }
            }
    }
}