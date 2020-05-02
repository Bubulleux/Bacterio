using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerHud : MonoBehaviour
{
    public Power power;
    public Text namePower;
    public Button upgradeBut;
    public RectTransform Reload;
    public GameObject locked;
    public BacteriMotor motor;

    //info
    public Text lvl;
    public Text price;
    public Text ReloadTime;

    void Start()
    {
        namePower.text = power.ToString();
        upgradeBut.onClick.AddListener(delegate { motor.Upgrade(power); });

    }

    // Update is called once per frame
    void Update()
    {
        locked.SetActive(motor.dicPowerLvl[power].lvl == 0);
        lvl.text = "Lvl: " + motor.dicPowerLvl[power].lvl;
        price.text = "Price: " + motor.dicPowerLvl[power].price;
        ReloadTime.text = "Reload Time: " + motor.dicPowerLvl[power].reloadTime + "S";
        upgradeBut.interactable = motor.xpPoint >= motor.dicPowerLvl[power].price;
        Reload.sizeDelta = new Vector2(0f, Mathf.Lerp(0f, 60f, motor.dicPowerLvl[power].reloadCoolDown / motor.dicPowerLvl[power].reloadTime));
    }
}
