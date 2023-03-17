using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

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
        if (Input.GetMouseButtonDown(0))
        {
            CheckHitObject();
        }

        if (_selectedObject != null)
        {
            if (Input.GetMouseButton(0))
            {
                DragObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }
        }
        
    }

    void CheckHitObject()
    {
        if (_selectedObject == null)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("hit: "+hit.collider.gameObject);
                _selectedObject = hit.collider.gameObject;
            }
        }
    }

    void DragObject()
    {
        Debug.Log("_selectedObject.transform.position: "+_selectedObject.transform.position);
        Debug.Log("_mainCamera.nearClipPlane + 1.0f: "+_mainCamera.nearClipPlane + 1.0f);
        _selectedObject.transform.position = _mainCamera.ScreenToWorldPoint(new UnityEngine.Vector3(Input.mousePosition.x,
            Input.mousePosition.y, _mainCamera.nearClipPlane + 1.0f));
    }

    void DropObject()
    {
        _selectedObject = null;
    }

    void SnapObjectToMousePosition()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0))
        {
            if (!UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
            {
                var cameraToPlaneRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit))
                {
                    _selectedObject = cameraToPlaneHit.transform.gameObject;
                }    
                
                
                // && _objectCount > 1)
                // {
                //     anchorPlacement.transform.GetChild(_objectCount-1).position = cameraToPlaneHit.point;
                // }
                // else
                // {
                //     anchorPlacement.transform.GetChild(0).position = cameraToPlaneHit.point;
                // }
            }
        }
        
        // from https://yewtu.be/watch?v=uNCCS6DjebA - Unity Drag and Drop Script from AIA
        // if (_selectedObject != null)
        // {
        //     Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,_mainCamera.WorldToScreenPoint(_selectedObject.transform.position).z);
        //     Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(position);
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
