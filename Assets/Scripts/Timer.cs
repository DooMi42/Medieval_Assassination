using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.IO;

public class Timer : MonoBehaviour
{
    float timer;

    int timerSeconds = 0;
    int timerMinutes = 0;
    int loadedSeconds = -1;

    string jsonTimer;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI PBtimerText;


    // Start is called before the first frame update
    void Start()
    {
        LoadFromJSON();
        PBtimerText.text = jsonTimer;
    }

    // Update is called once per frame
    void Update()
    {
        TimerLogic();
        UpdateUI();
    }

    public void LoadFromJSON()
    {
        if (!File.Exists($"{Application.dataPath}/Savedata/data.json")) return;
        jsonTimer = File.ReadAllText($"{Application.dataPath}/Savedata/data.json");
        if (int.TryParse(jsonTimer, out int seconds))
        {
            loadedSeconds = seconds;
            //int seconds = int.Parse(jsonTimer);
            int minutes = (int)(seconds/ 60f);

            seconds -= minutes * 60;
            jsonTimer = $"Personal Best: {minutes}:{seconds}";
        }

    }

    public void UpdateUI()
    {
        timerText.text = $"Current Run: {timerMinutes}:{timerSeconds}";
    }

    public void TimerLogic()
    {
        timer += Time.deltaTime;
        if (timerSeconds >= 60)
        {
            timerMinutes++;
            timerSeconds = 0;
        }
        if (timer > 1)
        {
            timerSeconds++;
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //you win
            if (!Directory.Exists($"{Application.dataPath}/Savedata")) Directory.CreateDirectory($"{Application.dataPath}/Savedata");
            if (loadedSeconds == -1 || loadedSeconds > timerSeconds + timerMinutes * 60)
            {
                File.WriteAllText($"{Application.dataPath}/Savedata/data.json", (timerSeconds + timerMinutes * 60).ToString());
            }
            
            
        }
    }
}
