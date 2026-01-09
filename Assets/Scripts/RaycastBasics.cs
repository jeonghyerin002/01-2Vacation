using UnityEngine;

public class RaycastBasics : MonoBehaviour
{
    [Header("최대 거리")]
    public float maxDistance = 100f;

    [Header("말만 맞추고 싶으면 레이어 지정")]
    public LayerMask targetLayers = ~0; //2진법 0,1 중에 0만 씀 (00000000) (모든 레이어 / All)


    void Update()
    {
        if(!Input.GetMouseButton(0)) return;

        Camera cam = Camera.main;     //메인 카메라라는 태그가 붙은 카메라를 자동으로 가져옴
        if(cam == null ) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);     //연두색은 클래스, 초록색 클래스

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.yellow, 1f) ;   //ray.origin은 ray의 시작지점, ray.Direction은 ray의 방향

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayers))    //out이 붙어있으면 함수에 있는 결과값을 out 뒷쪽 변수나 클래스에 데이터를 넣어준다.  Physics는 물리, 레이케스트가 부딪힌 곳을 히트라고 선언한다.
        {
            Debug.Log($"맞춤! name={hit.collider.name}, point={hit.point}, dist={hit.distance}");
        }
        else
        {
            Debug.Log("아무것도 못 맞춤");
        }
    }
}
