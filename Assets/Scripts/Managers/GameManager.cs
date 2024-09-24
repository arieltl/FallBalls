using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Start is called before the first frame update
    int _difficulty = 0;
    public int Difficulty => _difficulty;
    
    public float winTime = 0;

    // dont destroy on load
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void Win(float time) {
        winTime = time;
        LoadLevel("Victory");
    }
    
    
    //coroutine to load level
    public void LoadLevel(string levelName) {
        StartCoroutine(LoadLevelAsync(levelName));
    }
    
    IEnumerator LoadLevelAsync(string levelName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        if (asyncLoad == null) {
            Debug.LogError("Failed to load level " + levelName);
            yield return null;
        }
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
    
    public void GameOver() {
        Debug.Log("GameOver");
        LoadLevel("GameOver");
    }
    
    public void StartGame(int difficulty) {
        Debug.Log("StartGame");
        _difficulty = difficulty;
        LoadLevel("Level1");
    }
}