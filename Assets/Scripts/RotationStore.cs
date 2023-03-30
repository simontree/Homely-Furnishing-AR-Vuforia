using UnityEngine;

public class RotationStore : MonoBehaviour
{
    private bool _wasRotated;
    public void SetWasRotated(bool rotationChanged)
    {
        _wasRotated = rotationChanged;
    }

    public bool GetWasRotated()
    {
        return _wasRotated;
    }
}
