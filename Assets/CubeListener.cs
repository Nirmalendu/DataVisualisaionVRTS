using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CubeListener : MonoBehaviour 
{
	public Text labelx;
	public Text labely;
	public Text labelz;
	Vector3 unit = new Vector3 (50,50,-50);
	//Vector3 unitneg = new Vector3 (-5,-5,-5);
	Vector3 positionLabel;
	GameObject txtMeshTransform;
	public GameObject prefab;
	//public GameObject cmr;
	Color c;
	//Transform endMarker;
	public float speed = 50.0F;
	float startTime;
	float journeyLength;
	float overTime = 1.0F;
	Transform startMarker ;
    Dictionary<int, string> dictPurpose;
    public GameObject controller;
    void Start()
    {
        dictPurpose = new Dictionary<int, string>();
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

    public void OnPointerEnter () 
	{
        int[] a= dataPlot.getCor();
        //Debug.Log(a[0]);
        //Debug.Log(dataPlot.mean[0]);
        c = GetComponent<Renderer> ().material.color;
		GetComponent<Renderer>().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
		labelx.text =  ((((transform.position.x - 200) * float.Parse(dataPlot.std[3 + a[0]])/500)+ float.Parse(dataPlot.mean[3 +a[0]]))).ToString();
		labely.text = ((((transform.position.y - 200) * float.Parse(dataPlot.std[3 + a[1]]) / 500) + float.Parse(dataPlot.mean[3 + a[1]]))).ToString();
        labelz.text = ((((transform.position.z - 200) * float.Parse(dataPlot.std[3 + a[2]]) / 500) + float.Parse(dataPlot.mean[3 + a[2]]))).ToString();
        //labelColour.text =  (transform.position.z*DataReading.maxZ/1000.0f).ToString();
        positionLabel = new Vector3 (transform.position.x + 1, transform.position.y + 1, transform.position.z + 1);
		txtMeshTransform = (GameObject)Instantiate(prefab,positionLabel,Quaternion.identity);
        //label = txtMeshTransform.GetComponent<TextMesh>();
        string tag = gameObject.tag;
        //a =  transform.position;
        string [] stName = transform.name.Split(',');
        string typeInt = tag.Remove(0, 7) ;
        txtMeshTransform.GetComponent<TextMesh>().text = dictPurpose[int.Parse(typeInt)] + "\n" + stName[1];
		Transform cameraloc = GameObject.FindGameObjectWithTag("MainCamera").transform;
		txtMeshTransform.transform.LookAt (cameraloc);
		txtMeshTransform.transform.Rotate (new Vector3 (0, 180, 0));
		//WarpDrive az = new WarpDrive ();
		//az.wd ();
		//txtMeshTransform.gameObject.tag = "New";

		}

	public void OnMouseDown()
	{
        controller = GameObject.FindGameObjectWithTag("fps");
        startMarker = controller.transform;
		//endMarker = this.transform;
		startTime = Time.time;
		if (Vector3.Distance(controller.transform.position,  transform.position - unit) < Vector3.Distance(controller.transform.position,  transform.position + unit)) {
			journeyLength = Vector3.Distance (startMarker.position, Vector3.Min (transform.position + unit, transform.position - unit));
			StartCoroutine (go (startMarker.position, Vector3.Min(transform.position + unit, transform.position - unit), overTime ));

		} else {
			journeyLength = Vector3.Distance (startMarker.position, Vector3.Max (transform.position + unit, transform.position - unit));
			StartCoroutine (go (startMarker.position, Vector3.Max(transform.position + unit, transform.position - unit), overTime ));


		}
		//Debug.Log ("hiiii");


	}
	IEnumerator go(Vector3 source, Vector3 target, float duration)  {
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			controller.transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		controller.transform.position = target;
	}
	public void OnPointerLeave ()
	{
		GetComponent<Renderer>().material.color = c;
		Destroy (txtMeshTransform);
	}
}


