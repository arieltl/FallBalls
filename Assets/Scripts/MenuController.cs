using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager _gameManager;
    void OnEnable() {
        _gameManager = FindObjectOfType<GameManager>();
        
    }
    
    void StartGame(int difficulty) {
        _gameManager.StartGame(difficulty);
    }
    
    public void StartEasyGame() {
        StartGame(0);
    }
    
    public void StartMediumGame() {
        StartGame(1);
    }
    
    public void StartHardGame() {
        StartGame(2);
    }
    
    public void StartImpossibleGame() {
        StartGame(3);
    }
    
    
}
