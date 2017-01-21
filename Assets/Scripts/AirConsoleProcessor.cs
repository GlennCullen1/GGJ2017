using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AirConsoleProcessor : MonoBehaviour {

    public TestExplosionOnClick explosionScript;

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
                int maxWidth = (int)data["screenSize"]["divWidth"];
                int maxHeight = (int)data["screenSize"]["divHeight"];
                Debug.Log("POSITION RECIEVED: " + pos_x + "/" + maxWidth + ", " + pos_y + "/" + maxHeight);

                Vector3 screenCoords = new Vector3(
                    ((float)pos_y / (float)maxHeight) * Screen.width,
                    ((float)pos_x / (float)maxWidth) * Screen.height,
                    0f);
                explosionScript.CauseExplosionAtPoint(explosionScript.CastRayToWorld(screenCoords), explosionScript.radius, explosionScript.power);
                break;
            default:
                break;
        }

        //Debug.Log("Recieved message from " + from + ": " + (string)data);
    }
}
