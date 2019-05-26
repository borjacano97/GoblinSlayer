using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeleeAttacker))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Shield))]
[RequireComponent(typeof(SkillJumper))]

public class PlayerAttackManager : MonoBehaviour
{
    public enum Mode { MELEE, MAGE };
    public Mode CurrentMode { get; private set; }

    private MeleeAttacker meleeAttacker;
    private Shooter shooter;
    private Shield shield;
    private SkillHealing skillHealing;
    private SkillJumper skillJumper;
    public AnimatorControllerParameter warriorController;
    public AnimatorControllerParameter mageController;

    public Vector2 LookingAtPosition { get; set; }

	// Use this for initialization
	void Start () {
        shooter = GetComponent<Shooter>();
        if (!shooter)
            Debug.Log("Dependence not found: shooter");
        if (!(meleeAttacker = GetComponent<MeleeAttacker>()))
            Debug.Log("Dependence not found: meleeAttacker");

        if (!(shield = GetComponent<Shield>()))
            Debug.Log("Dependence not found: shield");

        skillHealing = GetComponent<SkillHealing>();
        skillJumper = GetComponent<SkillJumper>();
    }

    public Vector2 GetLookAtDirection()
    {
        var aux = (Camera.main.ScreenToWorldPoint(LookingAtPosition) - transform.position);
        return new Vector2(aux.x, aux.y).normalized;
    }

    public void SwitchMode()
    {
        CurrentMode = (Mode)((int)++CurrentMode%2);
    }

    public void Attack()
    {
        switch(CurrentMode)
        {
            case Mode.MELEE:
                meleeAttacker.MakeAttack(GetLookAtDirection());
                break;
            case Mode.MAGE:
                shooter.Shoot(GetLookAtDirection());
                break;
        }
    }

    public void Defend()
    {
        switch (CurrentMode)
        {
            case Mode.MELEE:
                shield.ActiveShield(true);
                break;
            case Mode.MAGE:
                skillHealing.Heal();
                break;
        }
    }
    public void StopDefending()
    {
        switch (CurrentMode)
        {
            case Mode.MELEE:
                shield.ActiveShield(false);
                break;
            case Mode.MAGE:
                skillHealing.StopHealing();
                break;
        }
    }
    public void Jump()
    {
        switch (CurrentMode)
        {
            case Mode.MAGE:
                if (skillJumper.toes.OnGround) skillJumper.Jump();
                else skillJumper.MakeADoubleJump(GetLookAtDirection());
                break;
            case Mode.MELEE:
                skillJumper.Jump();
                break;
        }
    }


}
