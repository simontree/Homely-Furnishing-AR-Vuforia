using UnityEngine;
using UnityEngine.UI;

public class MaterialDropdownHandler : MonoBehaviour
{
    private Dropdown _materialDropdown;
    public Dropdown selectionDropdown;
    public Material[] chairMaterials; 
    public Material[] tableMaterials;
    public Material[] sofaMaterials;
    public GameObject anchorPlacement;
    public SelectionDropdownHandler selectionDropdownHandler;
    private int _objectType;
    [SerializeField] private Text labelText;

    private void Awake()
    {
        _materialDropdown = GetComponent<Dropdown>();
    }

    private void Start()
    {
        _materialDropdown.onValueChanged.AddListener(delegate
        {
            switch (GetObjectType())
            {
                case 0:
                {
                    SetMaterial(tableMaterials[_materialDropdown.value]);
                    GetFurnitureObjectTransform().GetComponent<MaterialStore>().SetMaterialChanged(true);
                    break;
                }
                case 1:
                {
                    SetMaterial(sofaMaterials[_materialDropdown.value]);
                    GetFurnitureObjectTransform().GetComponent<MaterialStore>().SetMaterialChanged(true);
                    break;
                }
                case 2:
                {
                    SetMaterial(chairMaterials[_materialDropdown.value]);
                    GetFurnitureObjectTransform().GetComponent<MaterialStore>().SetMaterialChanged(true);
                    break;
                }
            }
        });
        selectionDropdown.onValueChanged.AddListener(delegate
        {
            if (anchorPlacement.transform.childCount > 0)
            {
                var selectedChildsMaterial = anchorPlacement.transform
                    .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown)).GetChild(0)
                    .GetComponent<MeshRenderer>().material;
                GetDropdownOptions(GetObjectType());
                
                if (GetFurnitureObjectTransform().GetComponent<MaterialStore>()
                    .GetHasMaterialChanged())
                {
                    _materialDropdown.value =
                        _materialDropdown.options.FindIndex(option => selectedChildsMaterial.name.Contains(option.text));
                }
            }

            if (anchorPlacement.transform.childCount == 0)
            {
                _materialDropdown.ClearOptions();
            }
        });
    }

    public void RefreshMaterialOptionsOnClick()
    {
        GetDropdownOptions(GetObjectType());
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
            if (objectName.Contains("Sofa"))
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

    private Transform GetFurnitureObjectTransform()
    {
        return anchorPlacement.transform
            .GetChild(
                selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown));
    }
    
    private void GetDropdownOptions(int _objectType)
    {
        _materialDropdown.ClearOptions();
        switch (_objectType)
        {
            case 0:
            {
                foreach (var material in tableMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    _materialDropdown.options.Add(option);
                }
                break;
            }
            case 1:
            {
                foreach (var material in sofaMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    _materialDropdown.options.Add(option);
                }
                break;
            }
            case 2:
            {
                foreach (var material in chairMaterials)
                {
                    var option = new Dropdown.OptionData(material.name);
                    _materialDropdown.options.Add(option);
                }
                break;
                }
            }
        _materialDropdown.RefreshShownValue();
    }
}