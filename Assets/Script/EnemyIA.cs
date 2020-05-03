using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIA : MonoBehaviour
{
    public BacteriMotor motor;
    public GameObject Player;
    public bool debug;

    public GameObject target;
    public Actions action;
    public Vector3 randomPoint;
    public TypeIA IAType;

    public Dictionary<TypeIA, Dictionary<object, float>> ratioBuy = new Dictionary<TypeIA, Dictionary<object, float>>()
    {
        {TypeIA.ninja, new Dictionary<object, float>()
            {
                {Stats.Speed, 4f },
                {Stats.Attack, 2f },
                {Stats.BonnusXP, 2f },
                {Stats.Regene, 2f },
                {Stats.Defence, 1f },
                {Power.Melee, 4f },
                {Power.Dash, 4f },
                {Power.Ranged, 1f },
                {Power.SlowDown, 2f }
            }
        },
        {TypeIA.tank, new Dictionary<object, float>()
            {
                {Stats.Speed, 2f },
                {Stats.Attack, 4f },
                {Stats.BonnusXP, 2f },
                {Stats.Regene, 2f },
                {Stats.Defence, 2f },
                {Power.Melee, 1f },
                {Power.Dash, 1f },
                {Power.Ranged, 4f },
                {Power.SlowDown, 2f }
            }
        },
        {TypeIA.normal, new Dictionary<object, float>()
            {
                {Stats.Speed, 1f },
                {Stats.Attack, 1f },
                {Stats.BonnusXP, 1f },
                {Stats.Regene, 1f },
                {Stats.Defence, 1f },
                {Power.Melee, 1f },
                {Power.Dash, 1f },
                {Power.Ranged, 1f },
                {Power.SlowDown, 1f }
            }
        }
    };

    private object buyObjectif;



    //HUD
    public Text lvlDisplay;
    public RectTransform healBarre;

    void Start()
    {

    }


    void Update()
    {
        lvlDisplay.text = "Lvl: " + motor.lvl;
        healBarre.sizeDelta = new Vector2(Mathf.Lerp(0, 148, motor.heal / 100f), 20f);


        if (buyObjectif == null)
        {
            if (motor.dicPowerLvl[Power.Melee].lvl == 0 && motor.dicPowerLvl[Power.Ranged].lvl == 0)
            {
                if (IAType == TypeIA.ninja)
                {
                    buyObjectif = Power.Melee;
                }

                if(IAType == TypeIA.tank)
                {
                    buyObjectif = Power.Ranged;
                }

                if(IAType == TypeIA.normal)
                {
                    buyObjectif = Random.Range(0,2) == 1 ? Power.Ranged : Power.Melee;
                }
            }
            else
            {
                List<object> possibility = new List<object>();
                possibility.Clear();
                foreach (KeyValuePair<Stats, int> _stat in motor.dicStat)
                {
                    if (_stat.Value < 10)
                    {
                        possibility.Add(_stat.Key);
                    }
                }
                foreach (KeyValuePair<Power, PowerStruc> _stat in motor.dicPowerLvl)
                {
                    possibility.Add(_stat.Key);
                }
                float somme = 0f;
                foreach (object _value in possibility)
                {
                    somme += calculeRatio(_value);
                }
                float rand = Random.Range(0f, somme);
                float i = 0f;
                foreach (object _value in possibility)
                {
                    float luck = calculeRatio(_value);
                    i += luck;
                    if (i > rand)
                    {
                        buyObjectif = _value;
                        break;
                    }
                }
            }
            
        }
        else
        {
            if (buyObjectif.GetType() == typeof(Stats))
            {
                if (motor.xpPoint != 0)
                {
                    motor.AddStat((Stats)buyObjectif);
                    buyObjectif = null;
                    return;
                }
            }
            if (buyObjectif.GetType() == typeof(Power))
            {
                if (motor.xpPoint >= motor.dicPowerLvl[(Power)buyObjectif].price)
                {
                    motor.Upgrade((Power)buyObjectif);
                    buyObjectif = null;
                    return;
                }
            }
        }
        
       


        action = Actions.random;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        float minDist = Mathf.Infinity;
        foreach(Collider _collider in colliders)
        {
            GameObject _go = _collider.gameObject;
            float dist = Vector3.Distance(transform.position, _go.transform.position);
            //Debug.DrawLine(transform.position, _go.transform.position, Color.Lerp(Color.white, Color.black, dist/10f));
            if (_go == gameObject)
            {
                continue;
            }

            if (_go.tag == "Bacterie")
            {
                if (action == Actions.farm)
                {
                    minDist = Mathf.Infinity;
                }

                if (_go.GetComponent<BacteriMotor>().lvl - 3 > motor.lvl  )
                {
                    if (action != Actions.flee)
                    {
                        minDist = Mathf.Infinity;
                    }
                    if (dist < minDist)
                    {
                        action = Actions.flee;
                        minDist = dist;
                        target = _go;
                    }
                    
                }

                if (action != Actions.flee && dist < minDist)
                {
                    action = Actions.hunt;
                    minDist = dist;
                    target = _go;
                }
            }
            if (_go.tag == "Heal" && motor.heal < 100)
            {
                if (action != Actions.heal)
                {
                    minDist = Mathf.Infinity;
                }
                if (minDist > dist)
                {
                    minDist = dist;
                    target = _go;
                    action = Actions.heal;
                }
            }
            if (_go.tag == "Food" && action != Actions.flee && action != Actions.hunt && action != Actions.heal && dist < minDist)
            {
                target = _go;
                action = Actions.farm;
                minDist = dist;
            }
        }

        if (action == Actions.hunt || action == Actions.flee)
        {
            if (action == Actions.hunt)
            {

                float dist = Vector3.Distance(transform.position, target.transform.position);
                float minDistAttack = 0f;
                float maxDistAttack = 0f;
                if (IAType == TypeIA.ninja)
                {
                    minDistAttack = 1.5f;
                    maxDistAttack = 3f;
                }

                if (IAType == TypeIA.tank)
                {
                    minDistAttack = 5f;
                    maxDistAttack = 8f;
                }

                if (IAType == TypeIA.normal)
                {
                    minDistAttack = 1.5f;
                    maxDistAttack = 8f;
                }
                if (dist > maxDistAttack)
                {
                    hunt();
                    if( debug)
                    {
                        Debug.Log(dist + "  " + maxDistAttack);

                    }
                }
                if (dist < minDistAttack)
                {
                    flee();
                }


                if (motor.canUse(Power.Melee) && (action == Actions.flee || action == Actions.hunt) &&  dist < 2f)
                {
                    motor.UsePower(Power.Melee);
                }
                if (motor.canUse(Power.Dash) && ((action == Actions.flee && dist < 9f) || (action == Actions.hunt && dist > 9f)))
                {
                    motor.UsePower(Power.Dash);
                }
                if (motor.canUse(Power.Ranged) && (action == Actions.flee || action == Actions.hunt))
                {
                    motor.UsePower(Power.Ranged);
                }
                if (motor.canUse(Power.Ranged) && (action == Actions.flee || action == Actions.hunt) && dist <= 5f)
                {
                    motor.UsePower(Power.Ranged);
                }

            }
            else
            {
                flee();
            }

        }
        if (action == Actions.farm || action == Actions.heal)
        {
            motor.GoDir(target.transform.position);
        }
        if (action == Actions.random)
        {
            if (Vector3.Distance(randomPoint, transform.position) < 5f)
            {
                randomPoint = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
            }
            motor.GoDir(randomPoint);
        }
        else
        {
            randomPoint = transform.position;
        }

    }

    private float calculeRatio(object _obj)
    {
        return ratioBuy[IAType][_obj];
    }

    private void flee()
    {
        Vector3 _dif = target.transform.position - transform.position;
        motor.GoDir(transform.position - _dif);
    }

    private void hunt()
    {
        motor.GoDir(target.transform.position);
    }
}

public enum Actions
{
    random,
    farm,
    hunt,
    flee,
    heal
}

public enum TypeIA
{
    ninja,
    tank,
    normal
}