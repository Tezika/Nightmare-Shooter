using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

	public Transform targetTransfom;

	[SerializeField]
	private float _smoothing = 5.0f;
	private Vector3 _offest;
	// Use this for initialization
	void Start () {
		this._offest = this.transform.position - targetTransfom.position; 
	}
	//early update
	void FixedUpdate(){
		Vector3 purposePos = targetTransfom.position + _offest;
		this.transform.position = Vector3.Lerp (this.transform.position, purposePos, Time.fixedDeltaTime * this._smoothing);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
