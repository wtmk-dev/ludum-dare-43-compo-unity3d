using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public delegate void GameStarted();
	public static event GameStarted OnGameStarted;

	private static readonly System.Random random = new System.Random(); 
	private static System.Random rng = new  System.Random(); 
	private static readonly object syncLock = new object();
	public static int RandomNumber(int min, int max){
		lock(syncLock) { // synchronize
			return random.Next(min, max);
		}
	}

	public enum State { START, ARGUE, CHOICE, GAMEOVER, WIN }
	private State state;
	public static Action<GameController.State> OnStateChanged;

	private GameObject cubePicker;
	private CubePickerController cpc;
	private CubePickerView cpv;
	private GameObject fallingBlockSpawner;

	[SerializeField]
	private Vector3 left,mid,right;

	private int currentStressed, strikeTarget;

	void OnEnable(){
		StatsTracker.OnStressMaxed += EndRound;
		CubePickerUI.OnGameOver += GameOver;
	}

	void OnDisable(){
		StatsTracker.OnStressMaxed -= EndRound;
		CubePickerUI.OnGameOver += GameOver;
		if( cpc != null ){
			cpc.OnGameWon -= GameWon;
		}
	}

	void Awake(){
		LoadPrefabs();
	}

	void Start(){
		GameSetUp();
		state = State.START;
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}

	public void Init(){
		state = State.ARGUE;
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}

	private void LoadPrefabs(){
		fallingBlockSpawner = Resources.Load( "Spawner" ) as GameObject;
		cubePicker = Resources.Load( "CubePicker" ) as GameObject;
	}

	private void GameSetUp(){
		cubePicker = Instantiate( cubePicker, transform.position, Quaternion.identity );
		cpc = cubePicker.GetComponent<CubePickerController>();
		cpv = GameObject.FindGameObjectWithTag( "CubePickerView" ).GetComponent<CubePickerView>();
		cpc.Init( cpv );
		cpc.OnGameWon += GameWon;

		GameObject leftSpawn = Instantiate( fallingBlockSpawner, left, Quaternion.identity );
		leftSpawn.GetComponent<FallingBlockSpawner>().SetCubePicker( cpc );
		GameObject midSpawn = Instantiate( fallingBlockSpawner, mid, Quaternion.identity );
		midSpawn.GetComponent<FallingBlockSpawner>().SetCubePicker( cpc );
		GameObject rightSpawn = Instantiate( fallingBlockSpawner, right, Quaternion.identity );
		rightSpawn.GetComponent<FallingBlockSpawner>().SetCubePicker( cpc );
	}

	private void EndRound( int id ){
		Debug.Log( "Round Ended" );
		currentStressed = id;
		state = State.CHOICE;
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}

	private void GameOver( int id ){
		state = State.GAMEOVER;
		Debug.Log( "They left " + id );
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}

	private void GameWon(){
		state = State.WIN;
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}

	public void SetChoice( int choice ){
		cpc.GiveStrike( currentStressed, choice );
		state = State.ARGUE;
		if( OnStateChanged != null ){
			OnStateChanged( state );
		}
	}
}
