using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public Text pointCounter;
    public BacteriMotor motor;
    public Camera cam;
    void Update()
    {
        pointCounter.text = "Point : " + motor.xpPoint;

        if (Input.GetMouseButton(0))
        {
            Ray _ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(_ray, out _hit))
            {
                Vector3 _mousse = _hit.point;
                motor.GoDir(_mousse);
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            motor.UsePower(Power.Melee);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            motor.UsePower(Power.Dash);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            motor.UsePower(Power.Ranged);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            motor.UsePower(Power.SlowDown);
        }

    }

    
}
