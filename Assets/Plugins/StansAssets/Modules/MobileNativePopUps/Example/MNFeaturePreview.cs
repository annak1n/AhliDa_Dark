////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class MNFeaturePreview : MonoBehaviour {

	protected GUIStyle style;

    public int buttonWidth = 200;
    public int buttonHeight = 50;
    public float StartY = 20;
    public float StartX = 10;



    public float XStartPos = 10;
    public float YStartPos = 10;

    public float XButtonStep = 220;
    public float YButtonStep = 60;

    public float YLableStep = 40;
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	protected virtual void InitStyles () {
		style =  new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 16;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		
	}


	public virtual void Start() {
		InitStyles();
	}

	public void UpdateToStartPos() {
		StartY = YStartPos;
		StartX = XStartPos;
	}
}

