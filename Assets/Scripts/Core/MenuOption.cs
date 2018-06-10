using UnityEngine;
using System.Collections;

public class MenuOption : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void startGame()
	{
		Application.LoadLevel (level);
	}
	public void exitGame()
	{
		Application.Quit ();
	}
	//Настройки
	public void LowQuality()
	{
		QualitySettings.SetQualityLevel (0,true);
        Debug.Log("Quality settings set to 'LOW'");
    }
	public void MediumQuality()
	{
		QualitySettings.SetQualityLevel (2,true);
        Debug.Log("Quality settings set to 'Medium'");
    }
	public void UltraQuality()
	{
		QualitySettings.SetQualityLevel (4,true);
        Debug.Log("Quality settings set to 'Ultra'");
    }
}