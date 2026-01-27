using UnityEngine;

public static class RunRules
{
    public static float DamageMult = 1f;
    public static float CooldownMult = 1f;

    public static void Reset()
    {
        DamageMult = 1f;
        CooldownMult = 1f;
    }
}
