using UnityEngine;

public class EnumStateDemo : MonoBehaviour
{
    enum UnitState
    {
        Idle,
        Moving,
        Attacking
    }
    [SerializeField] UnitState state = UnitState.Idle;

    public Vector3 moveTarget;
    public Transform enemyTarget;
    public float moveSpeed = 4f;
    public float attackRange = 2f;   //공격 범위



    void Update()
    {
        // 1) 상태 전환 (입력/명령)
        if (Input.GetKeyDown(KeyCode.M))
        {
            state = UnitState.Moving;
            Debug.Log("상태 : Moving");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            state = UnitState.Attacking;
            Debug.Log("상태 : Attacking");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            state = UnitState.Idle;
            Debug.Log("상태 : Idle");
        }

        switch (state)
        {
            case UnitState.Idle:
                //아무것도 안함
                break;
            case UnitState.Moving:
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, moveTarget) < 0.05f) // moveTarget의 거리와 내 거리가 0.05f 이하일 때는 idle 상태로 바꿔준다.
                    state = UnitState.Idle;
                break;
            case UnitState.Attacking:
                if(enemyTarget == null)
                {
                    state = UnitState.Idle;
                    break;
                }
                float dist = Vector3.Distance(transform.position, enemyTarget.position);

                if(dist > attackRange)
                {
                    //이동해서 공격(접근)
                    transform.position = Vector3.MoveTowards(transform.position, enemyTarget.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    //공격(여기서는 로그만)
                    Debug.Log("공격!");
                    state = UnitState.Idle;
                }
                break;
        }      


    }
}
