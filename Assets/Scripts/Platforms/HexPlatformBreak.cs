using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPlatformBreak : MonoBehaviour {
    MeshCollider _meshCollider;
    Material _material;


    int _physicalCondition = 100;


    private void Start() {
        _meshCollider = GetComponent<MeshCollider>();
        _material = GetComponent<MeshRenderer>().material;
    }


    public void ApplyDamage() {
        _physicalCondition -= 5;
        _physicalCondition = _physicalCondition < 0 ? 0 : _physicalCondition;

        var color = _material.color;
        color.a = _physicalCondition / 100f;
        _material.color = color;
        _meshCollider.enabled = _physicalCondition > 0;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            InvokeRepeating(nameof(ApplyDamage), 0, 0.1f);
        }
    }
    
    void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            CancelInvoke(nameof(ApplyDamage));
        }
    }
    
    
    public void OnRespawn() {
        CancelInvoke(nameof(ApplyDamage));
        Debug.Log("Reseting platform");
        _physicalCondition = 100;
        var color = _material.color;
        color.a = 1;
        _material.color = color;
        _meshCollider.enabled = true;
    }
}