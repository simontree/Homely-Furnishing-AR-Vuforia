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

    public SelectionDropdownHandler selectionDropdownHandler;
    public Dropdown dropdown;

    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
    }
    
    void Update()
    {
        // TODO: add alert that object has to be selected to be dragged
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
                    // Debug.Log("snap function called.");
                    
                    anchorPlacement.transform.GetChild(selectionDropdownHandler.GetSelectedObjectIndex(dropdown)).position = cameraToPlaneHit.point; 
                    
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
        Debug.Log("Content placed");
            RotateTowardsCamera(anchorPlacement.transform.GetChild(selectionDropdownHandler.GetSelectedObjectIndex(dropdown)).GameObject());
        _isPlaced = true;
    }
    
}
