using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Collections;

public class dataPlot : MonoBehaviour {

    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    float delay = 0.5f;
    public Text X,Y,Z;
    public Text Yeartext;
    public Text changeX, changeY, changeZ;
    public Text debugText, legendText;
    public GameObject cubeup, diamond,  tear, cylinderupthin, hut, donuts, capsule, cube, sphere, cylinder, coneup;
    public  float maxX, maxY, maxZ;
     int minCol = 3;
     int maxCol = 18;
    public static int selectedX, selectedY, selectedZ;
    //public int cRed = 1, cBlue = 2, cGreen = 3, cBlack= 14;
    //int mincolor = 0, maxColor = 33;
	static string[] rows;
    public Material redMat, blueMat, greenMat, blackMat; 
    private bool[] enabledClasses;
    Vector3 positionLabel;
    //public GameObject prefab;
    GameObject point;
    Dictionary<string, int> dictStates;
    Dictionary<int, string> dictPurpose;
    List<string> allObjects;
    int minYear = 2002, maxYear = 2012;
    public int yearShown;
    float overTime = .5f;
    float movementSpeed = 5;

    GameObject controller;
    int swapPos;
    Vector3 controlPanel, myPos;
    Quaternion myRot;
    string[] colNames;
    public static string[] mean;
    public static string[] std;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("fps");
        controlPanel = new Vector3(594.0F, 264.0F, 400.0F);
        swapPos = -1;
        dictStates = new Dictionary<string, int>();
        dictPurpose = new Dictionary<int, string>();
        fillStates();
        fillPurpose();

        selectedX = minCol;
        selectedY = minCol + 1;
        selectedZ = minCol + 2;

        string data = System.IO.File.ReadAllText("assets/norm.txt");
        rows = data.Split('\n');
        setCoordinates(rows);
        colNames = rows[0].Split(',');
        mean = rows[rows.Length - 3].Split(',');
        std = rows[rows.Length - 2].Split(',');
        //Debug.Log(mean[0] + mean[1] + mean[2] + mean[3]);
        X.text = colNames[selectedX];
        Y.text = colNames[selectedY];
        Z.text = colNames[selectedZ];

        changeX.text = colNames[selectedX];
        changeY.text = colNames[selectedY];
        changeZ.text = colNames[selectedZ];


        enabledClasses = new bool[11];
        for (int k = 0; k < enabledClasses.Length; k++)
            enabledClasses[k] = true;
        
        legendText.text = "North (Green)" + "\n" + "East (Yellow)" + "\n" + "West (Blue)" + "\n" + "South (Red)"+ "\n \n"+
            "Adoption - CAPSULE(0)" + "\n" +
            "For Begging- CUBE(1)" + "\n" +
            "Illicit intercourse - cuboidup(2)" + "\n" +
            "Marriage - diamond(3)" + "\n" +
            "For Prostitution - hut(4)" + "\n" +
            "Ransom - tear(5)" + " \n" +
            "Revenge - coneup(6)" + " \n" +
            "Body parts -cylinder(7)" + "\n"+
            "Slavery - cylinderupthin(8)" + "\n" +
            "Unlawaful activity - donut(9)" + "\n" +
            "Others - SPHERE(-)";
        legendText.enabled = false;
        Yeartext.text = yearShown.ToString();
    }
    public static int[] getCor()
    {
        int[] a = { selectedX, selectedY, selectedZ };
        return a;
    }
	public void xRotate()
    {
        
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].tag.Contains("refresh"))
                Destroy(gos[i]);
        selectedX++;
        setCoordinates(rows);
      
    }

	public void yRotate()
    {

        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].tag.Contains("refresh"))
                Destroy(gos[i]);
        selectedY++;
        setCoordinates(rows);

    }
    public void zRotate()
    {

        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].tag.Contains("refresh"))
                Destroy(gos[i]);
        selectedZ++;
        setCoordinates(rows);

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(2))
        {
            legendText.enabled = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            legendText.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!one_click) // first click no previous clicks
            {
                one_click = true;

                timer_for_double_click = Time.time; // save the current time
                                                    // do one click things;
            }
            else
            {
                one_click = false; // found a double click, now reset

                yearShown++;
                Yeartext.text = yearShown.ToString();
                if (yearShown == maxYear) yearShown = minYear;
                moveCoordinates();
            }
        }
        if (one_click)
        {
            // if the time now is delay seconds more than when the first click started. 
            if ((Time.time - timer_for_double_click) > delay)
         {

                //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

                one_click = false;

            }
        }


        //Cycle through all Years
        if (Input.GetKeyDown(KeyCode.X))
        {
            yearShown++;
            Yeartext.text = yearShown.ToString();
            if (yearShown == maxYear) yearShown = minYear;
            moveCoordinates();
        }

        //if X Axis is changed
       
        //if Y Axis is changed
        //if Z Axis is changed
        //if state1(Red) is changed

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
        if (Input.GetKeyDown(KeyCode.Minus))
            showClasses(10);


        if (Input.GetMouseButtonDown(1))
        {


            if (controller.transform.position != controlPanel)
            {
                myPos = controller.transform.position;
                myRot = controller.transform.localRotation;
            }
            if (swapPos == -1)
            {
                controller.transform.position = controlPanel;
                controller.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }

            else {
                controller.transform.position = myPos;
                controller.transform.localRotation = myRot;
            }
            swapPos = swapPos * -1;
        }
    }

    public void showClasses(int classType)
    {
        bool newStatus = !enabledClasses[classType];
        enabledClasses[classType] = newStatus;
        GameObject[] go = GameObject.FindGameObjectsWithTag("refresh" + classType.ToString("D2"));
        foreach (GameObject ob in go)
        {
            MeshRenderer render = ob.GetComponent<MeshRenderer>();
            ob.GetComponent<Collider>().enabled = newStatus;
            render.enabled = newStatus;
        }
    }




     void setCoordinates(string[] rows)
    {

        // Set Mininum for all/////
        if (selectedX == maxCol)  selectedX = minCol;
        if (selectedY == maxCol)  selectedY = minCol;
        if (selectedZ == maxCol)  selectedZ = minCol;
        if (yearShown == maxYear) yearShown = minYear;
        ///////////////////////

        allObjects = new List<string>();
        int index = 0;
        for (int start = 1; start < rows.Length - 3; start++)
        {
            //Debug.Log(rows[start]);
            GameObject shapeObject;
            string[] coordinates = rows[start].Split(',');
          
            if (float.Parse(coordinates[1]) == yearShown)
            {
                 
                Vector3 axis = new Vector3((float.Parse(coordinates[selectedX]) * 500) + 200, (float.Parse(coordinates[selectedY])*500) + 200, (float.Parse(coordinates[selectedZ])*500) + 200);
                int shapeType;
                shapeObject = setShape(coordinates[2], out shapeType);
                point = (GameObject)Instantiate(shapeObject, axis, Quaternion.identity);
                //allObjects.Add(point);
                point.gameObject.tag = "refresh" + shapeType.ToString("D2");
                point.gameObject.name = index.ToString() + "," + coordinates[0];
                allObjects.Add(index.ToString());
               // Debug.Log(coordinates[0] + " " + coordinates[1] + " " + coordinates[2])s;
                point.GetComponent<MeshRenderer>().material = setColor(coordinates[0]);
                //positionLabel = new Vector3(point.transform.position.x + 1, point.transform.position.y + 1, point.transform.position.z + 1);
                index++;
            }
        }
        //debugText.text = allObjects.Count.ToString();
        //string[] labels = rows[0].Split(',');
        // xCol.text = labels[selectedX].ToString();
        //yCol.text = labels[selectedY].ToString();
        //zCol.text = labels[selectedZ].ToString();
        //cCol.text = labels[selectColour].ToString ()+ " is colour";



    }


    void moveCoordinates()
    {


        int index = 0;
       
        for (int start = 1; start < rows.Length - 3; start++)
        {

            string[] coordinates = rows[start].Split(',');
            if (float.Parse(coordinates[1]) == yearShown)
            {
                Vector3 newAxis = new Vector3(float.Parse(coordinates[selectedX]) * 200, float.Parse(coordinates[selectedY]) * 200, float.Parse(coordinates[selectedZ]) * 200);
                //Debug.Log(coordinates[0] + " " + coordinates[1] + " " + coordinates[2]+","+index.ToString());
                GameObject oldGameO = GameObject.Find(allObjects[index]+","+ coordinates[0]);
                //allObjects.Remove(oldGameO);
                Vector3 source = oldGameO.transform.position;
                //oldGameO.transform.Translate((newAxis - source) * movementSpeed * Time.deltaTime);
                //oldGameO.transform.position = Vector3.MoveTowards(oldGameO.transform.position, newAxis , movementSpeed * Time.deltaTime);
                StartCoroutine(go(oldGameO, oldGameO.transform.position, newAxis, overTime));
                //allObjects.Add(oldGameO);
                index++;
            }
            
        }

    }

    IEnumerator go(GameObject gamObj,Vector3 source, Vector3 target, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            gamObj.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        gamObj.transform.position = target;
    }

    public Material setColor(string state)
    {
        Material colorMat = null;
        int colorType = dictStates[state];
        if (colorType == 1) return greenMat;
        if (colorType == 2) return blackMat;
        if (colorType == 3) return redMat;
        if (colorType == 4) return blueMat;

        return colorMat;

    }


    public GameObject setShape(string type, out int shapeType)
    {
        GameObject marker = null;
        shapeType = dictPurpose.FirstOrDefault(x => x.Value == type).Key;
        switch (type)
        {
            case "For Adoption":
                marker = sphere;
                break;
            case "For Begging":
                marker = cube;
                break;
            case "For Illicit intercourse":
                marker = cubeup;
                break;
            case "For marriage":
                marker = diamond;
                break;
            case "For Prostitution":
                marker = hut;
                break;
            case "For Ransom":
                marker = tear;
                break;
            case "For Revenge":
                marker = coneup;
                break;
            case "For Selling body parts":
                marker = cylinder;
                break;
            case "For Slavery":
                marker = cylinderupthin;
                break;
            case "For unlawaful activity":
                marker = donuts;
                break;
            case "Others":
                marker = capsule;
                break;
            default:
                marker = capsule;
                break;
        }
        return marker;
    }

    public void fillPurpose()
    {
        dictPurpose.Add(0, "For Adoption");
        dictPurpose.Add(1, "For Begging");
        dictPurpose.Add(2, "For Illicit intercourse");
        dictPurpose.Add(3, "For marriage");
        dictPurpose.Add(4, "For Prostitution");
        dictPurpose.Add(5, "For Ransom");
        dictPurpose.Add(6, "For Revenge");
        dictPurpose.Add(7, "For Selling body parts");
        dictPurpose.Add(8, "For Slavery");
        dictPurpose.Add(9, "For unlawaful activity");
        dictPurpose.Add(10, "Others");
    }



    public void fillStates()
    {

        dictStates.Add("Andhra Pradesh", 3);
        dictStates.Add("Arunachal Pradesh", 2);
        dictStates.Add("Assam", 2);
        dictStates.Add("Bihar", 1);
        dictStates.Add( "Chhattisgarh",1);
        dictStates.Add("Goa", 4);
        dictStates.Add("Gujarat", 4);
        dictStates.Add("Haryana", 1);
        dictStates.Add( "Himachal Pradesh",1);
        dictStates.Add("Jammu & Kashmir",1);
        dictStates.Add("Jharkhand", 2);
        dictStates.Add("Karnataka", 3);
        dictStates.Add("Kerala", 3);
        dictStates.Add("Madhya Pradesh", 1);
        dictStates.Add("Maharashtra", 4);
        dictStates.Add( "Manipur", 2);
        dictStates.Add("Mizoram", 2);
        dictStates.Add("Meghalaya", 2);
        dictStates.Add("Nagaland", 2);
        dictStates.Add( "Odisha", 2);
        dictStates.Add( "Punjab", 1);
        dictStates.Add( "Rajasthan",1);
        dictStates.Add("Sikkim", 2);
        dictStates.Add("Tamil Nadu", 3);
        dictStates.Add("Tripura", 2);
        dictStates.Add("Uttar Pradesh", 1);
        dictStates.Add( "Uttarakhand",1);
        dictStates.Add( "West Bengal", 2);
        dictStates.Add("A&N Islands", 3);
        dictStates.Add("Chandigarh", 1);
        dictStates.Add("D&N Haveli", 4);
        dictStates.Add("Daman & Diu", 4);
        dictStates.Add("Delhi UT", 1);
        dictStates.Add("Lakshadweep", 3);
        dictStates.Add("Puducherry", 3);
    }


	public void OnClickX(){
		xRotate ();
		X.text = colNames[selectedX];
        changeX.text = colNames[selectedX];
    }
	public void OnClickY(){
		yRotate ();
		Y.text = colNames[selectedY];
        changeY.text = colNames[selectedY];
    }
	public void OnClickZ(){
		zRotate ();
		Z.text = colNames[selectedZ];
        changeZ.text = colNames[selectedZ];
    }
}
