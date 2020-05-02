using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriMotor : MonoBehaviour
{
    public int xpPoint;
    public int lvl;
    public float heal = 100f;
    public float xp;
    private float regneneCoolDown;
    private float speed = 1f;

    public Dictionary<Stats, int> dicStat = new Dictionary<Stats, int>();
    public Dictionary<Power, PowerStruc> dicPowerLvl = new Dictionary<Power, PowerStruc>();

    public GameObject bullet;

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

        if (heal < 100 && dicStat[Stats.Regene] != 0)
        {
            regneneCoolDown += Time.deltaTime * dicStat[Stats.Regene] / 2f;
            if (regneneCoolDown >= 1f)
            {
                regneneCoolDown = 0f;
                heal++;
            }
        }

        if (heal <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GoDir(Vector3 _where)
    {
        if (_where.x < 100f && _where.x > -100f && _where.y < 100f && _where.y > -100f)
        {
            Vector3 _dif = _where - transform.position;
            _dif = new Vector3(_dif.x, 0f, _dif.z).normalized;
            //transform.rotation = Quaternion.LookRotation(_dif);
            transform.Translate(_dif * 3f * Time.deltaTime * (1 + dicStat[Stats.Speed] * 0.2f) * speed, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            Destroy(other.gameObject);
            xp += 0.5f * Mathf.Pow(0.8f,lvl) * (dicStat[Stats.BonnusXP] + 1);
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

        if (canUse(_power))
        {
            dicPowerLvl[_power].reloadCoolDown = dicPowerLvl[_power].reloadTime;
            if (_power == Power.Melee)
            {
                Collider[] _enemysDegat = Physics.OverlapSphere(transform.position, 1f);
                foreach(Collider enemy in _enemysDegat)
                {
                    if (enemy.tag == "Bacterie" && enemy.gameObject != gameObject && !enemy.isTrigger)
                    {
                        enemy.gameObject.GetComponent<BacteriMotor>().Degat(10 * dicPowerLvl[Power.Melee].lvl);
                    }
                }
            }
            if (_power == Power.Dash)
            {
                StartCoroutine(speedRatio(2f + dicPowerLvl[Power.Dash].lvl * 0.2f, 1f));
            }
            if (_power == Power.Ranged)
            {
                GameObject _bullet = Instantiate(bullet, new Vector3(transform.position.x,0.7f, transform.position.z), Quaternion.identity);
                _bullet.GetComponent<Bullet>().range = 3 + dicPowerLvl[Power.Ranged].lvl;
                _bullet.GetComponent<Bullet>().speed = 2 + dicPowerLvl[Power.Ranged].lvl;
                _bullet.GetComponent<Bullet>().degat = 10 * dicPowerLvl[Power.Ranged].lvl;
                float minDist = Mathf.Infinity;
                GameObject target = null;
                foreach(GameObject _go in GameObject.FindGameObjectsWithTag("Bacterie"))
                {
                    if (_go != gameObject && Vector3.Distance(transform.position, _go.transform.position) < minDist)
                    {
                        target = _go;
                        minDist = Vector3.Distance(transform.position, _go.transform.position);
                    }
                }
                _bullet.GetComponent<Bullet>().target = target;

            }
            if (_power == Power.SlowDown)
            {
                Collider[] _enemysDegat = Physics.OverlapSphere(transform.position, dicPowerLvl[Power.SlowDown].lvl * 2f);
                foreach (Collider enemy in _enemysDegat)
                {
                    if (enemy.tag == "Bacterie" && enemy.gameObject != gameObject && !enemy.isTrigger)
                    {
                        StartCoroutine(enemy.gameObject.GetComponent<BacteriMotor>().speedRatio(0.4f, 1f + dicPowerLvl[Power.SlowDown].lvl * 0.5f));
                    }
                }
            }
        }
    }

    public void Degat(int _degat)
    {
        heal -= Mathf.RoundToInt(_degat * Mathf.Pow(0.9f, dicStat[Stats.Defence]));
    }

    public IEnumerator speedRatio(float _ratio, float _time)
    {
        speed *= _ratio;
        yield return new WaitForSeconds(_time);
        speed /= _ratio;
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