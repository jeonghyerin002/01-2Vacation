using UnityEngine;

[System.Serializable]
public class UnitData
{
    public string name;
    public float moveSpeed;
    public Vector3 targetPos;
    public bool isMoving;

    public UnitData(string name, float speed)
    {
        this.name = name;
        moveSpeed = speed;
        isMoving = false;
    }
}
