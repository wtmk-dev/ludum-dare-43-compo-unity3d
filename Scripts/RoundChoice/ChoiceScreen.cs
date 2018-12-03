using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScreen : MonoBehaviour {

	[SerializeField]
	private GameObject goLeft, goRight;
	private ChoiceUI cUIL, cUIR;

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
		StatsTracker.OnStressMaxed += StressMaxed;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
		StatsTracker.OnStressMaxed -= StressMaxed;
	}

	void Awake(){
		cUIL = goLeft.GetComponent<ChoiceUI>();
		cUIR = goRight.GetComponent<ChoiceUI>();
	}

	private void StateChanged( GameController.State state ){
		if( state != GameController.State.CHOICE ){
			goLeft.SetActive( false );	
			goRight.SetActive( false );
		}
	}

	private void StressMaxed( int id ){
		Debug.Log( "Choice screen stress max" + id );
		if( id == 1 ){
			cUIL.WillActivate = true;
			cUIR.WillActivate = false;
		}
		else if ( id == 2 ){
			cUIL.WillActivate = false;
			cUIR.WillActivate = true;
		}

		goLeft.SetActive( cUIL.WillActivate );
		goRight.SetActive( cUIR.WillActivate );
	}

}
