using UnityEngine;
using System.Collections.Generic;

public class RTSGameManager : MonoBehaviour
{
    [Header("유닛")]
    public RTSUnitView[] unitViews;

    [Header("레이어")]
    public LayerMask unitMask;
    public LayerMask groundLayer;

    [Header("레이캐스트")]
    public float maxDistance = 100f;

    [Header("라인")]
    public LineRenderer line;
    public float lineHeight = 0.1f;

    private List<UnitData> units = new List<UnitData>();

    private int selectedId = -1;
    private bool hasTarget;
    private Vector3 targetPos;

    public int SelectedId => selectedId;
    void Start()
    {
        //유닛 데이터 생성
        units.Add(new UnitData("Alpha", 4f)); //(name, speed)
        units.Add(new UnitData("Beta", 5f));
        units.Add(new UnitData("Gamma", 3.5f));

        //뷰에 ID 부여
        for (int i = 0; i < unitViews.Length; i++)

            unitViews[i].unitId = i;
        if (line != null)
        {
            line.positionCount = 2;
            line.enabled = false;
        }


    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleUnitSelection();

        if (Input.GetMouseButtonDown(1))
            HandleMoveCommand();

        UpdateMovement();
        UpdateLine();
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
    // 2) 이동 명령 (우클릭)
    // =========================
    void HandleMoveCommand()
    {
        if (selectedId == -1) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundLayer))
        {
            hasTarget = true;
            targetPos = hit.point;

            units[selectedId].targetPos = hit.point;   //선택된 유닛의 위치를 각각 할당한다.
            units[selectedId].isMoving = true;
        }
    }

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
    // 선택 처리
    // =========================
    void Select(int id)
    {
        selectedId = id;
        hasTarget = false;

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
