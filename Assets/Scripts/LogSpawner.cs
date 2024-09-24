using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class LogSpawner : MonoBehaviour
{
    private float _timer = 0;
    private float _spawnInterval = 2;

    bool _isSpawning = false;
    
    public GameObject logPrefab;
    

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawning) return;
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            //reandom new timer
            _spawnInterval = Random.Range(30, 220) / 100f;
            //raendom new x offset
            var xOffSet = Random.Range(-16, 15);
            _timer = 0;
            var log = LeanPool.Spawn(logPrefab, transform.position + new Vector3(xOffSet,20,-10), transform.rotation);
            // log.transform.SetParent(transform);
        }
        
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isSpawning = true;
        }
    }
    
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isSpawning = false;
        }
    }
    
}
