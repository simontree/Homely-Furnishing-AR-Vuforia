using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

public class FurniturePlacingManager : MonoBehaviour
{
    public GameObject anchorPlacement;
    
    private Camera _mainCamera;
    private bool _isPlaced;
    
    private GameObject _furnitureObject;

    public DropdownHandler dropdownHandler;
    public Dropdown dropdown;

    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
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
        if (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0) && !UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
        {
                var cameraToPlaneRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit))
                {
                    anchorPlacement.transform.GetChild(dropdownHandler.GetSelectedObjectIndex(dropdown)).position = cameraToPlaneHit.point;
                }
        }
    }
    
    void RotateTowardsCamera(GameObject augmentation)
    {
        var lookAtPosition = _mainCamera.transform.position - augmentation.transform.position;
        lookAtPosition.y = 0;
        augmentation.transform.rotation = Quaternion.LookRotation(lookAtPosition);
    }

    /// <summary>
    /// Adjusts augmentation in a desired way.
    /// Anchor is already placed by ContentPositioningBehaviour.
    /// So any augmentation on the anchor is also placed.
    /// </summary>
    public void OnContentPlaced()
    {
        RotateTowardsCamera(anchorPlacement.transform.GetChild(dropdownHandler.GetSelectedObjectIndex(dropdown)).GameObject());
        _isPlaced = true;
    }
    
}
