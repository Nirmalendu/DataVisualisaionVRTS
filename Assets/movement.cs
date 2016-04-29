using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class movement : MonoBehaviour {
    private bool toggleMove;
    private bool nextMove;
    private float move;
    private int quizCount;
    private bool clearAll;
    Text Gear;
    Text Question;
    private int t;

	// Use this for initialization
	void Start () {

        toggleMove = false;
        quizCount = 0;
        clearAll = false;
        move = 0;
    }
	
	// Update is called once per frame
	void Update () {

        var d = Input.GetAxis("Mouse ScrollWheel");
        //Gets mouse wheel magnitude
        if (d > 0f)
        {
            // scroll up
            move += d * 10;
            
        }
        else if (d < 0f)
        {
            // scroll down
            move += d * 10;
            
        }
        //moves with mouse wheel magnitude
        if (Input.GetButton("Fire1"))
        {
            transform.position = transform.position + Camera.main.transform.forward * move * 10 * Time.deltaTime;
        }
        
    }
 
}
