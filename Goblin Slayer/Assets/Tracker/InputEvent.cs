using System;
using UnityEngine;
public enum EventType
{
    INVALID,
    STOP_WALKING,
    MOVE_LEFT,
    MOVE_RIGHT,
    JUMP, 
    ATTACK,
    DEFEND,
    STOP_DEFEND,
    CHANGE_MODE,
    MOUSE_MOVED
}

public class InputEvent
{
private static EventType StringToEventType(string s)
{
    if (s == "STOP_WALKING")return EventType.STOP_WALKING;
    if (s == "MOVE_LEFT")   return EventType.MOVE_LEFT;
    if (s == "MOVE_RIGHT")  return EventType.MOVE_RIGHT;
    if (s == "JUMP")        return EventType.JUMP;
    if (s == "ATTACK")      return EventType.ATTACK;
    if (s == "DEFEND")      return EventType.DEFEND;
    if (s == "STOP_DEFEND") return EventType.STOP_DEFEND;
    if (s == "CHANGE_MODE") return EventType.CHANGE_MODE;
    if (s.Contains("MOUSE_MOVED")) return EventType.MOUSE_MOVED;
    return EventType.INVALID;
}

private static bool IsReapetable(EventType eventType)
{
        return
            eventType == EventType.MOVE_LEFT    ||
            eventType == EventType.MOVE_RIGHT   ||
            eventType == EventType.STOP_WALKING ||
            eventType == EventType.DEFEND;
}

    public static InputEvent Parse(string serialized)
    {
        if(string.IsNullOrEmpty(serialized)) return null;

        string[] data = serialized.Split(':');
        EventType eventType = StringToEventType(data[0]);
        ulong frame = ulong.Parse(data[1]);

        if (eventType == EventType.INVALID)
            throw new System.Exception("Error parsing the string: '" + serialized + '\'');
        else if (eventType == EventType.MOUSE_MOVED)
            return MouseInputEvent.Parse(serialized, frame);
        else
            return new InputEvent(eventType, frame);

    }
    public InputEvent(EventType eventType, ulong frame)
    {
        this.eventType = eventType;
        this.frame = frame;
        this.repeteable = IsReapetable(eventType);
    }
    public virtual string Serialize()
    {
        return eventType.ToString() + ':' + frame.ToString();
    }
    public EventType eventType{ get; private set;}
    public ulong frame { get; private set; }
    public bool repeteable { get; private set; }
}
public class MouseInputEvent : InputEvent
{
    public MouseInputEvent(Vector3 mousePosition, ulong frame):
        base(EventType.MOUSE_MOVED, frame)
    {
        MousePosition = mousePosition;
    }

    public override string Serialize()
    {
        return eventType.ToString() + MousePosition.ToString()  + ':' + frame;
    }
    public static MouseInputEvent Parse(string source, ulong frame)
    {
        source.Replace(" ","");
        string[] data = source.Split('(', ',', ')');
        //string[0] == "MOUSE_MOVED" we already know, so we skip it
        float x = float.Parse(data[1]);
        float y = float.Parse(data[2]);
        return new MouseInputEvent(new Vector3(x, y, 0), frame);
    }

    public Vector2 MousePosition { get; private set; }
}