using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	public ParticleSystem muzzleFlash;
    public ParticleSystem tracer;

    public void PlayFlash(){
        if(muzzleFlash == null){
            Debug.Log("No Tracer particle system assigned to player");
            return;
        }
        ParticleSystem flash = Instantiate(muzzleFlash, this.transform.position, Quaternion.identity);
        flash.transform.parent = this.gameObject.transform;
        Destroy(flash.gameObject,flash.gameObject.GetComponent<ParticleSystem>().main.duration * 2);
    }
    public void PlayTracer(Vector3 position, Quaternion direction){
        return;
        if(tracer == null){
            Debug.Log("No Tracer particle system assigned to player");
            return;
        }
        ParticleSystem trace = Instantiate(tracer, position, direction);
        //trace.transform.parent = this.gameObject.transform;
        Destroy(trace.gameObject,trace.gameObject.GetComponent<ParticleSystem>().main.duration);
    }
}
