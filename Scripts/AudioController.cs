using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private AudioSource audioSource;
	[SerializeField]
	private List<AudioClip> audioClips;

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
		CubePickerController.OnScore += ScorePlay;
		StatsTracker.OnStressMaxed += StressMaxed;
	}

	void OnDisable(){
		GameController.OnStateChanged += StateChanged;
		CubePickerController.OnScore -= ScorePlay;
		StatsTracker.OnStressMaxed -= StressMaxed;
	}

	
	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	private void ScorePlay( int side ){
		if( side == 0 ){
			PlayOneShot( 6 );
		} else if ( side == 1 ){
			PlayOneShot( 7 );
		} else if ( side == 2 ){
			PlayOneShot( 7 );
		} else if ( side == 3 ){
			PlayOneShot( 10 );
		}
	}

	private void StressMaxed( int x ){
		PlayOneShot( 9 );
	}


	private void StateChanged( GameController.State state ){
		if( state == GameController.State.START ){
			PlayIntro();
		}

		if( state == GameController.State.ARGUE ){
			MainGameLoop();
		}

		if( state == GameController.State.CHOICE ){
			RoundStart();
		}

		if( state == GameController.State.GAMEOVER ){
			GameOver();
		}

		if( state == GameController.State.WIN ){
			WinMusic();
		}
	}

	private void PlayOneShot( int index ){
		audioSource.PlayOneShot( audioClips[ index ] );
	}

	private void RoundStart(){
		if( audioSource != null ){
			audioSource.loop = false;
			SetAudioClip( audioClips[ 1 ] );
		}
	}

	private void MainGameLoop(){
		if( audioSource != null ){
			audioSource.loop = true;
			SetAudioClip( audioClips[ 2 ] );
		}
		
	}

	private void PlayIntro(){
		if( audioSource != null ){
			audioSource.loop = true;
			SetAudioClip( audioClips[ 0 ] );
		}
	}

	private void GameOver(){
		audioSource.loop = true;
		SetAudioClip( audioClips[ 3 ] );
	}

	private void WinMusic(){
		//audioSource.Stop();
		audioSource.loop = true;
		SetAudioClip( audioClips[ 4 ] );
	}

	private void SetAudioClip( AudioClip ac ){
		StopAudio();
		audioSource.clip = ac;
		PlayAudio();
	}

	private void StopAudio(){
		audioSource.Stop();
	}

	private void PlayAudio(){
		audioSource.Play();
	}

	

}
