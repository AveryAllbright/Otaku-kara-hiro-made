//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ClickAndLoadLevel : MonoBehaviour {

	void OnClickAndLoadLevel( string levelName ){
		if( ! string.IsNullOrEmpty( levelName ) ){
			Application.LoadLevel( levelName ); 
		} 
	}
}
