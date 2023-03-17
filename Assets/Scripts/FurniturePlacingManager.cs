using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

public class FurniturePlacingManager : MonoBehaviour
{
    public bool GroundPlaneHitReceived { get; private set; }

    [Header("Augmentation Object")]
    public GameObject FurnitureObj = null;

    Camera mainCamera;
    Vector3 originalFurnitureObjScale;
    bool isPlaced;
    int automaticHitTestFrameCount;

    public GameObject AnchorPlacement;
    // Vector3 originalPrefabObjScale;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        originalFurnitureObjScale = FurnitureObj.transform.localScale;
        // originalPrefabObjScale = AnchorPlacement.transform.GetChild(0).localScale;
        // SetupMaterials(); TODO
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
        // FurnitureObj.transform.localPosition = Vector3.zero;
        // FurnitureObj.transform.localEulerAngles = Vector3.zero;
        // FurnitureObj.transform.localScale = Vector3.Scale(originalFurnitureObjScale, new Vector3(0.1f, 0.1f, 0.1f));
        
        Debug.Log("AnchorPlacement.transform.GetChild(0) in Reset(): "+AnchorPlacement.transform.GetChild(0));
        // AnchorPlacement.transform.GetChild(0).localPosition = Vector3.zero;
        // AnchorPlacement.transform.GetChild(0).localEulerAngles = Vector3.zero;
        // AnchorPlacement.transform.GetChild(0).localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
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
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit) 
                    )
                    FurnitureObj.transform.position = cameraToPlaneHit.point;
                
                    // rework this as now only 1 child can get dragged
                    AnchorPlacement.transform.GetChild(0).position = cameraToPlaneHit.point;
            }
                
        }
    }
    void RotateTowardsCamera(GameObject augmentation)
    {
        var lookAtPosition = mainCamera.transform.position - augmentation.transform.position;
        
        lookAtPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookAtPosition);
        augmentation.transform.rotation = rotation;
    }

    /// <summary>
    /// Displays a preview of the furniture at the location pointed by the device.
    /// It is registered to PlaneFinderBehaviour.OnAutomaticHitTest.
    /// </summary>
    public void OnAutomaticHitTest(HitTestResult result)
    {
        automaticHitTestFrameCount = Time.frameCount;
        
        // Debug.Log("automaticHitTestFrameCount: "+automaticHitTestFrameCount);
    
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
        // FurnitureObj.transform.localPosition = Vector3.zero;
        // RotateTowardsCamera(FurnitureObj);
        
        //test seems to work
        // AnchorPlacement.transform.GetChild(0).transform.localPosition = Vector3.zero;
        // RotateTowardsCamera(AnchorPlacement.transform.GetChild(0).GameObject());
        
        isPlaced = true;
    }
    
}
