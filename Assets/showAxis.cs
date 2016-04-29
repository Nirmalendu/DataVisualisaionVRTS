using UnityEngine;
using System.Collections;

public class showAxis : MonoBehaviour {

	GameObject txtMeshTransform;
	public GameObject prefab;
	Vector3 positionLabel;
	//public GameObject cmr;
	public void MyPointerEnter () 
	{
		
		positionLabel = new Vector3 (transform.position.x + 100, transform.position.y + 100, transform.position.z + 100);
		txtMeshTransform = (GameObject)Instantiate(prefab,positionLabel,Quaternion.identity);
		//label = txtMeshTransform.GetComponent<TextMesh>();
		txtMeshTransform.GetComponent<TextMesh>().text = transform.name.ToString();
		Transform cameraloc = GameObject.FindGameObjectWithTag("MainCamera").transform;
		txtMeshTransform.transform.LookAt (cameraloc);
		txtMeshTransform.transform.Rotate (new Vector3 (0, 180, 0));
		//txtMeshTransform.gameObject.tag = "New";
	}

	public void MyPointerLeave ()
	{
		Destroy (txtMeshTransform);
	}
}
