using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinMenuController : MonoBehaviour
{
    // Start is called before the first frame update

   public TextMeshProUGUI timeText;
   private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        float time = 90f;
        if (_gameManager != null) {
            time = _gameManager.winTime;
        }
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        
        timeText.text = $"Time: {minutes:D2}:{seconds:D2}";


    }
    
    
    public void Restart() {
        _gameManager.LoadLevel("MainMenu");
    }

}
