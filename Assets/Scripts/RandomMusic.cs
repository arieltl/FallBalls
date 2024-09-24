using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour {
    public AudioClip[] music;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = music[Random.Range(0, music.Length)];
        _audioSource.Play();
        _audioSource.loop = true;
    }

    // Update is called once per frame
}
