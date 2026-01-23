using UnityEngine;

public class ArcherUnit : UnitBase
{
    [SerializeField] int damage = 12;

    protected override void OnAttack(IDamageable t)
    {
        var tr = ((MonoBehaviour)t).transform;
        Debug.DrawLine(transform.position + Vector3.up, tr.position + Vector3.up, Color.white, 0.15f);

        t.TakeDamage(damage);
    }
}
