using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePickerView : MonoBehaviour {


	[SerializeField]
	private GameObject goLeft, goRight;
	[SerializeField]
	private Image fuse;
	private CubePickerUI cpUIL, cpUIR;
	private CubePickerController controller;

	void Awake(){
		cpUIL = goLeft.GetComponent<CubePickerUI>();
		cpUIR = goRight.GetComponent<CubePickerUI>();
	}

	void Update(){
		CheckForWin();
	}

	public void Init( CubePickerController controller ){
		this.controller = controller;
	}

	public void UpdateBoth( FallingBlock.Colour colour ){
		cpUIL.SetPick( colour );
		cpUIR.SetPick( colour );
	}

	public void UpdateLeft( FallingBlock.Colour colour ){
		cpUIL.SetPick( colour );
	}

	public void UpdateRight( FallingBlock.Colour colour ){
		cpUIR.SetPick( colour );
	}

	public void UpdateFuseForTimer( float current, float max ){
		UpdateBar( current, max, fuse );
	}

	public void StrikeLeft(){
		cpUIL.UpdateNumberOfStrikes();
	}

	public void StrikeRight(){
		cpUIR.UpdateNumberOfStrikes();
	}

	public void CheckForWin(){
		if( cpUIL != null && cpUIR != null ){
			if( cpUIL.LoveIsMaxed && cpUIR.LoveIsMaxed ){
				Debug.Log( "you win" );
				controller.TriggerGameWin();
			}
		}
	}

	private void UpdateBar( float current, float max, Image bar ){
        bar.fillAmount = current / max;
    }
	
}
