using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HorseRaceManager : MonoBehaviour
{
    [Header("결승선 위치")]
    public float finishLineX = 10f;

    [Header("말 오브젝트들")]
    public HorseView[] horseViews;

    public List<Horse> horses = new List<Horse>();
    private bool raceFinished = false;

    void Start()
    {
        //말 데이터 생성
        horses.Add(new Horse("Red", Random.Range(1.5f, 3.0f) , 1));  //new쓰고 생성자 쓴게 생성자임>>(string name, float speed)<<
        horses.Add(new Horse("Blue", Random.Range(1.5f, 3.0f), 2));
        horses.Add(new Horse("Green", Random.Range(1.5f, 3.0f),3));

        for(int i = 0; i < horseViews.Length; i++)
        {
            horseViews[i].horseId = i;
            horseViews[i].transform.position = new Vector3(0, 0, i * 1.5f);
        }
        Debug.Log("경마 시작");

    }


    void Update()
    {
        if (raceFinished) return;  //경기가 종료되면 아래로직(이동)이 막히게 함.

        for (int i = 0; i <  horses.Count; i++)
        {
            Horse h = horses[i];     //클래스 홀스를 h라고 선언한다. 그리고 horses[i]을 넣어준다.

            h.Move(Time.deltaTime);

            //화면 위치 갱신
            horseViews[i].UpdatePosition(h.positionX); //(클래스 홀스에 있는 포지션 x)

            //결승선 체크
            if(!h.finished && h.positionX >= finishLineX)
            {
                h.finished = true;
                raceFinished = true;
                Debug.Log($"우승 : {h.name}");
            }
        }

        //void ShowHorseData()            //말의 현재 스피드, 위치를 알려준다. 
        //{
        //    foreach(Horse horse in horses)
        //    {
        //        Debug.Log(horse.name, horse.speed, horse.positionX);
        //    }
        //}


    }
}
