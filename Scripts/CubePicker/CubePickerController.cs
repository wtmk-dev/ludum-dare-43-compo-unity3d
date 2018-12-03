using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePickerController : MonoBehaviour {

	public delegate void GameWon();
	public event GameWon OnGameWon;
	public static Action<int> OnScore; // 0 both 1 left 2 right 3 bad
	
	[SerializeField]
	private float timeTilNextPick;
	private float pickTimer;
	private bool isInSyn, isActive, hasWon, hasScored;
	private FallingBlock.Colour leftColour;
	private FallingBlock.Colour rightColour;
	private CubePickerView view;



	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
	}

	void Awake(){
		isInSyn = false;
	}

	void Update(){
		if( isActive ){
			PickAfterTimer();
		}
	}

	public void Init( CubePickerView view ){
		this.view = view;
		this.view.Init( this );
	}

	private void StateChanged( GameController.State state ){
		if( state == GameController.State.START ){
		}else if( state == GameController.State.ARGUE){
			SetActive( true );
			isInSyn = false;
			hasScored = false;
			DisplayPicks();
		}else if( state == GameController.State.CHOICE ){
			SetActive( false );
		}
	}

	private void SetActive( bool isActive ){
		this.isActive = isActive;
	}

	public void CheckIn( FallingBlock.Colour colour ){
		if( isInSyn ){
			if( colour == leftColour ){
				DisplayPicks();
				ResetPicTimer();
				timeTilNextPick -= 0.005f;
				hasScored = true;
				if( OnScore != null ){
					OnScore( 0 ); //both
				}
			}else{
				isInSyn = false;
				DisplayPicks();
			}
		}
		else{
			if( colour == leftColour && colour == rightColour ){
				isInSyn = true;
				DisplayPicks();
				ResetPicTimer();
				timeTilNextPick -= 0.001f;
				if( OnScore != null ){
					OnScore( 0 ); //both
				}
			}
			else if( colour == leftColour ){
				DispalyOneSidePick( true );
				timeTilNextPick -= 0.001f;
				hasScored = false;
				if( OnScore != null ){
					OnScore( 1 ); //left
				}
			}
			else if( colour == rightColour ){
				DispalyOneSidePick( false );
				timeTilNextPick -= 0.001f;
				hasScored = false;
				if( OnScore != null ){
					OnScore( 2 ); //right 
				}
			}
			else{
				Debug.Log( "Bad stuff happens" );
				timeTilNextPick -= 0.001f;
				hasScored = false;
				if( OnScore != null ){
					OnScore( 3 ); //bad
				}
			}
		}
	}

	public void GiveStrike( int id, int choice ){
		if( id == 2 ){
			if( choice == 1 ){
				view.StrikeRight();
			}else if ( choice == 0 ){
				view.StrikeLeft();
			}
		}else if( id == 1 ){
			if( choice == 1 ){
				view.StrikeLeft();
			}else if ( choice == 0 ){
				view.StrikeRight();
			}
		}
	}

	public void TriggerGameWin(){
		if( !hasWon ){
			hasWon = true;
			if( OnGameWon != null ){
				OnGameWon();
			}
		}else { return; }
	}

	private void DisplayPicks(){
		if( isInSyn ){
			leftColour = SetRandomColour();
			rightColour = leftColour;
			view.UpdateBoth( leftColour );
		}
		else{
			leftColour = SetRandomColour();
			view.UpdateLeft( leftColour );
			rightColour = SetRandomColour();
			view.UpdateRight( rightColour );
		}
	}

	private void DispalyOneSidePick( bool isLeft ){
		if( !isLeft ){
			rightColour = SetRandomColour();
			view.UpdateRight( rightColour );
		}else{
			leftColour = SetRandomColour();
			view.UpdateLeft( leftColour );
		}
	}

	private void PickAfterTimer(){
		pickTimer += Time.fixedDeltaTime;
		if( pickTimer > timeTilNextPick ){
			pickTimer = 0;

			if( hasScored ){
				isInSyn = true;
				hasScored = false;
			}else{
				isInSyn = false;
			}

			DisplayPicks();
		}

		view.UpdateFuseForTimer( pickTimer, timeTilNextPick );
	}

	private void ResetPicTimer(){
		pickTimer = 0;
	}

	private FallingBlock.Colour SetRandomColour(){
		int roll = GameController.RandomNumber( 0, FallingBlock.COLOUR_MAX );
		FallingBlock.Colour colour;
		switch( roll ){
			case 0:
			return colour = FallingBlock.Colour.BLACK;
			case 1:
			return colour = FallingBlock.Colour.BLUE;
			case 2:
			return colour = FallingBlock.Colour.GREEN;
			case 3:
			return colour = FallingBlock.Colour.RED;
			case 4:
			return colour = FallingBlock.Colour.CYAN;
			default: return colour = FallingBlock.Colour.BLACK;
		}
	}
	
}
