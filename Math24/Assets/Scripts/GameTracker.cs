using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTracker : MonoBehaviour {

    static int STATE_WAITING = 0;
    static int STATE_READY = 1;
    static int STATE_PLAYING = 2;

    private int currentState;
    private bool isSetup;
    private bool isPause;

    public Text timerIntText;
    public Text timerFloatText;
    public Number number;
    public Slider slider;


    public float readyTime = 5;
    public float playTime = 60;
    public float currentTime;
    // Use this for initialization
    void Start () {
        currentState = STATE_WAITING;
        currentTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState == STATE_WAITING) Waiting();
        else if (currentState == STATE_READY) Ready();
        else if (currentState == STATE_PLAYING) Playing();
    }

    private void Waiting()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentState = STATE_READY;
            isSetup = false;
        }
    }

    private void Ready()
    {
        if (isSetup)
        {
            UpdateTimer();
            number.NewRandom();
            if(currentTime <= 0)
            {
                currentState = STATE_PLAYING;
                isSetup = false;
            }
        }
        else
        {
            currentTime = readyTime;
            isSetup = true;
            isPause = false;
        }
    }

    private void Playing()
    {
        if (isSetup)
        {
            if (Input.GetKeyDown(KeyCode.Space)) isPause = !isPause;
            if(!isPause) UpdateTimer();
            if (currentTime <= 0)
            {
                currentState = STATE_WAITING;
                isSetup = false;
            }
        }
        else
        {
            number.NewRandom();
            currentTime = playTime;
            isSetup = true;
        }
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0f) currentTime = 0f;
        timerIntText.text = Mathf.Floor(currentTime).ToString("F0");
        timerFloatText.text = ((int)((currentTime % 1f) * 100)).ToString("00");
        if (currentState == STATE_READY) slider.value = currentTime / readyTime;
        else if (currentState == STATE_PLAYING) slider.value = currentTime / playTime;
    }
}
