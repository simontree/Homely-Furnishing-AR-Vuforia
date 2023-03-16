using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

public class FurniturePlacingManager : MonoBehaviour
{
    public bool GroundPlaneHitReceived { get; private set; }
    
    Vector3 ObjectScale
    {
        get
        {
            var augmentationScale = VuforiaRuntimeUtilities.IsPlayMode() ? 0.1f : ObjectSize;
            return new Vector3(augmentationScale, augmentationScale, augmentationScale);
        }
    }
    
    [Header("Augmentation Object")]
    [SerializeField] GameObject FurnitureObj = null;
    
    [Header("Augmentation Size")]
    [Range(0.1f, 2.0f)]
    [SerializeField] float ObjectSize = 0.65f; // doesn't seem to have an effect when changing it - from core samples
    
    const string GROUND_PLANE_NAME = "Emulator Ground Plane";
    
    Camera mainCamera;
    string floorName;
    Vector3 originalFurnitureObjScale;
    bool isPlaced;
    int automaticHitTestFrameCount;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        originalFurnitureObjScale = FurnitureObj.transform.localScale;
        // SetupMaterials(); TODO
        floorName = GROUND_PLANE_NAME;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // EnablePreviewModeTransparency(!mIsPlaced); TODO
        if (!isPlaced)
            RotateTowardsCamera(FurnitureObj);
        if (isPlaced)
            SnapObjectToMousePosition();
    }

    public void Reset()
    {
        FurnitureObj.transform.localPosition = Vector3.zero;
        FurnitureObj.transform.localEulerAngles = Vector3.zero;
        FurnitureObj.transform.localScale = Vector3.Scale(originalFurnitureObjScale, ObjectScale);
        isPlaced = false;
    }

    private void LateUpdate()
    {
        // The AutomaticHitTestFrameCount is assigned the Time.frameCount in the
        // OnAutomaticHitTest() callback method. When the LateUpdate() method
        // is then called later in the same frame, it sets GroundPlaneHitReceived
        // to true if the frame number matches. For any code that needs to check
        // the current frame value of GroundPlaneHitReceived, it should do so
        // in a LateUpdate() method.
        GroundPlaneHitReceived = automaticHitTestFrameCount == Time.frameCount;
        
        if (!isPlaced)
        {
            // The Chair should only be visible if Ground Plane Hit was received for this frame
            var isVisible = GroundPlaneHitReceived;
            // mFurnitureObjRenderer.enabled = mFurnitureObjShadowRenderer.enabled = isVisible;
        }
    }

    void SnapObjectToMousePosition()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0))
        {
            if (!UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
            {
                var cameraToPlaneRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit) &&
                    cameraToPlaneHit.collider.gameObject.name == floorName)
                    FurnitureObj.transform.position = cameraToPlaneHit.point;
            }
                
        }
    }

    void RotateTowardsCamera(GameObject augmentation)
    {
        var lookAtPosition = mainCamera.transform.position - augmentation.transform.position;
        
        lookAtPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookAtPosition);
        Debug.Log("rotation: "+rotation);
        augmentation.transform.rotation = rotation;
    }

    /// <summary>
    /// Displays a preview of the furniture at the location pointed by the device.
    /// It is registered to PlaneFinderBehaviour.OnAutomaticHitTest.
    /// </summary>
    public void OnAutomaticHitTest(HitTestResult result)
    {
        automaticHitTestFrameCount = Time.frameCount;
    
        if (!isPlaced)
        {
            // Content is not placed yet. So we place the augmentation at HitTestResult
            // position to provide a visual feedback about where the augmentation will be placed.
            FurnitureObj.transform.position = result.Position;
        }
    }

    /// <summary>
    /// Adjusts augmentation in a desired way.
    /// Anchor is already placed by ContentPositioningBehaviour.
    /// So any augmentation on the anchor is also placed.
    /// </summary>
    public void OnContentPlaced()
    {
        Debug.Log("OnContentPlaced() called.");
        //Align content to anchor
        FurnitureObj.transform.localPosition = Vector3.zero;
        RotateTowardsCamera(FurnitureObj);
        isPlaced = true;
    }
    
}
