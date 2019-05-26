using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automata : MonoBehaviour
{

    public TextAsset file;
    public InputEventInterpreter interpreter;
    private InputEvent lastRepeatableEvent = null;
    Queue<InputEvent> inputEvents;

    private ulong currentFrame;
    void Start()
    {
        currentFrame = 0;
        inputEvents = new Queue<InputEvent>();
        string[] lines = file.text.Split('\n');
        foreach (string line in lines)
        {
            InputEvent inputEvent = InputEvent.Parse(line);
            if (inputEvent != null)
                inputEvents.Enqueue(inputEvent);
        }


        lastRepeatableEvent = inputEvents.Peek();
    }

    private bool outOfEvents = false;

    void FixedUpdate()
    {
        ++currentFrame;
        //si quedan eventos
        while (inputEvents.Count > 0)
        {
            //Si hay que cambiar de evento
            if (inputEvents.Peek().frame == currentFrame)
            {
                //Tomamos el evento de la cola (quitándolo)
                InputEvent inputEvent = inputEvents.Dequeue();
                //Lo usamos
                interpreter.Do(inputEvent);

                //si es repetible, actualizamos el último repetible
                if (inputEvent.repeteable)
                    lastRepeatableEvent = inputEvent;
                //si no, hacemos el repetible
                else interpreter.Do(lastRepeatableEvent);

                
            }
            //si no hay evento que hacer este frame
            else
            {
                interpreter.Do(lastRepeatableEvent);
                break;
            }

        }

        if (!outOfEvents && inputEvents.Count <= 0)
        {
            outOfEvents = true;
            Debug.Log("No more Events in the file!!");
        }

    }
}