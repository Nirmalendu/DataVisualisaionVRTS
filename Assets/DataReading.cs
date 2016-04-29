using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Collections;
//using UnityEditor;
public class DataReading : MonoBehaviour
{
	public GameObject cubeup, cubedown, cylinderup, cylinderdown, cylinderupthin, cylinderdownthin, capsuledown, capsule, cube, sphere, cylinder;
    public Text legend;
	public float al;
	public Text xCol, yCol, zCol, cCol;
	public static float maxX,maxY,maxZ,maxColour;
	public GameObject controller;
	Vector3 controlPanel;
	private GameObject marker;
	int minCol =4;
	int maxCol =21;
	//public int totalCol =21;
	 int selectedX, selectedY, selectedZ, selectColour;
	GameObject point;
	int mode;
	int cycle_times;
	string[] rows;
    private bool[] enabledClasses;
	Vector3 positionLabel, myPos;
	Quaternion myRot;
	GameObject txtMeshTransform;
	public GameObject prefab;
	int swapPos;
    // Use this for initialization
    void Start()
    {
	    mode = 0;

		controller = GameObject.FindGameObjectWithTag("fps");
		controlPanel = new Vector3 (594.0F, 264.0F, 179.0F);
	    cycle_times = 5;
	    Vector3 a = new Vector3(0, 0, 0);
        legend.text = "1-Engineering" + "\n" + "2-Business" + "\n" + "3-Physical Sciences" + "\n" +
       "4-Law & Public Policy" + "\n" +
       "5-Computers & Mathematics" + "\n" +
       "6-Agriculture & Natural Resources" + "\n" +
         "7-Industrial Arts & Consumer Services" + "\n" +
        "8-Arts" + "\n" +
       "9-Health" + "\n" + "Social Science";
        selectedX = minCol;
        selectedY = minCol + 1;
        selectedZ = minCol + 2;
        selectColour = minCol + 3;

        //string path1 = AssetDatabase.GetAssetPath(dataFile);
        string data = System.IO.File.ReadAllText("assets/recent-grads.txt");
	    rows = data.Split('\n');
	    Debug.Log(rows[0]);
	    setCoordinates(rows, "NONE");
		swapPos = -1;
        enabledClasses = new bool[10];
        for (int k = 0; k < enabledClasses.Length; k++)
            enabledClasses[k] = true;
        


	    //for (int start = 1; start < rows.Length - 1;start++)
	    // {
	    //    string[] coordinates = rows[start].Split(',');
	    //    Debug.Log(coordinates[2]);
	    //Vector3 axis = 
	    //  Vector3 z = new Vector3((float.Parse(coordinates[selectedZ])) * 100f, (float.Parse(coordinates[selectedY])) * 100f, (float.Parse(coordinates[selectedX])) * 100f);
	    //   point = (GameObject)Instantiate(marker, z, Quaternion.identity);
	    //    point.GetComponent<Renderer>().material.color = new Color(float.Parse(coordinates[selectedZ]) / float.Parse(coordinates[selectColour]), float.Parse(coordinates[selectedY]) / float.Parse(coordinates[selectColour]), float.Parse(coordinates[selectedX]) / float.Parse(coordinates[selectColour]), al);


	    //    Debug.Log(coordinates[3]);
	    // }

	    //Debug.Log(rows[5]);
	    //point = (GameObject)Instantiate(cube, a, Quaternion.identity);
	}

	    // Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(1)){
			

			if(controller.transform.position != controlPanel){
				myPos = controller.transform.position;
				myRot = controller.transform.localRotation;
			}
			if(swapPos == -1){
				controller.transform.position = controlPanel;
				controller.transform.localRotation = Quaternion.Euler (0, 90, 0);
			}

			else{
				controller.transform.position = myPos;
				controller.transform.localRotation = myRot;
			}
			swapPos = swapPos * -1;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < gos.Length; i++)
				if (gos[i].tag.Contains("refresh") || gos[i].tag.Contains("txtMesh"))
                    Destroy(gos[i]);
            
			setCoordinates(rows, "R");
		}
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < gos.Length; i++)
				if (gos[i].tag.Contains("refresh") || gos[i].tag.Contains("txtMesh"))
                    Destroy(gos[i]);

            setCoordinates(rows, "T");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < gos.Length; i++)
				if (gos[i].tag.Contains("refresh") || gos[i].tag.Contains("txtMesh"))
                    Destroy(gos[i]);

            setCoordinates(rows, "Y");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < gos.Length; i++)
				if (gos[i].tag.Contains("refresh") || gos[i].tag.Contains("txtMesh"))
                    Destroy(gos[i]);
			
			

            setCoordinates(rows, "U");
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
            showClasses(0);
		if (Input.GetKeyDown(KeyCode.Alpha1))
            showClasses(1);
		if (Input.GetKeyDown(KeyCode.Alpha2))
            showClasses(2);
		if (Input.GetKeyDown(KeyCode.Alpha3))
            showClasses(3);
		if (Input.GetKeyDown(KeyCode.Alpha4))
            showClasses(4);
		if (Input.GetKeyDown(KeyCode.Alpha5))
            showClasses(5);
		if (Input.GetKeyDown(KeyCode.Alpha6))
            showClasses(6);
		if (Input.GetKeyDown(KeyCode.Alpha7))
            showClasses(7);
		if (Input.GetKeyDown(KeyCode.Alpha8))
            showClasses(8);
		if (Input.GetKeyDown(KeyCode.Alpha9))
            showClasses(9);
    }
	void setCoordinates(string[] rows, string type)
	{

    if (type == "NONE") ;
    if (type == "R")
        selectedX++;
    if (type == "T")
        selectedY++;
    if (type == "Y")
        selectedZ++;
    if (type == "U")
        selectColour++;

    if (selectedX == maxCol)
        selectedX = minCol;
    if (selectedY == maxCol)
        selectedY = minCol;
    if (selectedZ == maxCol)
        selectedZ = minCol;
    if (selectColour == maxCol)
        selectColour = minCol;

    List<float> listX = new List<float>();
		List<float> listY = new List<float>();
		List<float> listZ = new List<float>();
		List<float> listColour = new List<float>();

		for (int start = 1; start < rows.Length - 1; start++)
		{
			string[] coordinates = rows[start].Split(',');
			/*Debug.Log (coordinates [selectedX]);
			Debug.Log (coordinates [selectedZ]);
			Debug.Log (coordinates [selectedY]);
			Debug.Log (coordinates [selectColour]);*/

			listX.Add(float.Parse(coordinates[selectedX]));
			listY.Add(float.Parse(coordinates[selectedY]));
			listZ.Add(float.Parse(coordinates[selectedZ]));
			listColour.Add(float.Parse(coordinates[selectColour]));
		}
		    maxX = listX.Max();
		    maxY = listY.Max();
		    maxZ = listZ.Max();
		    maxColour = listColour.Max ();
		Debug.Log (maxX + "  " + maxY + "  " + maxZ);

		for (int start = 1; start < rows.Length - 1; start++)
		{
			GameObject shapeObject;
			string[] coordinates = rows[start].Split(',');

			Vector3 axis = new Vector3((float.Parse(coordinates[selectedX]) / maxX) * 1000f, (float.Parse(coordinates[selectedY])/maxY) * 1000f, (float.Parse(coordinates[selectedZ])/maxZ) * 1000f);
            int shapeType;
            shapeObject = setShape(coordinates[3], out shapeType);
			point = (GameObject)Instantiate(shapeObject, axis, Quaternion.identity);
			point.gameObject.tag = "refresh"+ shapeType.ToString();
			positionLabel = new Vector3 (point.transform.position.x + 1, point.transform.position.y + 1, point.transform.position.z + 1);
			/*txtMeshTransform = (GameObject)Instantiate(prefab,positionLabel,Quaternion.identity);
			txtMeshTransform.GetComponent<TextMesh>().text = coordinates[2];
			Transform cameraloc = GameObject.FindGameObjectWithTag("MainCamera").transform;
			txtMeshTransform.transform.LookAt (cameraloc);
			txtMeshTransform.tag = "textMesh";
			txtMeshTransform.transform.Rotate (new Vector3 (0, 180, 0));
			//major.text = coordinates [2];*/
			point.GetComponent<Renderer> ().material.color = new Color (float.Parse (coordinates [selectColour]) / maxColour, float.Parse (coordinates [selectColour]) / maxColour, float.Parse (coordinates [selectColour]) / maxColour);

		}
		string[] labels = rows [0].Split (',');
		xCol.text = labels[selectedX].ToString ();
		yCol.text = labels[selectedY].ToString ();
		zCol.text = labels[selectedZ].ToString ();
		cCol.text = labels[selectColour].ToString ()+ " is colour";
        /*
		System.Random r = new System.Random ();
		selectedX = r.Next (minCol, maxCol);
		while (selectedY == selectedX) {
			selectedY = r.Next (minCol, maxCol);
		}
		while (selectedZ == selectedX && selectedZ == selectedY) {
			selectedZ = r.Next (minCol, maxCol);
		}
		selectColour = r.Next (minCol, maxCol);
        */
		//mode = ++mode % cycle_times;
		//selectedX++;
		//selectedY++;
		//selectedZ++;
		//selectColour++;
		    
	
	}


	public GameObject setShape(string type,out int shapeType)
	{
        shapeType = 0;
		switch (type)
		{
		case "Engineering":
			marker = cube;
            shapeType = 1;
			break;
		case "Business":
			marker = sphere;
            shapeType = 2;
            break;
		case "Physical Sciences":
			marker = cubeup;
            shapeType = 3;
            break;
		case "Law & Public Policy":
			marker = capsuledown;
            shapeType = 4;
            break;
		case "Computers & Mathematics":
            shapeType = 5;
            marker = cubedown;
			break;
		case "Agriculture & Natural Resources":
            shapeType = 6;
            marker = cylinderup;
			break;
		case "Industrial Arts & Consumer Services":
            shapeType = 7;
            marker = cylinderdown;
			break;
		case "Arts":
			marker = cylinderupthin;
            shapeType = 8;
            break;
		case "Health":
			marker = cylinderdownthin;
            shapeType = 9;
            break;
		case "Social Science":
            shapeType = 0;
            marker = capsule;
			break;
		default:
			marker = capsule;
            shapeType = 0;
            break;
		}
		return marker;

	}

    public void showClasses(int classType)
    {
        bool newStatus = !enabledClasses[classType];
        enabledClasses[classType] = newStatus;
        GameObject[] go = GameObject.FindGameObjectsWithTag("refresh"+ classType);
        foreach (GameObject ob in go)
        {
            MeshRenderer render = ob.GetComponent<MeshRenderer>();
            render.enabled = newStatus;
        }
    }
    public static string getTypeFromInt(int type)
    {
        
        if (type == 0) return "Social Science";
        if (type == 1) return "Engineering";
        if (type == 2) return "Business";
        if (type == 3) return "Physical Sciences";
        if (type == 4) return "Law & Public Policy";
        if (type == 5) return "Computers & Mathematics";
        if (type == 6) return "Agriculture & Natural Resources";
        if (type == 7) return "Industrial Arts & Consumer Services";
        if (type == 8) return "Arts";
        if (type == 9) return "Health";
        return "NONE";
    }
}

