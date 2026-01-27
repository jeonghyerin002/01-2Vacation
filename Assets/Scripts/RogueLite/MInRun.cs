using UnityEngine;

public class MInRun : MonoBehaviour
{
    int floor = 1;

    public PlayerAttack playerAttack;

    void Start()
    {
        RunRules.Reset();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Bless();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Curse();
    }
    void Bless()
    {
        RunRules.CooldownMult *= 0.75f;
        floor++;
        Debug.Log($"[Bless] floor={floor}  cooldown x{RunRules.CooldownMult:0.00}");
    }
    void Curse()
    {
        RunRules.DamageMult *= 1.5f;
        floor++;
        Debug.Log($"[Curse] floor={floor}  damage x{RunRules.DamageMult:0.00}");
    }
    void OnGUI()
    {
        GUILayout.Label($"Floor: {floor}");
        GUILayout.Label($"Damage x{RunRules.DamageMult:0.00}   Cooldown x{RunRules.CooldownMult:0.00}");
        GUILayout.Label("Press [1] Bless (faster)  /  [2] Curse (stronger)");
        GUILayout.Label("Hold [Space] to attack dummy");
    }
}
