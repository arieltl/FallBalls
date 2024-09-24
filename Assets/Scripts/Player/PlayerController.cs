using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    private Rigidbody _rb;

    private int _count = 0;
    // public TextMeshProUGUI countText;
    // public GameObject winTextObject;


    public float speed = 3;
    public float jumpIntensity = 10;

    public float angle;

    bool _jump = false;
    bool _braking;
    Vector2 _movement;
    float _lastAngle;
    bool _moving;
    
    public List<AudioClip> respawnSounds;
    
    private AudioSource _audioSource;
    LevelManager _levelManager;

    private void Start() {
        // winTextObject.SetActive(false);
        
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _levelManager = FindObjectOfType<LevelManager>();
        // SetCountText();
    }

    private void OnMove(InputValue value) {
        var v = value.Get<Vector2>();
        _movement = v;
    }


    // void SetCountText() {
    //     countText.text = $"Count: {_count.ToString()}";
    // }

    private void OnJump() {
        _jump = true;
    }


    void FixedUpdate() {
        Vector3 velocity = _rb.velocity;
        if (_jump) {
            if (Physics.Raycast(transform.position, Vector3.down, 0.6f)) {
                Vector3 move = Vector3.up * jumpIntensity;
                _rb.AddForce(move, ForceMode.Impulse);
            }
            // Vector3 move = velocity.normalized * impulseSpeed;
      
            _jump = false;
        } else {
            _braking = _movement.y < 0;
            float yInput = _movement.y;
            float xInput = _movement.x;


            if (_moving) {
                angle = 90 - Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg;
            } else {
                xInput = 0;
                angle += _movement.x*1.3f;
            }

            if (_braking) {
                if (velocity.magnitude > 0.1) {
                    yInput = -0.5f;
                } else {
                    yInput = 0;
                }
            }


            var move = Quaternion.AngleAxis(angle, Vector3.up) * (new Vector3(xInput, 0, yInput) * speed);


            _rb.AddForce(move);

            // _rb.angularDrag = (velocity.magnitude > 0.15f && _moving) ? 0.05f : 15;
            _moving = (velocity.magnitude > 1.2);
            _lastAngle = angle;
        }


        if (transform.position.y < -10) {
            //get random sound
            _audioSource.clip = respawnSounds[Random.Range(0, respawnSounds.Count)];
            _audioSource.Play();
            _levelManager.Respawn();
            
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Pickup")) return;

        other.gameObject.SetActive(false);
        _count++;
        if (_count >= 9) {
            // winTextObject.SetActive(true);
        }

        // SetCountText();
    }
}