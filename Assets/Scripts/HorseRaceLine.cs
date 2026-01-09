using UnityEngine;

public class HorseRaceLine : MonoBehaviour
{
    [Header("참조")]
    public HorseRaceManager race;
    public LineRenderer line;

    private void Awake()
    {
        if (line != null) line.positionCount = 2;     //꼭지점을 두 개 만들어 줌.
    }


    void Update()
    {
        if(race == null || line == null) return;

        int id = race.SelectedId;
        if(id < 0)
        {
            line.enabled = false;       //enable 할 수 있게 하다. disable 무능하게 하다(비활성화)
            return;
        }
        Transform horseTf = race.GetHorseTrasform(id);
        if(horseTf == null)
        {
            line.enabled = false;
            return;
        }

        line.enabled = true;

        Vector3 start = horseTf.position;
        Vector3 end = new Vector3 (race.finishLineX, start.y, start.z);

        line.SetPosition(0, start);
        line.SetPosition(1, end);        //아까  Awake함수에 선언한 꼭지점 두 개의 위치값을 선언한다.
    }
}
