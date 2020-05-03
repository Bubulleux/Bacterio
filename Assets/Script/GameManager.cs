using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxFood;
    public int maxEnemy;
    public int maxHeal;
    private int foodCount;
    private int enemyCount;
    private int healCount;
    public GameObject food;
    public GameObject enemy;
    public GameObject heal;
    public Transform foods;
    public Transform enemys;
    public Transform heals;
    public GameObject player;
    public GameObject HUD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUD.SetActive(player != null);
        if (player == null)
        {
            GameObject.Find("Menu").GetComponent<MenuManager>().GameOver();
        }

        foodCount = foods.childCount;
        while(foodCount < maxFood)
        {
            GameObject go = Instantiate(food, new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), Quaternion.identity, foods);
            go.GetComponent<Xp>().xpCount = 1;
            go.GetComponent<Xp>().size = 1;
            foodCount++;

        }

        enemyCount = enemys.childCount;
        while (enemyCount < maxEnemy)
        {
            GameObject go = Instantiate(enemy, new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), Quaternion.identity, enemys);
            int _lvl = Random.Range(player.GetComponent<BacteriMotor>().lvl <= 5 ? 1 : player.GetComponent<BacteriMotor>().lvl - 5, player.GetComponent<BacteriMotor>().lvl + 1);
            go.GetComponent<BacteriMotor>().lvl = _lvl;
            go.GetComponent<BacteriMotor>().xpPoint = _lvl;
            go.GetComponent<EnemyIA>().IAType = (TypeIA)Random.Range(0, 3);
            enemyCount++;
        }

        healCount = heals.childCount;
        while (healCount < maxHeal)
        {
            GameObject go = Instantiate(heal, new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), Quaternion.identity, heals);
            healCount++;
        }
    }
}
