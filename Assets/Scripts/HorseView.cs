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
    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? Vector3.one * 1.2f : Vector3.one;  //one = 1, 1, 1  / = selected ?(if) ture : false 풀어쓰면 밑에 if문

        //if(selected == true)
        //{
        //    transform.localScale = Vector3.one * 1.2f;
        //}
        //if (selected == false)
        //{
        //    transform.localScale = Vector3.one;
        //}
    }
}
