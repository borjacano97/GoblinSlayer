﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(Rage))]
public class BarBehaviour : MonoBehaviour
{

    private Health health;
    private Mana mana;
    private Rage rage;
    public Scrollbar healthBar;
    public Scrollbar manaBar;
    public Transform rageBar;
    public Text ragetext;
    public Text rageModeText;

    //private Health healthBoss;
    //Scrollbar healthBossBar;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        health = player.GetComponent<Health>();
        mana = player.GetComponent<Mana>();
        rage = player.GetComponent <Rage>();

        //Falta la barra de vida del Boss
        //healthBoss = GameObject.Find("Boss").GetComponent<Health>();
    }

    void Update()
    {
        healthBar.size = ((float)health.currentHealth / (float)health.maxHealth);
        manaBar.size = ((float)mana.currentMana / (float)mana.maxMana);
        ragetext.text = (int)rage.percentage + "%";
        rageBar.GetComponent<Image>().fillAmount = rage.currentRage / rage.rageMax;

        switch (rage.rageState)
        {
            case Rage.State.NORMAL:
                rageModeText.text = "NORMAL MODE";
                break;
            case Rage.State.MASACRE:
                rageModeText.text = "MASACRE MODE";
                break;
            case Rage.State.SLAYER:
                rageModeText.text = "SLAYER MODE";
                break;
        }
    }
}