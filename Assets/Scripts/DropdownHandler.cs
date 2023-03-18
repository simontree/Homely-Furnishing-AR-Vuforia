using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    public GameObject AnchorPlacement;

    List<string> selectableObjects = new List<string>();
    private Dropdown dropdown;

    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }
    // public void DropdownExample(int index)
    // {
    //     switch (index)
    //     {
    //         //numberText.text can be any object to be changed
    //         case 0: numberText.text = "0";
    //             break;
    //         case 1: numberText.text = "1";
    //             break;
    //         case 2: numberText.text = "2";
    //             break;
    //         case 3: numberText.text = "3";
    //             break;
    //     }
    // }

    void Update()
    {
        if (AnchorPlacement.transform.childCount > 0)
        {
            foreach (Transform child in AnchorPlacement.transform)
            {
                // dropdown.options.Add(new Dropdown.OptionData() {text = child.gameObject.name});
                Debug.Log("added item: "+child.gameObject.name);
                // Debug.Log("dropdow");
            }
        }
    }
}
