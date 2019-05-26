using UnityEngine;

public class InputEventInterpreter : MonoBehaviour {

   
    public PlayerController playerController;

    public void Do(InputEvent inputEvent)
    {
        switch (inputEvent.eventType)
        {
            case EventType.INVALID:
                return;
            case EventType.STOP_WALKING:
                playerController.Do(PlayerCommand.STOP_WALKING);
                break;
            case EventType.MOVE_LEFT:
                playerController.Do(PlayerCommand.MOVE_LEFT);
                break;
            case EventType.MOVE_RIGHT:
                playerController.Do(PlayerCommand.MOVE_RIGHT);
                break;
            case EventType.JUMP:
                playerController.Do(PlayerCommand.JUMP);
                break;
            case EventType.ATTACK:
                playerController.Do(PlayerCommand.ATTACK);
                break;
            case EventType.DEFEND:
                playerController.Do(PlayerCommand.DEFEND);
                break;
            case EventType.STOP_DEFEND:
                playerController.Do(PlayerCommand.STOP_DEFEND);
                break;
            case EventType.CHANGE_MODE:
                playerController.Do(PlayerCommand.CHANGE_MODE);
                break;
            case EventType.MOUSE_MOVED:
                playerController.SetLookingAt(((MouseInputEvent)inputEvent).MousePosition);
                break;
        }
    }
}