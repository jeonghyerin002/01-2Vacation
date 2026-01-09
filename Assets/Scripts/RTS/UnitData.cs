using UnityEngine;

[System.Serializable]
public class UnitData
{
    public string name;
    public float moveSpeed;

    public UnitData(string name, float speed)
    {
        this.name = name;
        moveSpeed = speed;
    }
}
