using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
	}

	private void StateChanged( GameController.State state ){
		if( state != GameController.State.START ){
			gameObject.SetActive( false );	
		}else{
			gameObject.SetActive( true );
		}
		
	}
}
