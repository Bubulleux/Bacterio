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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xpBarre.sizeDelta = new Vector2(Mathf.Lerp(0, 294, player.GetComponent<BacteriMotor>().xp), 0f);
        lvlText.text = "Levels: " + player.GetComponent<BacteriMotor>().lvl;
        healBarre.sizeDelta = new Vector2(Mathf.Lerp(0, 294, player.GetComponent<BacteriMotor>().heal / 100f), 0f);
    }

    [Serializable]
    public struct stat
    {
        public Stats name;
        public Color color;
    }
}
