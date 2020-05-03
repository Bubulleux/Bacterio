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
            if (Input.GetKey(KeyCode.LeftControl))
            {
                motor.Upgrade(Power.Melee);
            }
            else
            {
                motor.UsePower(Power.Melee);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                motor.Upgrade(Power.Dash);
            }
            else
            {
                motor.UsePower(Power.Dash);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                motor.Upgrade(Power.Ranged);
            }
            else
            {
                motor.UsePower(Power.Ranged);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                motor.Upgrade(Power.SlowDown);
            }
            else
            {
                motor.UsePower(Power.SlowDown);
            }
        }



        if (Input.GetKey(KeyCode.F1))
        {
            motor.xpPoint++;
            motor.lvl++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            motor.AddStat(Stats.Speed);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            motor.AddStat(Stats.Attack);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            motor.AddStat(Stats.BonnusXP);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            motor.AddStat(Stats.Regene);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            motor.AddStat(Stats.Defence);
        }

    }


}
