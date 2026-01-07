using UnityEngine;

[System.Serializable]
public class Horse
{
    public string name;
    public float speed;
    public float positionX;
    public bool finished;
    public int bustCount;

    public Horse(string name, float speed , int bust) //생성자 : new로 생성했을 때 생성자를 통해서 데이터를 넣는다.
    {
        this.name = name;     //인수로 받아오는 이름을 내 클래스 이름에 넣는다.
        this.speed = speed;
        this.bustCount = bust;
        positionX = 0;
        finished = false;
    }
    public void Move(float  deltaTime)
    {
        if (finished) return;

        positionX += speed * deltaTime;  //시간이 지나는 것에 따라서 스피드 만큼 이동
    }
}
