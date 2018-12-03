using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour {

	[SerializeField]
	private GameObject bg,gameovertxt,goRetry,goQuit;
	private Button retry, quit;


	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
	}

	void Awake(){
		retry = goRetry.GetComponent<Button>();
		quit = goQuit.GetComponent<Button>();
		retry.onClick.AddListener( Retry );
		quit.onClick.AddListener( QuitGame );
	}

	private void StateChanged( GameController.State state ){
		Debug.Log( "Gameover state changed " + state );
		if( state == GameController.State.START ){
			//turn off ui
			bg.SetActive( false );
			gameovertxt.SetActive( false );
			goRetry.SetActive( false );
			goQuit.SetActive( false  );
		}else if( state == GameController.State.WIN ){
			bg.SetActive( true );
			gameovertxt.SetActive( true );
			goRetry.SetActive( true );
			goQuit.SetActive( true  );
		}
	}

	private void Retry(){
		SceneManager.LoadScene( "SampleScene" );
	} 

	private void QuitGame(){
		#if UNITY_STANDALONE
        //Quit the application
			Application.Quit();
		#endif
	
			//If we are running in the editor
		#if UNITY_EDITOR
			//Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
