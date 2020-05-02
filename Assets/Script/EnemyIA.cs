using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public BacteriMotor motor;
    public GameObject Player;
    void Start()
    {
        
    }


    void Update()
    {
        float _dist = Vector3.Distance(transform.position, Player.transform.position);
        if (_dist < 10f)
        {
            motor.GoDir(Player.transform.position);
        }
    }
}
