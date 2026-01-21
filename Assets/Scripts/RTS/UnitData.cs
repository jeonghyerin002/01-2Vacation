using UnityEngine;

[System.Serializable]
public class UnitData
{
    public string name;
    public float moveSpeed;

    [Header("Combat Stats")]
    public int maxHp = 100;
    public int hp = 100;
    public int attackDamage = 10;
    public float attackRange = 2.5f;
    public float attackCooldown = 1.0f;

    public Vector3 unitTf;
    public Vector3 targetPos;
    public bool isMoving;

    // 내부 타이머(쿨타임)
    [HideInInspector] public float attackTimer = 0f;

    public UnitData(string name, float speed)
    {
        this.name = name;
        moveSpeed = speed;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    public void ClampHp()
    {
        if (hp < 0) hp = 0;
        if (hp > maxHp) hp = maxHp;
    }
}