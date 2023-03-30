using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class FurniturePlacingManager : MonoBehaviour
{
    public GameObject anchorPlacement;
    
    private Camera _mainCamera;
    private bool _isPlaced;

    public SelectionDropdownHandler selectionDropdownHandler;
    public Dropdown selectionDropdown;

    [SerializeField] private ButtonManager buttonManager;
    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
    }

    private Transform GetFurnitureObjectTransform()
    {
        return anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(selectionDropdown));
    }
    
    void Update()
    {
        if (_isPlaced)
        {
            SnapObjectToMousePosition();
        }
    }
    
    void SnapObjectToMousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            var cameraToPlaneRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit))
                {
                    GetFurnitureObjectTransform().position = cameraToPlaneHit.point;
                }
        }
    }
    
    void RotateTowardsCamera(GameObject augmentation)
    {
        if (buttonManager.WasRotated() == false)
        {
            var lookAtPosition = _mainCamera.transform.position - augmentation.transform.position;
            lookAtPosition.y = 0;
            augmentation.transform.rotation = Quaternion.LookRotation(lookAtPosition);
        }
    }

    /// <summary>
    /// Adjusts augmentation in a desired way.
    /// Anchor is already placed by ContentPositioningBehaviour.
    /// So any augmentation on the anchor is also placed.
    /// </summary>
    public void OnContentPlaced()
    {
        if (buttonManager.WasRotated() == false)
        {
            RotateTowardsCamera(GetFurnitureObjectTransform().GameObject());
        }
        _isPlaced = true;
    }
}
