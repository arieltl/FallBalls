using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Respawn() {
        Debug.Log("ArenaOnRespawn");
        BroadcastMessage("OnRespawn");
    }
}
