using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxFood;
    public int maxEnemy;
    [SerializeField]
    private int FoodCount;
    private int EnemyCount;
    public GameObject Food;
    public GameObject Enemy;
    public Transform foods;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FoodCount = foods.childCount;
        while(FoodCount < maxFood)
        {
            GameObject go = Instantiate(Food, new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), Quaternion.identity, foods);
            go.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
            FoodCount++;
                           
        }
    }
}
