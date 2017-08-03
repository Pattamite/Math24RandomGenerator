using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public int totalNumber = 4;

    private int[] value;
    public Text[] numberText;
    

	// Use this for initialization
	void Start ()
    {
        value = new int[totalNumber];
        for (int i = 0; i < totalNumber; i++)
            value[i] = 9;
    }

    public void NewRandom()
    {
        RandomValue();
        SetText();
    }

    private void RandomValue()
    {
        for (int i = 0; i < totalNumber; i++)
            value[i] = Random.Range(1, 10);
    }

    private void SetText()
    {
        for (int i = 0; i < totalNumber; i++)
            numberText[i].text = value[i].ToString();
    }
}
