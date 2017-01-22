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
	public GameObject[] WinScreenUI;
	public PlanetSpawner activePlanets;
	public ScoreManager scoreManager;

	int clickCount = 0;

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
			if (Input.GetMouseButtonDown (0)) {
				clickCount++;

			}
			if (clickCount > 2) {
				TransitionFromWin ();
				TransitionToStart ();
				clickCount = 0;
			}
			break;
		}
	}

	void TransitionToStart()
	{
		currentState = GameState.Start;
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
		currentState = GameState.Game;
		Time.timeScale = 1;
	}
	void TransitionFromGame()
	{
		//clean up planets
		GameObject[] planets = GameObject.FindGameObjectsWithTag ("Planet");
		foreach (GameObject obj in planets) {
			Destroy (obj);
		}

		
	}
	void TransitionToWin(int winnerID)
	{
		currentState = GameState.Win;
		Time.timeScale = 0;
		WinScreenUI[winnerID-1].SetActive (true);
		scoreManager.ResetScores ();
		activePlanets.resetPlanetTotal ();

	}
	void TransitionFromWin()
	{
		foreach (GameObject obj in WinScreenUI) {
			obj.SetActive (false);
		}
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

