using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class DespawnHeight : MonoBehaviour
{
    // Start is called before the first frame update
   public float height = -5;

   private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        
        if (transform.position.y < height)
        {
            _rb.velocity  = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            LeanPool.Despawn(gameObject);
        }
    }
}
