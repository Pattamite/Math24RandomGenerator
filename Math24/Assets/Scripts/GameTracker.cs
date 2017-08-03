using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTracker : MonoBehaviour {

    public Text timerIntText;
    public Text timerFloatText;
    public Number number;
    public float readyTime = 5;
    public float gameTime = 60;
    public float currentTime;
    // Use this for initialization
    void Start () {
        currentTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;;
        timerIntText.text = currentTime.ToString("F0");
        timerFloatText.text = ((int)((currentTime % 1f) * 100)).ToString("00");
    }
}
