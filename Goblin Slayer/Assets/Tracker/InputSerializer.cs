using System;
using System.IO;
using UnityEngine;

public class InputSerializer : MonoBehaviour {

    public string directoryPath = "Assets/Tracker/";
    public string extension = "txt";
    public string fileName = "";
    StreamWriter streamWriter;
    EventType lastEvent = EventType.INVALID;
    private void Start()
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        if (fileName == "")
            fileName += DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");


        streamWriter = new StreamWriter(directoryPath + fileName + '.' + extension, false);
        //streamWriter = new StreamWriter(filePath + "\\TEMP_FILE_NAME_" + DateTime.Now.Ticks, false);
        streamWriter.AutoFlush = true;
    }

    private void OnDestroy()
    {
        if (streamWriter != null)
            streamWriter.Close();
    }

    public void Serialize(InputEvent inputEvent)
    {
        if (lastEvent != inputEvent.eventType)
        {
            //Ignoramos el cambio de posición del ratón porque se produce casi cada frame y nos estropea la comprobación de redundancia
            if (inputEvent.eventType != EventType.MOUSE_MOVED)
                lastEvent = inputEvent.eventType;
            streamWriter.Write(inputEvent.Serialize() + '\n');
        }
    }
}
