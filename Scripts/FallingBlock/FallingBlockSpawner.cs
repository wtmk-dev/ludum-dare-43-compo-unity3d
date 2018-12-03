using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockSpawner : MonoBehaviour {
	

	private GameObject prefab;
	[SerializeField] 
	private int MAX_BLOCKS;
	private int spawned = 0;
	[SerializeField]
	private float spawnInterval;
	private float spawnTimer;
	private Vector3 startPos;
	private Queue<FallingBlock> objects  = new Queue<FallingBlock>();
	private bool isActive; 

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
	}

	void Awake( ){
		prefab = Resources.Load( "FallingBlock" ) as GameObject;
		startPos = transform.position;
		LoadBlocks();
	}

	void Update() {
		if( isActive ){
			ActivateFallingBlockAfterTimer();
		}
	}

	private void StateChanged( GameController.State state ){
		if( state == GameController.State.START ){
		}else if( state == GameController.State.ARGUE){
			spawnInterval = 0.5f;
			SetActive( true );
		}else if( state == GameController.State.CHOICE ){
			SetActive( false );
		}
	}

	private void SetActive( bool isActive ){
		this.isActive = isActive;
	}

	private void LoadBlocks(){
		spawned = 0;
		do{
			GameObject obj = GameObject.Instantiate( prefab,transform.position, Quaternion.identity ) as GameObject;
			FallingBlock clone = obj.GetComponent<FallingBlock>();
			obj.transform.parent = transform;
			clone.Init( startPos, this );
			objects.Enqueue( clone );
			spawned++;
		}while( spawned < MAX_BLOCKS );
	}

	public void SetCubePicker( CubePickerController cpc ){
		foreach ( FallingBlock obj in objects ){
			obj.SetCubePicker( cpc );
		}
	}

	public void ReturnToPool( FallingBlock obj ){
		obj.gameObject.SetActive( false );
		objects.Enqueue( obj );
	}

	private void ActivateFallingBlockAfterTimer(){
		spawnTimer += Time.fixedDeltaTime;
		if( spawnTimer > spawnInterval ){
			spawnTimer = 0;
			if( spawnInterval > .29f ){
				spawnInterval -= 0.001f;
			}
			ActivateFallingBlock();
		}
	}

	private void ActivateFallingBlock(){
		FallingBlock clone = objects.Dequeue();
		clone.SetActive( true );
	}
	
}
