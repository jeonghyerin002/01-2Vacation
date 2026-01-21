using UnityEngine;

public class RTSUnitView : MonoBehaviour
{
    public int unitId;

    [Header("Debug (Inspector)")]
    [SerializeField] private string unitNameDebug;
    [SerializeField] private int hpDebug;
    [SerializeField] private int maxHpDebug;

    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? Vector3.one * 1.2f : Vector3.one;
    }

    // RTSGameManager가 매 프레임(또는 선택 시) 값을 넣어줄 거임
    public void SetDebug(string unitName, int hp, int maxHp)
    {
        unitNameDebug = unitName;
        hpDebug = hp;
        maxHpDebug = maxHp;
    }
}
