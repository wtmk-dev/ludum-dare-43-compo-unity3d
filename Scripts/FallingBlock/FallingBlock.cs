using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

	public static readonly int COLOUR_MAX = 5;
	public enum Colour { RED,GREEN,BLUE,BLACK,CYAN }
	private Colour colour;
	private float lifeTime;
	private float lifeTimeMax = 3f;
	private Vector3 startPos;
	private Rigidbody rigidbody;
	private Material material;
	private FallingBlockSpawner spawner;
	private CubePickerController cubePickerController;

	void OnEnable(){
		SetRandomColour();
		ResetLifeTime();
		ResetLocation();
	}

	void OnDisable(){
		ResetVelocity();
		ResetLocation();
	}

	void Update(){
		Die();
	}

	void OnMouseDown() {
		if( cubePickerController != null ){
			cubePickerController.CheckIn( colour );
			spawner.ReturnToPool( this );
		}
	}

	public void Init( Vector3 startPos, FallingBlockSpawner spawner ){
		this.startPos = startPos;
		this.spawner = spawner;
		rigidbody = GetComponent<Rigidbody>();
		material = GetComponent<Renderer>().material;
		SetRandomColour();
		SetActive( false );
	}

	public void SetCubePicker( CubePickerController cubePickerController ){
		this.cubePickerController = cubePickerController;
	}

	public void SetActive( bool isActive ){
		gameObject.SetActive( isActive );
	}

	private void SetRandomColour(){
		if( material != null ){
			int roll = GameController.RandomNumber( 0, COLOUR_MAX );
			switch( roll ){
				case 0:
				colour = Colour.BLACK;
				material.color = Color.black;
				break;
				case 1:
				colour = Colour.BLUE;
				material.color = Color.blue;
				break;
				case 2:
				colour = Colour.GREEN;
				material.color = Color.green;
				break;
				case 3:
				colour = Colour.RED;
				material.color = Color.red;
				break;
				case 4:
				colour = Colour.CYAN;
				material.color = Color.cyan;
				break;
			}
		}
	}

	private void ResetLocation(){
		if( transform.parent != null ){
			transform.position = startPos;
		}
	}

	private void ResetVelocity(){
		if( rigidbody != null ){
			rigidbody.velocity = Vector3.zero;
		}
	}

	private void ResetLifeTime(){
		lifeTime = 0;
	}

	private void Die(){
		lifeTime += Time.fixedDeltaTime;
		if( lifeTime > lifeTimeMax ){
			if( spawner != null ){
				spawner.ReturnToPool( this );
			}
		}
	}

}
