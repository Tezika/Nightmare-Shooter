using UnityEngine;
using System.Collections;

public class ClickTest : MonoBehaviour {
	public  void testClick_1(){
		Application.LoadLevel (1);
	}
	public void testClick_2(string str){
		Debug.Log (str);
	}
	
}
