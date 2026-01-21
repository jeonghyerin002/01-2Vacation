using UnityEngine;

public class Numbers : MonoBehaviour
{

    int NumberA = 0;
    int NumberB = 0;
    int NumberC = 0;
    void Start()
    {
        for (int i = 0; i < 4; i+=2)
        {
            NumberA += 2;                    
        }

        Debug.Log(NumberA);    
    }

    void Update()
    {
        
    }
}
