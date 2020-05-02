using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriMotor : MonoBehaviour
{
    public float speed = 3f;
    public int xpPoint;
    public int lvl;
    public float heal = 100f;
    public float xp;

    public Dictionary<Stats, int> dicStat = new Dictionary<Stats, int>();
    public Dictionary<Power, PowerStruc> dicPowerLvl = new Dictionary<Power, PowerStruc>();

    // Start is called before the first frame update
    void Start()
    {
        dicStat.Add(Stats.Speed, 0);
        dicStat.Add(Stats.Attack, 0);
        dicStat.Add(Stats.BonnusXP, 0);
        dicStat.Add(Stats.Regene, 0);
        dicStat.Add(Stats.Defence, 0);

        dicPowerLvl.Add(Power.Melee, new PowerStruc(Power.Melee));
        dicPowerLvl.Add(Power.Dash, new PowerStruc(Power.Dash));
        dicPowerLvl.Add(Power.Ranged, new PowerStruc(Power.Ranged));
        dicPowerLvl.Add(Power.SlowDown, new PowerStruc(Power.SlowDown));
    }

    // Update is called once per frame
    void Update()
    {
        if (xp >= 1f)
        {
            xp -= 1f;
            xpPoint++;
            lvl++;
        }

        foreach(KeyValuePair<Power, PowerStruc> _power in dicPowerLvl)
        {
            if (_power.Value.reloadCoolDown > 0f)
            {
                dicPowerLvl[_power.Key].reloadCoolDown -= Time.deltaTime;
            }

            if (_power.Value.reloadCoolDown < 0f)
            {
                dicPowerLvl[_power.Key].reloadCoolDown = 0f;
            }
        }
    }

    public void GoDir(Vector3 _where)
    {
        Vector3 _dif = _where - transform.position;
        _dif = new Vector3(_dif.x, 0f, _dif.z);
        transform.rotation = Quaternion.LookRotation(_dif);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            Destroy(other.gameObject);
            xp += 0.5f * Mathf.Pow(0.7f,lvl);
        }
    }
    
    public void AddStat(Stats _stat)
    {
        if (dicStat[_stat] < 10)
        {
            dicStat[_stat]++;
            xpPoint--;
        }
    }

    public void Upgrade(Power _power)
    {
        if (true)
        {
            xpPoint -= dicPowerLvl[_power].price;
            dicPowerLvl[_power].lvl++;
            dicPowerLvl[_power].SetInfo();
        }
    }

    public bool canUse(Power _power)
    {
        PowerStruc _powerClasse = dicPowerLvl[_power];
        return _powerClasse.lvl != 0 && _powerClasse.reloadCoolDown == 0f;
    }

    public void UsePower(Power _power)
    {
        Debug.Log(_power.ToString() + "  Used");

        if (canUse(_power))
        {
            dicPowerLvl[_power].reloadCoolDown = dicPowerLvl[_power].reloadTime;
        }
    }
    
}
public enum Stats
{
    Speed,
    Attack,
    BonnusXP,
    Regene,
    Defence
}

public enum Power
{
    Melee,
    Dash,
    Ranged,
    SlowDown
}

public class PowerStruc
{
    public int lvl;
    public float reloadCoolDown;
    public int price = 1;
    public float reloadTime;
    private Power power;

    public PowerStruc(Power _power)
    {
        power = _power;
        SetInfo();
    }

    public void SetInfo()
    {
        price = lvl + 1;
        reloadTime = (((int)power + 1) * 2) - (lvl * 0.5f);
        reloadTime = reloadTime < 0.2f ? 0.2f : reloadTime;
    }
}