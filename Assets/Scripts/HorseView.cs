using System.Transactions;
using UnityEngine;

public class HorseView : MonoBehaviour
{
    public int horseId;

    public void UpdatePosition(float x) //()= 인수 혹은 파라미터
    {
        Vector3 pos = transform.position; //지금 스크립트 오브젝트의 현재 위치
        pos.x = x;
        transform.position = pos; //현재 위치를 pos로 갱신      
    }
}
