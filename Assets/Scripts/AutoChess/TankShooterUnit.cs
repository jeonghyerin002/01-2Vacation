using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TankShooterUnit : UnitBase
{
    [SerializeField] int damage = 15;
    [SerializeField] int criticalDamage = 2;   //배수로 계산
    [SerializeField] float criticalPercent = 0.2f;

    [SerializeField] int currentShield = 0;
    [SerializeField] int shield = 30;     // current hp + shield
    [SerializeField] float shieldPercent = 0.02f;
    [SerializeField] float shieldCooldown = 5f;
    float shieldTimer;

    [SerializeField] int maxHp = 50;

    Renderer myRenderer;
    Color originalColor;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();             //원래 색으로 되돌리기
        originalColor = myRenderer.material.color;
    }
    protected override void Update()
    {
        base.Update();
        shieldTimer += Time.deltaTime;          //쉴드 사용 후 얼마나 시간이 지났는지 확인
    }
    protected override void OnAttack(IDamageable t)
    {
        var tr = ((MonoBehaviour)t).transform;

        int currentDamage = damage;
        bool isCritical = Random.value < criticalPercent;

        if (isCritical)
        {
            currentDamage *= criticalDamage;
            Debug.DrawLine(transform.position + Vector3.up, tr.position + Vector3.up, Color.red, 0.15f);
            Debug.Log("크리티컬!");
        }
        else
        {
            Debug.DrawLine(transform.position + Vector3.up, tr.position + Vector3.up, Color.white, 0.15f);
            Debug.Log("공격!");
        }
        t.TakeDamage(currentDamage);
    }
    public override void TakeDamage(int amount)
    {
        if(IsDead) return;

        int remainingDamage = amount;            //TakeDamage(int amount)

        if (!IsDead && currentShield <= 0 && shieldTimer >= shieldCooldown)
        {
            if (Random.value < shieldPercent)
            {
                shieldTimer = 0f;
                currentShield = shield;
                UpdateSheildColor();
                Debug.Log("쉴드 생성 성공!");
            }
            else
            {
                Debug.Log("쉴드 생성 실패!");
            }
        }
        if (currentShield > 0)
        {
            int absorbed = Mathf.Min(currentShield, remainingDamage);    //쉴드가 대신 맞아준 데미지 양 = (현재 쉴드 ex.30 , 남아있는 데미지 ex.40) = 30이 남음
            currentShield -= absorbed;                                   //쉴드가 30 막음
            remainingDamage -= absorbed;

            UpdateSheildColor();//hp에서 10 감소

            if (remainingDamage > 0)
            {
                base.TakeDamage(remainingDamage);
            }
        }


    }
  
    void UpdateSheildColor()
    {
        if(currentShield <= 0)
        {
            SetColor(originalColor);
            return;
        }
        float ratio = (float)currentShield / shield;       //currentShield를 float로 바꾼 다음 shield로 나눈다.

        Color shieldColor = Color.Lerp(originalColor, Color.cyan, ratio);

        SetColor(shieldColor);
    }
    void SetColor(Color c)
    {
            myRenderer.material.color = c;
    }
}

