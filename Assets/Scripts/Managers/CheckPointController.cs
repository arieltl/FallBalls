using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {
    static readonly int MainColor = Shader.PropertyToID("_MainColor");

    [ColorUsage(true, true)]
    public Color activeColor;
    
    LevelManager _levelManager;

    Color _originalColor;
    Material _material;
    
    ArenaController _arenaController;
    public ArenaController arenaController => _arenaController;
    
    public int n = 0;
    
    
    


    void OnEnable() {
        _material = GetComponent<MeshRenderer>().material;
        _levelManager = FindObjectOfType<LevelManager>();
        _arenaController = GetComponentInParent<ArenaController>();
    }

    public void Reset() {
        _material.SetColor(MainColor, _originalColor);
    }
    
    

    
    public void SetActive() {
        
        _originalColor = _material.GetColor(MainColor);
        _material.SetColor(MainColor, activeColor);
        Debug.Log("Activated Checkpoint " + transform.position);
    }
    void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        //create HDR color
        //set color Intensity
        _levelManager.SetCurrentCheckpoint(gameObject);

    }
}