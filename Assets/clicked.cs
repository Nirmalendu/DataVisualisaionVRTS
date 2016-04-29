using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class clicked : MonoBehaviour {
	public Text a;

	public void OnClick(){
		//dataPlot.xRotate ();
		a.text = dataPlot.selectedX.ToString ();
	}
}
