using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

	[SerializeField]
	private GameObject goFade;
	private Image startImg;

	void OnEnable(){
		GameController.OnStateChanged += StateChanged;
	}

	void OnDisable(){
		GameController.OnStateChanged -= StateChanged;
	}

	void Awake(){
		startImg = goFade.GetComponent<Image>();
		goFade.SetActive( false );
	}

	private void StateChanged( GameController.State state ){
		// if( state == GameController.State.START ){
		// 	startImg.enabled = true;
		// 	StartCoroutine( FadeImage( true, startImg ) );	
		// } else if( state == GameController.State.ARGUE ){
		// 	startImg.enabled = true;
		// 	StartCoroutine( FadeImage( true, startImg ) );
		// } else if( state == GameController.State.CHOICE ){
		// 	startImg.enabled = true;
		// 	StartCoroutine( FadeImage( true, startImg ) );
		// } else if( state == GameController.State.WIN ){
		// 	startImg.enabled = true;
		// 	StartCoroutine( FadeImage( true, startImg ) );
		// } else if( state == GameController.State.GAMEOVER ){
			
		// }
		goFade.SetActive( true );
		StartCoroutine( FadeImage( true, startImg ) );
	}

	IEnumerator FadeImage(bool fadeAway, Image fadeImage ) {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                fadeImage.color = new Color(1, 1, 1, i);
                yield return new WaitForEndOfFrame();
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                fadeImage.color = new Color(1, 1, 1, i);
                yield return new WaitForEndOfFrame();
            }
        }

		OnFadeImageComplete();
	}

	private void OnFadeImageComplete(){
		goFade.SetActive( false );
		StopCoroutine( FadeImage( false, startImg ) ); 
	}
}
