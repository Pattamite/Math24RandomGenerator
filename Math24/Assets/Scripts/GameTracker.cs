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
    private Image sliderColor;

    public Text timerIntText;
    public Text timerFloatText;
    public Text timerDotText;
    public Number number;
    public Slider slider;
    public Text header;
    public Color highColor;
    public Color midColor;
    public Color lowColor;

    public float readyTime = 5;
    public float playTime = 60;
    public float currentTime;
    // Use this for initialization
    void Start () {
        currentState = STATE_WAITING;
        currentTime = 0;
        sliderColor = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
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
            sliderColor.color = lowColor;
            header.text = "Ready";
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
            header.text = "Make 24";
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

        if (slider.value >= 0.5)
        {
            timerIntText.color = highColor;
            timerFloatText.color = highColor;
            timerDotText.color = highColor;
            sliderColor.color = highColor;
        }
        else if (slider.value >= 0.25)
        {
            timerIntText.color = midColor;
            timerFloatText.color = midColor;
            timerDotText.color = midColor;
            sliderColor.color = midColor;
        }
        else
        {
            timerIntText.color = lowColor;
            timerFloatText.color = lowColor;
            timerDotText.color = lowColor;
            sliderColor.color = lowColor;
        }
    }
}
