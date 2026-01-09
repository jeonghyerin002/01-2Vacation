using Unity.VisualScripting;
using UnityEngine;

public class RTSUnitView : MonoBehaviour
{
    public int unitId;

    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? Vector3.one * 1.2f : Vector3.one;
    }
}
