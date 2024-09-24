using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
    // Start is called before the first frame update
    [FormerlySerializedAs("initalCheckpoint")]
    public GameObject initialCheckpoint;
    public GameObject lastCheckpoint;

    GameObject _currentCheckpoint;
    CheckPointController _currentCheckpointController;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI checkpointText;
    
    
    GameManager _gameManager;
    GameObject _player;
    Rigidbody _playerRb;

    int _n_checkpoints;
    
    ArenaController _arenaController;
    float _time = 0f;

    int _lives = -1;
    
    
  
    void Start() {
        _currentCheckpoint = initialCheckpoint;
        _currentCheckpointController = _currentCheckpoint.GetComponent<CheckPointController>();
        _currentCheckpointController.SetActive();
        _arenaController = _currentCheckpointController.arenaController;
        _player = GameObject.FindWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
        _gameManager = FindAnyObjectByType<GameManager>();
        _n_checkpoints = lastCheckpoint.GetComponent<CheckPointController>().n;
        
        if (_gameManager == null) {
            Debug.LogError("GameManager not found");
            return;
        }

        _lives = _gameManager.Difficulty switch {
            1 => 10,
            2 => 3,
            3 => 1,
            _ => -1
        };
    }

    public void Respawn() {
        if (_lives > 0) {
            _lives--;
        }
        if (_lives == 0) {
            _gameManager.GameOver();
            return;
        }
        Debug.Log("Respawn");
        if (_arenaController != null) {
            _arenaController.Respawn();
        }
        //get checkpoint absolution position
        Vector3 position = _currentCheckpoint.transform.position;
        //Remove player velocity
        _playerRb.velocity = Vector3.zero;
        //Set player position to checkpoint position
        _player.transform.position = position + Vector3.up * 10;
        
       
    }

    void Update() {
        _time += Time.deltaTime;
        UpdateTime();
        UpdateLives();
        UpdateCheckpoint();
    }

    private void UpdateTime() {
        int minutes = (int)_time / 60;
        int seconds = (int)_time % 60;
        timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
    }
    
    private void UpdateCheckpoint() {
        checkpointText.text = $"Checkpoint: {_currentCheckpointController.n:D2}/{_n_checkpoints:D2}";
    }
    void UpdateLives() {
        if (_lives < 0) {
            livesText.text = "Lives: infinite";
            return;
        }
        livesText.text = $"Lives: {_lives:D2}";
    }
    public void SetCurrentCheckpoint(GameObject checkpoint) {
        //check if current is same as checkpoint
        if (_currentCheckpoint == checkpoint) return;
        

        _currentCheckpointController.Reset();
        _currentCheckpoint = checkpoint;
        _currentCheckpointController = _currentCheckpoint.GetComponent<CheckPointController>();
        _arenaController = _currentCheckpointController.arenaController;
        _currentCheckpointController.SetActive();
        
        if (_currentCheckpointController.n == _n_checkpoints) {
            _gameManager.Win(_time);
        }
    }
}