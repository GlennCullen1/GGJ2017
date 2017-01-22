using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AirConsoleProcessor : MonoBehaviour {

    public TestExplosionOnClick explosionScript;
    public GameStateManager manager;

	// Use this for initialization
	void Awake () {
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
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
    }

    void OnConnect(int device_id)
    {
        Debug.Log("CONNECTED PLAYER1: " + AirConsole.instance.GetControllerDeviceIds()[0] + " DeviceID: " + device_id);

        if (manager.ConnectedPlayers == null)
            manager.ConnectedPlayers = new List<int>();
        /*manager.ConnectedPlayers.Clear();
        foreach(int playerID in AirConsole.instance.GetControllerDeviceIds())
        {
            manager.ConnectedPlayers.Add(playerID);
        }*/

        manager.ConnectedPlayers = AirConsole.instance.GetControllerDeviceIds();

        var message = new
        {
            action = "playerConnect",
            playerId = manager.ConnectedPlayers.IndexOf(device_id)
        };

        AirConsole.instance.Message(device_id, message);
    }

    void OnDisconnect(int device_id)
    {
        manager.ConnectedPlayers = AirConsole.instance.GetControllerDeviceIds();
    }
}
