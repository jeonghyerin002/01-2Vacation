using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class RTSGameManager : MonoBehaviour
{
    [Header("유닛")]
    public RTSUnitView[] unitViews;

    [Header("레이어")]
    public LayerMask unitMask;
    public LayerMask groundLayer;
    public LayerMask enemyLayer; // 추가


    [Header("레이캐스트")]
    public float maxDistance = 100f;

    [Header("라인")]
    public LineRenderer line;
    public float lineHeight = 0.1f;

    [Header("적(씬에 배치된 EnemyView들)")]
    public EnemyView[] enemies; // 추가

    private List<UnitData> units = new List<UnitData>();

    private int selectedId = -1;
    private Vector3 targetPos;

    // 전투 타겟(추가)
    private EnemyView currentEnemyTarget;

    public int SelectedId => selectedId;
    void Start()
    {
        //유닛 데이터 생성
        units.Add(new UnitData("Alpha", 4f)); //(name, speed)
        units.Add(new UnitData("Beta", 5f));
        units.Add(new UnitData("Gamma", 3.5f));

        for (int i = 0; i < units.Count; i++)
        {
            units[i].hp = units[i].maxHp;
            units[i].attackTimer = 0f;
        }

        //뷰에 ID 부여
        for (int i = 0; i < unitViews.Length; i++)
            unitViews[i].unitId = i;

        if (line != null)
        {            line.positionCount = 2;
            line.enabled = false;
        }


    }


    void Update()
    {
        HandleUnitSelection();        // 이전 과제(완성된 상태)
        HandleRightClickCommand();    // 이번 과제 TODO
        UpdateMovement();             // 이전 과제(완성된 상태)
        UpdateCombat();               // 이번 과제 TODO
        UpdateLine();                 // 이전 과제(완성된 상태, 필요하면 라인 확장)
        UpdateInspectorDebug();       // 이번 과제 TODO
    }

    // =========================
    // 1) 유닛 선택 (좌클릭)
    // =========================
    void HandleUnitSelection()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, unitMask))
        {
            RTSUnitView view = hit.collider.GetComponent<RTSUnitView>();
            if (view != null)
            {
                Select(view.unitId);
                return;
            }
        }
        Select(-1);

    }
    // =========================
    // 2) 우클릭 명령 확장 (이번 과제)
    // - 적을 우클릭하면: 타겟 지정
    // - 바닥을 우클릭하면: 이동 명령
    // =========================
    void HandleRightClickCommand()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (selectedId == -1) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hitEnemy, maxDistance, enemyLayer))
        {
            EnemyView enemies = hitEnemy.collider.GetComponent<EnemyView>();
            if(enemies != null)
            {
                currentEnemyTarget = enemies;
            }
            return;
        }
        if (Physics.Raycast(ray, out RaycastHit hitGround, maxDistance, groundLayer))
        {
            currentEnemyTarget = null;
            targetPos = hitGround.point;
        }
    

        // TODO:
        // 1) 우클릭이 아니면 return
        // 2) 선택된 유닛이 없으면 return
        // 3) Raycast를 쏴서 "enemyLayer"를 먼저 검사
        //    - 맞으면 currentEnemyTarget 지정(EnemyView)
        //    - 이동 목표(hasTarget)는 끄거나 유지 여부는 선택(권장: 끄기)
        // 4) enemy가 안 맞으면 "groundLayer" 검사
        //    - 맞으면 이동 목표 설정(hasTarget/targetPos)
        //    - currentEnemyTarget은 해제(권장)
    }
    
    //void HandleMoveCommand()
    //{
    //    if (selectedId == -1) return;

    //    Camera cam = Camera.main;
    //    if (cam == null) return;

    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan, 1f);

    //    if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundLayer))
    //    {
    //        hasTarget = true;
    //        targetPos = hit.point;

    //        units[selectedId].targetPos = hit.point;   //선택된 유닛의 위치를 각각 할당한다.
    //        units[selectedId].isMoving = true;
    //    }
    //}

    // =========================
    // 3) 이동 처리
    // =========================
    void UpdateMovement()
    {
        for(int i = 0; i < unitViews.Length; i++)
        {
            if (units[i].isMoving == true)
            {
                unitViews[i].transform.position = Vector3.MoveTowards(GetUnitTransform(i).position, units[i].targetPos, units[i].moveSpeed * Time.deltaTime);
                
                if(Vector3.Distance(GetUnitTransform(i).position, units[i].targetPos) < 0.1f)
                {
                    units[i].isMoving = false;
                }
            }
        }
    }
    // =========================
    // 4) 전투 처리 (이번 과제)
    // 조건:
    // - 유닛 선택됨
    // - currentEnemyTarget != null
    // - 타겟이 살아있음(활성 or hp>0)
    // - 거리 <= attackRange
    // - attackTimer 누적 후 cooldown 도달 시 공격
    // =========================
    void UpdateCombat()
    {
        if (selectedId == -1) return;
        if (currentEnemyTarget == null) return;

        if (currentEnemyTarget.IsDead())
        {
            currentEnemyTarget = null;
            return;
        }

        if(Vector3.Distance(currentEnemyTarget.transform.position, GetUnitTransform(selectedId).position) <= units[selectedId].attackRange)
        {
            units[selectedId].attackTimer += Time.deltaTime;

            if(units[selectedId].attackTimer >= units[selectedId].attackCooldown)
            {
                currentEnemyTarget.TakeDamage(units[selectedId].attackDamage);
                units[selectedId].attackTimer = 0;
            }
        }


        // TODO:
        // 1) 선택된 유닛 없으면 return
        // 2) currentEnemyTarget이 없으면 return
        // 3) 타겟이 죽었거나 비활성이면 타겟 해제 후 return
        // 4) 거리 체크( Vector3.Distance )
        // 5) 쿨타임 타이머 누적( Time.deltaTime )
        // 6) 쿨타임 되면: <- 
        //    - currentEnemyTarget.TakeDamage(attackDamage)
        //    - timer = 0
        //    - 타겟이 죽었으면 타겟 해제
    }

    // =========================
    // 4) 라인 업데이트
    // =========================
    void UpdateLine()
    {
        if(selectedId == -1) return;

        Transform unitTf = GetUnitTransform(selectedId);

        Vector3 movedTf = targetPos;
        if(unitTf == null)
        {
            line.enabled = false;
            return;
        }
        line.enabled = true;

        line.SetPosition(0, unitTf.position);
        line.SetPosition(1, movedTf);
        

    }
    // =========================
    // 6) 인스펙터 디버그 업데이트 (이번 과제)
    // - 선택된 유닛의 hp/maxHp/name을 RTSUnitView에 전달
    // =========================
    void UpdateInspectorDebug()
    {
        for(int i = 0; i < unitViews.Length; i++)
        {
            Debug.Log($"{units[i].name}, {units[i].hp}, {units[i].maxHp}");
        }
        // TODO:
        // for문으로 모든 unitViews를 돌면서
        // units[i]의 name, hp, maxHp를 view.SetDebug(...)로 넣기
        // ????????????????????????????? 음 흠 이게 뭘까나~
    }

    // =========================
    // 선택 처리
    // =========================
    void Select(int id)
    {
        selectedId = id;
        currentEnemyTarget = null;

        for (int i = 0; i < unitViews.Length; i++)
            unitViews[i].SetSelected(i == selectedId);

        Debug.Log(selectedId == -1 ? "선택 해제" : $"선택: {units[selectedId].name}");
    }
    public Transform GetUnitTransform(int id)
    {
        if (unitViews == null) return null;
        if (id < 0 || id >= unitViews.Length) return null;
        return unitViews[id].transform;
    }

}
