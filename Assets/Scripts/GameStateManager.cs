using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum  GameState {Start,Game,Win};

public class GameStateManager : MonoBehaviour
{
	public GameState currentState = GameState.Start;
	public Text waitingPlayers;
	public GameObject StartScreenWaiting;
	public GameObject StartScreenCountdown;
	public GameObject StartScreenUI;
	public int ConnectedPlayers = 0;
	bool countdown = false;
	GameObject WinScreenUI;
	// Use this for initialization
	void Start ()
	{
		TransitionToStart();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (currentState) {
		case GameState.Start:
			waitingPlayers.text = ConnectedPlayers.ToString ();
			if (ConnectedPlayers >= 4 && !countdown) {
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

