using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {
	private Vector3 localPos;
	private float swayAmount = .08f;
	private float maxSwayAmount = .1f;
	private float smoothSwayAmount = 4f;

	// Use this for initialization
	void Start () {
		localPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float movementX = Input.GetAxis("Mouse X") * -swayAmount;
		float movementY = Input.GetAxis("Mouse Y") * -swayAmount;
		movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
		movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);
		Vector3 finalPosition = Vector3.zero;
		if(Input.GetButton("Fire2")){
			finalPosition = new Vector3(0, 0, 0);
		}else{
			finalPosition = new Vector3(movementX, movementY, 0);
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + localPos, Time.deltaTime * smoothSwayAmount);
	}
}
