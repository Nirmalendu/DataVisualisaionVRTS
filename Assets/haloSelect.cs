using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class haloSelect : MonoBehaviour {
	public Text stateName;
	public GameObject[] states;
	int activatorFlag;
	// Use this for initialization
	void Start () {
		activatorFlag = -1;
	}
	
	// Update is called once per frame
	public void OnClick () {
        states = (GameObject[])FindObjectsOfType(typeof(GameObject));
        //Debug.Log(stateName.text);
        activatorFlag = activatorFlag * -1;

        if (activatorFlag == 1) {
            GetComponent<Image>().color = Color.green;
            for (int i = 0; i < states.Length; i++)
				if (states [i].name.Contains (stateName.text.ToString ())) {
                    Debug.Log(states[i].name);
                    Behaviour h = (Behaviour)states [i].GetComponent ("Halo");
					h.enabled = true;
				}
		} else {
            GetComponent<Image>().color = Color.white;
            for (int i = 0; i < states.Length; i++)
				if (states [i].name.Contains (stateName.text.ToString ())) {
					Behaviour h = (Behaviour)states [i].GetComponent ("Halo");
					h.enabled = false;
				}
		}
	}	
			
}
