using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownExample : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;

    public void DropdownExampleText(int index)
    {
        switch (index)
        {
            //numberText.text can be any object to be changed
            case 0: numberText.text = "0";
                break;
            case 1: numberText.text = "1";
                break;
            case 2: numberText.text = "2";
                break;
            case 3: numberText.text = "3";
                break;
        }
    }
}
