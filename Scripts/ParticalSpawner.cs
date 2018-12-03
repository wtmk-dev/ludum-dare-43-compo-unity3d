using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSpawner : MonoBehaviour {

	[SerializeField]
	private ParticleSystem leftHopePS,rightHopePS,leftDespairPS,rightDespairPS;

	void OnEnable(){
		CubePickerController.OnScore += ScorePlay;
	}

	void OnDisable(){
		CubePickerController.OnScore -= ScorePlay;
	}

	private void ScorePlay( int side ){
		if( side == 0 ){
			//if( !leftHopePS.isPlaying && !rightHopePS.isPlaying ){
				leftHopePS.Play();
				rightHopePS.Play();
			//}
		} else if ( side == 1 ){
			//if( !leftHopePS.isPlaying ){
				leftHopePS.Play();
			//}

		} else if ( side == 2 ){
			//if( !rightHopePS.isPlaying ){
				rightHopePS.Play();
			//}

		} else if ( side == 3 ){
			//if( !leftDespairPS.isPlaying && !rightDespairPS.isPlaying ){
				leftDespairPS.Play();
				rightDespairPS.Play();
			//}
		}
	}
}
