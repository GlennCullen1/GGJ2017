using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AirConsoleProcessor : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;
	}

    void OnReady(string code)
    {
        Debug.Log("AirConsoleProcessor: AirConsole is ready!");
    }
	
	void OnMessage(int from, JToken data)
    {
        AirConsole.instance.Message(from, "TY!");
        Debug.Log("Data recieved: " + data);
        string action = (string)data["action"];

        switch (action)
        {
            case "move":
                int pos_x = (int)data["clickPos"]["pos_x"];
                int pos_y = (int)data["clickPos"]["pos_y"];
                Debug.Log("POSITION RECIEVED: " + pos_x + ", " + pos_y);
                break;
            default:
                break;
        }

        //Debug.Log("Recieved message from " + from + ": " + (string)data);
    }
}
