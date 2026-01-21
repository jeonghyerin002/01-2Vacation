using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    SkillBase skill = new FireBallSkill();

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            skill.Use();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            skill = new HealSkill();
        }
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            skill = new FireBallSkill();
        }
    }
}
