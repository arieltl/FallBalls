using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _audioSource.Play();
        }
    }

 
}
