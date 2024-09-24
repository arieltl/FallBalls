using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    public GameObject end;
    public Vector3 endOffset;
    public float duration = 5f;
    
    Vector3 _begin;
    Vector3 _end;
    
    // Start is called before the first frame update
    void Start()
    {
        _begin = transform.position;
        _end = end.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(Time.time / duration * Mathf.PI * 2) * 0.5f + 0.5f;
        transform.position = Vector3.Slerp(_begin, _end+endOffset, t);
    }
}
