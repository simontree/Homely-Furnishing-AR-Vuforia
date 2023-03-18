using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class hitalgo : MonoBehaviour
{
    private GameObject _pickedObject = null;
    // private Camera mainCamera;
    void Start()
    {
        // mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _pickedObject == null)
        {
            Ray cameraToPlaneRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("cameraToPlaneRay: "+ cameraToPlaneRay);
            if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit, Mathf.Infinity) 
                // && cameraToPlaneHit.collider.CompareTag("Selectable")
                )
            {
                Debug.Log("hit.gameObject: " + cameraToPlaneHit.collider.gameObject);
                _pickedObject = cameraToPlaneHit.collider.gameObject;
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _pickedObject = null;
        }
        
    }
}
