﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeBehaviour : MonoBehaviour {

    public PlayerAttackManager mode;

    private void Update()
    {
        CurrentMode();
    }
    public void CurrentMode()
    {
        switch (mode.CurrentMode)
        {
            case PlayerAttackManager.Mode.MELEE:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                break;
            case PlayerAttackManager.Mode.MAGE:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }
}
