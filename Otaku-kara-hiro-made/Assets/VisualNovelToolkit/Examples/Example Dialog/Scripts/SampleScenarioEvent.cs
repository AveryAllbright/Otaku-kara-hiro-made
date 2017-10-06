//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		 /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode]
public class SampleScenarioEvent : MonoBehaviour {
	public Rect m_Rect1 = new Rect( 200 , 100 , 200 , 44 );
	public Rect m_Rect2 = new Rect( 420 , 100 , 200 , 44 );
	
	private bool isPlaying;
		
	void OnFinishScenario(){
		isPlaying = false;
	}
	
	void OnGUI(){
		if( isPlaying ){
			return;	
		}
			
		// Instantiate Resources/SachiTalks.prefab
		if( GUI.Button( m_Rect1 , "SachiTalks" ) ){
			ViNoAPI.PlayScenario( "SachiTalks" );
			isPlaying = true;
		}

		// Instantiate Resources/YoshinoTalks.prefab
		if( GUI.Button( m_Rect2 , "YoshinoTalks" ) ){
			ViNoAPI.PlayScenario( "YoshinoTalks" );			
			isPlaying = true;
		}

	}
}
