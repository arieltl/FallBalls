using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager _gameManager;
    void Start()
    {
        
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Restart() {
        _gameManager.LoadLevel("MainMenu");
    }
}
