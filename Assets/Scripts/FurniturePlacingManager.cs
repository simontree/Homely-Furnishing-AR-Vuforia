using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

public class FurniturePlacingManager : MonoBehaviour
{
    public GameObject anchorPlacement;
    
    private Camera _mainCamera;
    private bool _isPlaced;

    public SelectionDropdownHandler selectionDropdownHandler;
    public Dropdown dropdown;

    private Vector3 rotation;
    [SerializeField] private float speed;

    private bool _rotated;

    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        _rotated = false;
    }

    private Transform GetFurnitureObjectTransform()
    {
        return anchorPlacement.transform
            .GetChild(selectionDropdownHandler.GetSelectedObjectIndex(dropdown));
    }
    
    void Update()
    {
        if (_isPlaced)
        {
            SnapObjectToMousePosition();
        }

        if (anchorPlacement.transform.childCount > 0)
        {
            RotateObject();
        }
    }
    
    void SnapObjectToMousePosition()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0) && !UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
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
        if (_rotated == false)
        {
            RotateTowardsCamera(GetFurnitureObjectTransform().GameObject());
        }
        _isPlaced = true;
    }

    private void RotateObject()
    {
        if(Input.GetKey(KeyCode.A)) rotation = Vector3.up;
        else if (Input.GetKey(KeyCode.D)) rotation = Vector3.down;
        else rotation = Vector3.zero;
        GetFurnitureObjectTransform().Rotate(speed * Time.deltaTime * rotation);
        _rotated = true;
    }
}
