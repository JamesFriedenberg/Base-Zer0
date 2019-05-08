using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scope : MonoBehaviour
{
    //The possible scopes that can be attached
	public enum Scope {
		None = 0,
		HoloSight,
		KobraSight,
		CCOSight,
		ACOG,
		PKA,
		SniperScope,
		PSO
	};
    //The active scope
    public Scope myScope = Scope.None;
	private string[] scopeImages = {"", "scope_redDot", "scope_kobra","scope_cco","scope_acog", "scope_acog", "scope_default","scope_default"};

    //Scope Number
    public int activeScope;
    //the field of view of the camera when you ADS
    public int fov = 30;

    //the sensitivity of the player while ADS
    public float sensativity;

    //changes the position of the gun in case of tall site
    public Vector3 newTransform = Vector3.zero;

    //get the active scope
    public string GetScopeImage(){
        return scopeImages[(int)myScope];
    }
}
