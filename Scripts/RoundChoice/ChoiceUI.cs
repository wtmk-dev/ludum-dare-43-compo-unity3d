using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour {

	private GameController controller;
	[SerializeField]
	private Button hurtThem, hurtYou;
	public bool WillActivate {get;set;}

	void Awake( ){
		hurtThem.onClick.AddListener( HurtThem );
		hurtYou.onClick.AddListener( HurtYou );
	}

	void Start() {
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();	
	}

	private void HurtThem(){
		if( controller != null ){
			controller.SetChoice( 0 );
		}

	}

	private void HurtYou(){
		if( controller != null ){
			controller.SetChoice( 1 );
		}
	}


}
