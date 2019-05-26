using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public KeyCode switchModeKey = KeyCode.W;


    public InputEventInterpreter inputEventInterpreter;
    public InputSerializer serializer;
    private Vector3 lastMousePosition = Vector3.zero;
    private ulong currentFrame = 0;
    private readonly List<EventType> buffer = new List<EventType>();

    private bool mouseMoved = false;
   

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool attack = Input.GetButtonDown("Fire1");
        bool defending = Input.GetButton("Fire2");
        bool stopDefending = Input.GetButtonUp("Fire2");
        bool switchMode = Input.GetKeyDown(switchModeKey);

        //HORIZONTAL MOVEMENT
        if (horizontal > 0)
            buffer.Add(EventType.MOVE_RIGHT);
        else if (horizontal < 0)
            buffer.Add(EventType.MOVE_LEFT);
        else
            buffer.Add(EventType.STOP_WALKING);
        //JUMPING
        if (jump)
            buffer.Add(EventType.JUMP);

        //COMBAT
        if (attack)
            buffer.Add(EventType.ATTACK);
        if (defending)
            buffer.Add(EventType.DEFEND);
        if (stopDefending)
            buffer.Add(EventType.STOP_DEFEND);

        if (switchMode)
            buffer.Add(EventType.CHANGE_MODE);

        //UPDATE MOUSE POSITION
        if (Input.mousePosition != lastMousePosition)
            mouseMoved = true;
    }

    private void FixedUpdate()
    {
        ++currentFrame;
        foreach (EventType eventType in buffer)
        {
            InputEvent inputEvent = new InputEvent(eventType, currentFrame);
            inputEventInterpreter.Do(inputEvent);
            serializer.Serialize(inputEvent);

        }


        if (mouseMoved)
        {
            mouseMoved = false;
            lastMousePosition = Input.mousePosition;
            var mouseEvent = new MouseInputEvent(lastMousePosition, currentFrame);
            inputEventInterpreter.Do(mouseEvent);
            serializer.Serialize(mouseEvent);
        }

        if(buffer.Count > 0)
            buffer.Clear();
    }

}
