using UnityEngine;

public class HealSkill : SkillBase
{
    public override void Use()     //override = 재정의 한다 라는 키워드
    {
        Debug.Log("힐한다.");
    }
}
