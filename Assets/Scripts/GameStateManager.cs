using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum  GameState {Start,Game,Win};

public class GameStateManager : MonoBehaviour
{
	public GameState currentState = GameState.Start;
	public Text waitingPlayers;
	public GameObject StartScreenWaiting;
	public GameObject StartScreenCountdown;
	public GameObject StartScreenUI;
    public int[] ActivePlayers = new int[4];
	public List<int> ConnectedPlayers = new List<int>();    //List of the player ids
	bool countdown = false;
	GameObject WinScreenUI;
	// Use this for initialization
	void Start ()
	{
        ConnectedPlayers = new List<int>();
        ActivePlayers = new int[4];
        ActivePlayers[0] = -1;
        ActivePlayers[1] = -1;
        ActivePlayers[2] = -1;
        ActivePlayers[3] = -1;
        TransitionToStart();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (currentState) {
		case GameState.Start:
			waitingPlayers.text = ConnectedPlayers.Count.ToString ();
			if (ConnectedPlayers.Count >= 4 && !countdown) {
				StartCoroutine (CountDown ());
			}
			break;
		case GameState.Game:
			break;
		case GameState.Win:
			break;
		}
	}

	void TransitionToStart()
	{
		countdown = false;
		Time.timeScale = 0;
		StartScreenUI.SetActive (true);
		StartScreenWaiting.SetActive (true);
		StartScreenCountdown.SetActive (false);
	}
	void TransitionFromStart()
	{
		StartScreenUI.SetActive (false);
	}
	void TransitionToGame()
	{
		Time.timeScale = 1;
	}
	void TransitionFromGame()
	{

	}
	void TransitionToWin(int winnerID)
	{
		Time.timeScale = 0;
		WinScreenUI.SetActive (true);
	}
	void TransitionFromWin()
	{
		WinScreenUI.SetActive (false);
	}

	IEnumerator CountDown()
	{
		countdown = true;
		StartScreenWaiting.SetActive (false);
		StartScreenCountdown.SetActive(true);
		Time.timeScale = 1;
		yield return new WaitForSeconds(1.0f);
		TransitionFromStart ();
		TransitionToGame ();
	}

	public void EndGame(int winnerID)
	{
		TransitionFromGame ();
		TransitionToWin (winnerID);
	}
}

