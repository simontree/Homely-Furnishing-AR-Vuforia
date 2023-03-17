using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;
using Vector3 = GLTF.Math.Vector3;

public class FurniturePlacingManager : MonoBehaviour
{
    public GameObject anchorPlacement;
    
    private Camera _mainCamera;
    private bool _isPlaced;
    
    private int _objectCount;
    
    public LayerMask selectableMask;
    private GameObject _selectedObject;
    private GameObject _furnitureObject;

    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
    }
    
    void Update()
    {
        if (_isPlaced)
        {
            SnapObjectToMousePosition();
            _objectCount = anchorPlacement.transform.childCount;
        }
    }

    void SnapObjectToMousePosition()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0))
        {
            if (!UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
            {
                var cameraToPlaneRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit)
                    && _objectCount > 1)
                {
                    anchorPlacement.transform.GetChild(_objectCount-1).position = cameraToPlaneHit.point;
                }
                else
                {
                    anchorPlacement.transform.GetChild(0).position = cameraToPlaneHit.point;
                }
            }
        }

        // from https://yewtu.be/watch?v=uNCCS6DjebA - Unity Drag and Drop Script from AIA
        // if (_selectedObject != null)
        // {
        //     Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,_mainCamera.WorldToScreenPoint(_selectedObject.transform.position).z);
        //     Vector3 position2 = new Vector3(Input.mousePosition.x, Input.mousePosition.y,Input.mousePosition.z);
        //     // GLTF
        //     Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(position2);
        // }
        
    }

    // from https://yewtu.be/watch?v=uNCCS6DjebA - Unity Drag and Drop Script from AIA
    // private RaycastHit CastRay()
    // {
    //     Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.farClipPlane);
    //     Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane);
    //     Vector3 worldMousePosFar = _mainCamera.ScreenToWorldPoint(screenMousePosFar);
    //     Vector3 worldMousePosNear = _mainCamera.ScreenToWorldPoint(screenMousePosFar);
    //     RaycastHit hit;
    //     Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
    //     return hit;
    // }
    

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
        Debug.Log("OnContentPlaced() called.");
        if(_objectCount == 1)
        {
            RotateTowardsCamera(anchorPlacement.transform.GetChild(0).GameObject());
        }
        if (_objectCount > 1)
        {
            RotateTowardsCamera(anchorPlacement.transform.GetChild(_objectCount-1).GameObject());
        }
        _isPlaced = true;
    }
    
}
