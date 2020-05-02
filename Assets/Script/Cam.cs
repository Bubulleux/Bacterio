using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = new Vector3(player.position.x, 15f, player.position.z);
    }
}
