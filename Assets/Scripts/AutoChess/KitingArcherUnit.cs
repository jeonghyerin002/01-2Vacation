using UnityEngine;

public class KitingArcherUnit : UnitBase
{
    [SerializeField] int damage = 10;
    [SerializeField] int criticalDamage = 2;   //배수로 계산
    [SerializeField] float criticalPercent = 0.2f;

    protected override void OnAttack(IDamageable t)
    {
        var tr = ((MonoBehaviour)t).transform;
        if (Random.value < criticalPercent)
        {
            damage *= criticalDamage;
            Debug.DrawLine(transform.position + Vector3.up, tr.position + Vector3.up, Color.red, 0.15f);
            Debug.Log("크리티컬!");
        }
        else
        {
            Debug.DrawLine(transform.position + Vector3.up, tr.position + Vector3.up, Color.white, 0.15f);
            Debug.Log("공격!");
        }
        t.TakeDamage(damage);
    }
}

