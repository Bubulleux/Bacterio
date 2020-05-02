using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxFood;
    public int maxEnemy;
    [SerializeField]
    private int foodCount;
    private int enemyCount;
    public GameObject food;
    public GameObject enemy;
    public Transform foods;
    public Transform enemys;
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
            go.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
            foodCount++;

        }

        enemyCount = enemys.childCount;
        while (enemyCount < maxEnemy)
        {
            GameObject go = Instantiate(enemy, new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), Quaternion.identity, enemys);
            int _lvl = Random.Range(player.GetComponent<BacteriMotor>().lvl <= 10 ? 1 : player.GetComponent<BacteriMotor>().lvl - 10, player.GetComponent<BacteriMotor>().lvl + 10);
            go.GetComponent<BacteriMotor>().lvl = _lvl;
            go.GetComponent<BacteriMotor>().xpPoint = _lvl;
            go.GetComponent<EnemyIA>().IAType = (TypeIA)Random.Range(0, 3);
            enemyCount++;
        }
    }
}
