using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    public GameObject player;

    public RectTransform xpBarre;
    public Text lvlText;
    public RectTransform healBarre;
    public Text xpPointDisplay;
    public Text healText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xpBarre.sizeDelta = new Vector2(0f, Mathf.Lerp(0, 300, player.GetComponent<BacteriMotor>().xp));
        lvlText.text = "Lvl: " + player.GetComponent<BacteriMotor>().lvl;
        healBarre.sizeDelta = new Vector2(Mathf.Lerp(0, 400, player.GetComponent<BacteriMotor>().heal / 100f), 0f);
        xpPointDisplay.text = "XP: " + player.GetComponent<BacteriMotor>().xpPoint;
        healText.text = player.GetComponent<BacteriMotor>().heal.ToString();
    }

    [Serializable]
    public struct stat
    {
        public Stats name;
        public Color color;
    }
}
