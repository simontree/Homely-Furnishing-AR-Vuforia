using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStoreManager : MonoBehaviour
{
    private bool _hasMaterialChanged;
    public void SetMaterialChanged(bool materialChanged)
    {
        _hasMaterialChanged = materialChanged;
    }

    public bool GetHasMaterialChanged()
    {
        return _hasMaterialChanged;
    }
    
}
