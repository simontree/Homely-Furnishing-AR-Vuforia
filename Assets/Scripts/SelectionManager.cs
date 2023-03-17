using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// selection via raycast not working yet - try selection via dropdown?? 
public class SelectionManager : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    public LayerMask draggableMask;
    private GameObject selectedObject;
    void Start()
    {
        _mainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
    }
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
        //     
        //     if (hit.collider != null)
        //     {
        //         selectedObject = hit.collider.gameObject;
        //     }
        // }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out var hit))
        //     {
        //         // var selection = hit.collider;
        //         // if (selection.CompareTag((selectableTag)))
        //         // {
        //         //     
        //         // }
        //     }
        // }
        
    }
}
