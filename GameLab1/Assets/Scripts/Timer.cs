using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public Player player;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    [Header("Time Up UI")]
    public GameObject TimeUpScreen;
    public Button RetryButton;
    public Button QuitButton;

    [Header("Time Up Sound")]
    public AudioSource AudioSource;
    public AudioClip TimeUpclip;

    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.Tenths, "0.0");
        timeFormats.Add(TimerFormats.Hundredths, "0.00");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit))){
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
            AudioSource.clip = TimeUpclip;
            AudioSource.Play();
            TimeUpScreen.SetActive(true);
            RetryButton.onClick.AddListener(Retry);
            QuitButton.onClick.AddListener(ExitGame);
            player.SetFrozen();
        }
        SetTimerText();
    }

    private void SetTimerText(){
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    public enum TimerFormats{
        Whole,
        Tenths,
        Hundredths
    }

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame(){
        Application.Quit();
        Debug.Log("Quit!");
    }

}
