using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayerSin : MonoBehaviour {

    public GameObject obstacle;
    private Transform _playerTransform;

    float _baseX;
    float _basey;
    bool _isFollowing = false;

    public float xSpread = 10; 
    public float xSpeed = 1;
    public float zSpread = 2;
    public float zSpeed = 1;
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _baseX = obstacle.transform.position.x;
        _basey = obstacle.transform.position.y;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!_isFollowing) return;
        float xOffset = Mathf.Sin(Time.time * xSpeed) * xSpread;
        float zOffset = Mathf.Sin(Time.time * zSpeed) * zSpread;
        
        
        Vector3 targetPosition = new Vector3(_baseX + xOffset, _basey , _playerTransform.position.z + zOffset); 
        obstacle.transform.position = Vector3.Slerp(obstacle.transform.position, targetPosition, Time.deltaTime*10);
                
                
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isFollowing = true;
        }
    }
    
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            _isFollowing = false;
        }
    }
}
