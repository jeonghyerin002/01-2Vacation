using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class LoopBootcamp : MonoBehaviour
{
    [Header("테스트 데이터")]
    public int[] numbers = { 3, 1, 4, 1, 5 };
    public List<string> names = new() { "Alma", "Bora", "Ciel" };

    private List<int> card = new List<int>();
    private int[] deckCard = new int[5];

    void Start()
    {

        
        for (int i = 0; i < 5; i++)
        {
            deckCard[i] = Random.Range(0, 13);
        }
        for (int i = 0; i < 12; i++)
        {
            card.Add(Random.Range(0, 13));
        }

        deckCard[0] = Random.Range(0, 13);
        deckCard[1] = Random.Range(0, 13);
        deckCard[2] = Random.Range(0, 13);
        deckCard[3] = Random.Range(0, 13);
        deckCard[4] = Random.Range(0, 13);

        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));
        card.Add(Random.Range(0, 13));


        for (int i = 0; i < card.Count; i++)
        {
            Debug.Log(card[i]);
        }


        Debug.Log(card[0]);
        Debug.Log(card[1]);
        Debug.Log(card[2]);
        Debug.Log(card[3]);
        Debug.Log(card[4]);


        foreach (int n in card)
        {
            Debug.Log(n);
        }

        Debug.Log(numbers.Length); //길이
        Debug.Log(names.Count);    //갯수

        for(int i = 0; i < numbers.Length; i++)
        {
            numbers[i] += 10;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            Debug.Log(numbers[i]);
        }
        foreach (int x in numbers)
        {
            Debug.Log(x);
        }

        for (int i = 0; i < names.Count; i++)
        {
            names[i] += "t";
        }
        foreach (string x in names)
        {
            Debug.Log(x);
        }

    }


    void Update()
    {
        
    }
}
