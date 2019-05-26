using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaMode : MonoBehaviour
{
    public ManaState modeState;
    private PlayerAttackManager pl;
    public enum ManaState { NORMAL, BATTLE, CRITIC }
    private Mana mn;
    private Health health;
    public float autoManaNormal;
    public float autoManaBattle;
    public float autoManaCritic;
    public float IncrementoModoGuerrero;

    private void Start()
    {
        pl = GetComponent<PlayerAttackManager>();
        mn = GetComponent<Mana>();
        health = GetComponent<Health>();
    }

    public void ChangueRegenMana()
    {
        switch (modeState)
        {
            case ManaState.NORMAL:
                mn.autoManaRegenRate = autoManaNormal;
                if (health.GetHP() < health.maxHealth * 3.0f / 4.0f)

                { modeState = ManaState.BATTLE; }
                break;
            case ManaState.BATTLE:
                mn.autoManaRegenRate = autoManaBattle;

                if (health.GetHP() < health.maxHealth * 1.0f / 4.0f)
                {
                    modeState = ManaState.CRITIC;
                }
                else if (health.GetHP() >= health.maxHealth * 3.0f / 4.0f) modeState = ManaState.NORMAL;
                break;
            case ManaState.CRITIC:
                mn.autoManaRegenRate = autoManaCritic;
                if (health.GetHP() > health.maxHealth * 1 / 4)
                {

                    modeState = ManaState.BATTLE;
                }
                break;
        }
        if (pl.CurrentMode == PlayerAttackManager.Mode.MELEE)
        {
            mn.autoManaRegenRate *= IncrementoModoGuerrero;
        }
    }
}

    

