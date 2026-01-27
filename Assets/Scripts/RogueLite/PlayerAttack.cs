using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int baseDamage = 10;
    [SerializeField] float baseCooldown = 1.0f;

    float timer;
    int dummyHp = 200;

    void Update()
    {
        timer += Time.deltaTime;

        float cd = baseCooldown * RunRules.CooldownMult;

        if (Input.GetKey(KeyCode.Space) && timer >= cd)
        {
            timer = 0f;

            int dmg = Mathf.RoundToInt(baseDamage * RunRules.DamageMult);
            dummyHp -= dmg;

            Debug.Log($"Hit! dmg={dmg}, dummyHP={dummyHp}");

            if (dummyHp <= 0)
            {
                dummyHp = 200; // 죽으면 다시 생성된 것처럼 리셋(짧고 굵게)
                Debug.Log("Dummy respawn!");

            }
        }
    }
}
