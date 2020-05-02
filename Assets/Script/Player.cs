using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public BacteriMotor motor;
    public Camera cam;
    public void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().player = gameObject;
    }

    void Update()
    {
        

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



        if (Input.GetKey(KeyCode.F1))
        {
            motor.xpPoint++;
            motor.lvl++;
        }
        
        

    }


}
