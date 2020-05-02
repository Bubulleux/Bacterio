using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public Stats stName;
    public Color color;
    public Transform barre;
    public RawImage[] graduation;
    public Button but;
    public BacteriMotor motor;
    public Text stat;
    void Start()
    {
        but.onClick.AddListener(delegate { motor.AddStat(stName); });
        stat.text = stName.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        but.interactable = motor.xpPoint >= 1;
        for (int i = 0; i < barre.childCount; i++)
        {
            barre.GetChild(i).GetComponent<RawImage>().color = color;
            barre.GetChild(i).gameObject.SetActive(i < motor.dicStat[stName]);
        }
    }
}
