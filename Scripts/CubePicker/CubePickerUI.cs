using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePickerUI : MonoBehaviour {
	
	public static Action<int> OnGameOver;
	[SerializeField]
	private Image pick, loveBar, stressBar;
	[SerializeField]
	private List<GameObject> strikes = new List<GameObject>();
	public int id;
	private int numberOfStrikes = 0;
	public bool LoveIsMaxed {get;set;}


	public void UpdateStress( float current, float max ){
		UpdateBar( current, max, stressBar );
	}

	public void UpdateLove( float current, float max ){
		UpdateBar( current, max, loveBar );
	}

	public void UpdateNumberOfStrikes(){
		if( numberOfStrikes < strikes.Count ){
			numberOfStrikes++;
			for( int i = 0; i < numberOfStrikes; i++ ){
				strikes[ i ].SetActive( true );
			}
		}
		else{
			Debug.Log( "Game Over" );
			if( OnGameOver != null ){
				OnGameOver( id );
			}
		}
	}

	private void UpdateBar( float current, float max, Image bar ){
        bar.fillAmount = current / max;
    }

	public void SetPick( FallingBlock.Colour colour ){
		switch( colour ){
			case FallingBlock.Colour.BLACK:
			pick.color = Color.black;
			break;
			case FallingBlock.Colour.BLUE:
			pick.color = Color.blue;
			break;
			case FallingBlock.Colour.GREEN:
			pick.color = Color.green;
			break;
			case FallingBlock.Colour.RED:
			pick.color = Color.red;
			break;
			case FallingBlock.Colour.CYAN:
			pick.color = Color.cyan;
			break;
		}
	}

	public int GetNumberOfStrikes( ){
		return numberOfStrikes;
	}
}
