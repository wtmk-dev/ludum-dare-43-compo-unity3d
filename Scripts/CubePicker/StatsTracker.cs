using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour {

	public static Action<int> OnStressMaxed;
	private int id, round;
	[SerializeField]
	private float timeToStress;
	private float stressTimer, stressLevel, stressMax, loveLevel, loveMax;
	private bool isActive = false;
	private CubePickerUI cubePickerUI;

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
		CubePickerController.OnScore += LogScore;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
		CubePickerController.OnScore -= LogScore;
	}

	void Awake(){
		cubePickerUI = GetComponentInParent<CubePickerUI>();
	}

	void Update(){
		if( isActive ){
			UpdateStressOverTime();
			StressOut();
		}
	}

	private void Init(){
		id = cubePickerUI.id;
		stressLevel = 0;
		loveLevel = 0;
		stressMax = 100f;
		loveMax = 100f;
		SetActive( false );
	}

	private void StartNewRound(){
		round++;
		stressLevel = 0;
		cubePickerUI.UpdateStress( stressLevel, stressMax );
	}

	private void StateChanged( GameController.State state ){
		if( state == GameController.State.START ){
			Init();
		}else if( state == GameController.State.ARGUE ){
			StartNewRound();
			SetActive( true );
		}else if( state == GameController.State.CHOICE ){
			SetActive( false );
		}
	}

	private void SetActive( bool isActive ){
		this.isActive = isActive;
	}

	private void LogScore( int side ){
		if( isActive ){
			if( side ==  0 ){
				GainLove();
			}else if( side == 3 ){
				 stressLevel += 3 + cubePickerUI.GetNumberOfStrikes();
			}else if( side == id ){
				GainLove();
			}else{
				stressLevel += 3 + cubePickerUI.GetNumberOfStrikes();
			}
		}

	}

	private void GainLove(int amount = 0){
		if( loveLevel < loveMax ){
			loveLevel += 2 + cubePickerUI.GetNumberOfStrikes();
			cubePickerUI.UpdateLove( loveLevel, loveMax );
		}else{
			cubePickerUI.LoveIsMaxed = true;
		}
		
	}

	private void UpdateStressOverTime(){
		stressTimer += Time.fixedDeltaTime;
		if( stressTimer > timeToStress ){
			stressTimer = 0;
			stressLevel += 1 + cubePickerUI.GetNumberOfStrikes();
			cubePickerUI.UpdateStress( stressLevel, stressMax );
		}
	}

	private void StressOut(){
		if( stressLevel >= stressMax ){
			loveLevel = loveLevel / 2;
			cubePickerUI.UpdateLove( loveLevel, loveMax );
			if( OnStressMaxed != null ){
				OnStressMaxed( id );
			}
		}
	}
}
