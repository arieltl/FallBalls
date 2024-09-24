using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour {
    // Start is called before the first frame update
    private Rigidbody _rb;
    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnRespawn() {
      StartCoroutine(ResetRot());
    }
    
  
	
    IEnumerator ResetRot()
    {
        _rb.Sleep();
        yield return new WaitForFixedUpdate();
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();

        // Transport it, the fkin shit flips in mid ai
        transform.rotation = Quaternion.identity;
        yield return new WaitForFixedUpdate();

// Stop the car again, this results in only a very very slight inertia, not enough to flip it
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();

        _rb.WakeUp();
    }
    

}