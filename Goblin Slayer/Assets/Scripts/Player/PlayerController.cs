using UnityEngine;

[RequireComponent(typeof(Walker))]
[RequireComponent(typeof(PlayerAttackManager))]
public class PlayerController : MonoBehaviour
{
    private Walker walker;
    private PlayerAttackManager attackManager;
    private Vector2 lookAt;

    private void Start()
    {
        walker = GetComponent<Walker>();
        attackManager = GetComponent<PlayerAttackManager>();
    }

    public void SetLookingAt(Vector2 mousePos)
    {
       attackManager.LookingAtPosition = mousePos;
    }

    public void Do(PlayerCommand command)
    {
        switch (command)
        {
            case PlayerCommand.MOVE_LEFT:
                walker.Walk(Walker.Direction.LEFT);
                break;
            case PlayerCommand.MOVE_RIGHT:
                walker.Walk(Walker.Direction.RIGHT);
                break;
            case PlayerCommand.JUMP:
                attackManager.Jump();
                break;
            case PlayerCommand.STOP_WALKING:
                walker.Stop();
                break;
            case PlayerCommand.ATTACK:
                attackManager.Attack();
                break;
            case PlayerCommand.DEFEND:
                attackManager.Defend();
                break;
            case PlayerCommand.STOP_DEFEND:
                attackManager.StopDefending();
                break;
            case PlayerCommand.CHANGE_MODE:
                attackManager.SwitchMode();
                break;

        }
    }
}


/* 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Walker))]
public class PlayerController : MonoBehaviour
{
    Walker walker;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode switchModeKey;
    public KeyCode attackKey;
    public KeyCode defendKey;
    public KeyCode menuKey;

    private KeyCode lastDirectionKeyPressed;
    private PlayerAttackManager playerAttackManager;
    private PauseBehaviour pauseHUD;
    public bool defending = false;


	void Start () {
        walker = GetComponent<Walker>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
        pauseHUD = GameObject.Find("HUD").GetComponent<PauseBehaviour>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!defending)
        {
            //Walk to the left
            if (Input.GetKey(leftKey))
            {
                lastDirectionKeyPressed = leftKey;
                walker.Walk(Walker.WalkDirection.LEFT);
            }
            //Walk to the right
            else if (Input.GetKey(rightKey))
            {
                lastDirectionKeyPressed = rightKey;
                walker.Walk(Walker.WalkDirection.RIGHT);
            }
            //Stop walking
            if ((Input.GetKeyUp(leftKey) && lastDirectionKeyPressed == leftKey)
              || (Input.GetKeyUp(rightKey) && lastDirectionKeyPressed == rightKey))
                walker.Stop();

            //Jump
            if (Input.GetKeyDown(jumpKey))
                playerAttackManager.Jump();

            if (Input.GetKeyDown(switchModeKey))
            {
                playerAttackManager.SwitchMode();
            }

            if (Input.GetKeyDown(attackKey))
            {
                playerAttackManager.Attack();
            }
        }

        if(Input.GetKeyDown(defendKey))
        {
            defending = true;
            playerAttackManager.Defend();
        }
        if (Input.GetKeyUp(defendKey))
        {
            defending = false;
            playerAttackManager.StopDefending();
        }
        if(Input.GetKeyDown(menuKey))
        {
            pauseHUD.ActivePause();
        }
    }

}
*/
