using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;


public class weapon : MonoBehaviour
{

    public float damage = 10f;
    public float range = 200f;
    public float fireRate = 5f;
    public float impactForce = 200f;
    public int magSize = 10;
    public int currentAmmoCount = 30;
    public float reloadTime = 1.0f;
    public int adsZoom = 30;
    public float adsAccuracy = 1000.0f;
    public float accuracy = 100.0f;
    public float recoil = 1.5f;
    public int bulletCount = 1;
    public bool semiAuto = false;
    public bool projectile = false;
    public string fireAnimation = "firing";

    private float currentAccuracy = 1.0f;
    private bool hasFired = false;
    private bool willReset1 = false;
    private bool willReset2 = false;
    private bool hasReloaded = true;

    public string currentAmmoType = "AR";
    public string scopeName = "scope_defualt";

    public Camera fpsCam;
    public GameObject fpsController;
    public GameObject gameManager;
    public GameObject projObj;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject blood;
    public GameObject bulletHole;
    private GameObject scopeImage;
    public GameObject weaponCamera;

    public AudioSource fireSound;
    public Animator animator;
    public Animator secondMotionAnimator;
	public Animator weaponAnimator;

    private float fireTimer = 100f;
    private float reloadTimer = 100f;
    private float walkTimer = 100f;
    private float willFireTimer = 100f;

    private bool willFire = false;

    private Vector3 localPos;
    private float swayAmount = .08f;
    private float maxSwayAmount = .1f;
    private float smoothSwayAmount = 2f;

    private Quaternion localRot = new Quaternion();
    private GameObject[] crossHairs;

    private int myIterations = 0;

    //
    private bool[] myUpgrades = {false, false, false, false};

    //TODO:
    /*
    * Add in different sensativity when ADS
    * Add in modularity of ADS accuracy on different components
    * Different gun sounds based on modularity
    * Different Impact force based on modularity
    * Different bulletHole image based on modularity
    * Varying muzzle flash animations
    * Improved animations
    * Weapon Sway
    */
    void Start()
    {
        localPos = transform.localPosition;
        gameManager = GameObject.FindGameObjectWithTag("gm");

        ShopSystemHandler shopHandle = gameManager.GetComponent<ShopSystemHandler>();
        for(int i = 0; i < shopHandle.weaponsList.Length; i++){
            if(shopHandle.weaponsList[i].objName == this.gameObject.name){
                myUpgrades[0] = shopHandle.weaponsList[i].stockUpgraded;
                myUpgrades[3] = shopHandle.weaponsList[i].barrelUpgraded;
                myUpgrades[2] = shopHandle.weaponsList[i].scopeUpgraded;
                myUpgrades[1] = shopHandle.weaponsList[i].magazineUpgraded;
            }
        }

        if(projectile) semiAuto = true;
        FindStats(this.gameObject);

        crossHairs = GameObject.FindGameObjectsWithTag("crossHair");

        if(currentAmmoCount > magSize) currentAmmoCount = magSize;
/*
        ShopWeapon myWeapon;
        for(int i = 0; i < gameManager.weapons.length; i++){
            if(gameManager.weapons[i].name == this.gameObject.name){
                myWeapon = gameManager.weapons[i];
                break;
            }
        }
        myUpgrades = myWeapon.upgrades;
*/
        GameObject[] scopeImages = GameObject.FindGameObjectsWithTag("scopeImage");
        for(int i = 0; i < scopeImages.Length; i++){
            if(scopeImages[i].name == scopeName){
                scopeImage = scopeImages[i];
            }
        }
        currentAccuracy = accuracy;
        GameManager gm = gameManager.GetComponent<GameManager>();
        if(gm.ammoInWeapons != null && gm.ammoInWeapons.Length > 0){
            currentAmmoCount = gm.ammoInWeapons[gm.playerWeapons[gm.currentWeapon]];
        }
    }
    void FindStats(GameObject objToSearch){
        if(myIterations > 30) return;

        myIterations++;
        Transform[] children = this.GetComponentsInChildren<Transform>();

        for(int i = 0; i < children.Length; i++){
            if(children[i].GetComponent<stock>() != null){
                if(children[i].GetComponent<stock>().upgrade && myUpgrades[0]){
                    accuracy += children[i].GetComponent<stock>().accuracy;
                    recoil *= children[i].GetComponent<stock>().recoil;
                }else if(children[i].GetComponent<stock>().upgrade){
                    children[i].gameObject.SetActive(false);
                }

                if(!children[i].GetComponent<stock>().upgrade && !myUpgrades[0]){
                }else if(!children[i].GetComponent<stock>().upgrade){
                    children[i].gameObject.SetActive(false);
                }
            }else if(children[i].GetComponent<magazine>() != null){
                if(children[i].GetComponent<magazine>().upgrade && myUpgrades[1]){
                    magSize += children[i].GetComponent<magazine>().magSize;
                    reloadTime *= children[i].GetComponent<magazine>().reloadTime;
                }else if(children[i].GetComponent<magazine>().upgrade){
                    children[i].gameObject.SetActive(false);
                }

                if(!children[i].GetComponent<magazine>().upgrade && !myUpgrades[1]){
                }else if(!children[i].GetComponent<magazine>().upgrade){
                    children[i].gameObject.SetActive(false);
                }
            }else if(children[i].GetComponent<scope>() != null){
                if(children[i].GetComponent<scope>().upgrade && myUpgrades[2]){
                    scopeName = children[i].GetComponent<scope>().scopeImage;
                    adsZoom = children[i].GetComponent<scope>().fov;
                    this.transform.localPosition += children[i].GetComponent<scope>().newTransform;
                }else if(children[i].GetComponent<scope>().upgrade){
                    children[i].gameObject.SetActive(false);
                }

                if(!children[i].GetComponent<scope>().upgrade && !myUpgrades[2]){
                }else if(!children[i].GetComponent<scope>().upgrade){
                    children[i].gameObject.SetActive(false);
                }
            }else if(children[i].GetComponent<barrel>() != null){
                if(children[i].GetComponent<barrel>().upgrade && myUpgrades[3]){
                    accuracy += children[i].GetComponent<barrel>().accuracy;
                }else if(children[i].GetComponent<barrel>().upgrade){
                    children[i].gameObject.SetActive(false);
                }

                if(!children[i].GetComponent<barrel>().upgrade && !myUpgrades[3]){
                }else if(!children[i].GetComponent<barrel>().upgrade){
                    children[i].gameObject.SetActive(false);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //float movementX = Input.GetAxis("Mouse X") * -swayAmount;
        //float movementY = Input.GetAxis("Mouse Y") * -swayAmount;
        //movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        //movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);
        //Vector3 finalPosition = Vector3.zero;
        //if(Input.GetButton("Fire2")){
        //    finalPosition = new Vector3(movementX, 0, 0);
        //}else{
        //    finalPosition = new Vector3(movementX, movementY, 0);
        //}
        //transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + localPos, Time.deltaTime * smoothSwayAmount);

        Scene curScene = SceneManager.GetActiveScene();
        string sceneName = curScene.name;

        if(sceneName == "HQ")
        {

            if (Input.GetKeyDown(KeyCode.O))
            {
                //SceneManager.LoadScene("Shoptest");
            }
        }
        fireTimer += Time.deltaTime;
        reloadTimer += Time.deltaTime;
        walkTimer += Time.deltaTime;
        willFireTimer += Time.deltaTime;

        if(walkTimer < .5f){
            fpsController.GetComponent<FirstPersonController>().isFiring = true;
        }else{
            fpsController.GetComponent<FirstPersonController>().isFiring = false;
        }
        if (fireTimer >= 1 / (fireRate * 2))
        {
            secondMotionAnimator.SetBool(fireAnimation, false);
			weaponAnimator.SetBool("Fire", false);

        }
        if (reloadTimer < reloadTime)
        {
            hasReloaded = false;
            float myFOV = fpsCam.GetComponent<Camera>().fieldOfView;
            animator.SetBool("ads", false);
            fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(myFOV, 60, .2f);
            fpsController.GetComponent<FirstPersonController>().isReloading = true;
            return;
        }
        else if(!Input.GetButton("Fire2")){
            hasReloaded = true;
        }
        fpsController.GetComponent<FirstPersonController>().isReloading = false;
        //animator.SetBool("reloading", false);
		weaponAnimator.SetBool("Reload", false);
		//weaponAnimator.applyRootMotion = true;
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
            return;
        }
        if(fireTimer >= 1 / fireRate && reloadTimer > reloadTime + 0.3f){
            if(Input.GetButton("Fire1") && !willFire && fpsController.GetComponent<FirstPersonController>().IsRunning()){
                willFire = true;
                walkTimer = 0;
                StartCoroutine(WillShoot());
            }else if(Input.GetButton("Fire1") && !willFire){
                if(semiAuto && hasFired) return;
                Shoot();
                fireTimer = 0;
            }
        }
        if( fireTimer >= 1 / fireRate){
            willReset1 = true;
        }
        if(Input.GetButtonUp("Fire1")){
            willReset2 = true;
        }
        if(willReset1 && willReset2) hasFired = false;
        if(hasReloaded) ADS();
    }
    IEnumerator WillShoot()
    {

        yield return new WaitForSeconds(.2f);
        willFire = false;
        if(!(semiAuto && hasFired)){ 
            Shoot();
            fireTimer = 0;
        }
        
    }
    void ADS()
    {
        float myFOV = fpsCam.GetComponent<Camera>().fieldOfView;
        if (Input.GetButton("Fire2"))
        {
            currentAccuracy = adsAccuracy;
            if(scopeImage){
                for(int i = 0; i < crossHairs.Length; i++){
                    crossHairs[i].GetComponent<Image>().enabled = false;
                }
                scopeImage.GetComponent<Image>().enabled = true;
                weaponCamera.SetActive(false);
            }
            animator.SetBool("ads", true);
            fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(myFOV, adsZoom, .2f);
        }
        else
        {
            currentAccuracy = accuracy;
            if(scopeImage){                
                for(int i = 0; i < crossHairs.Length; i++){
                    crossHairs[i].GetComponent<Image>().enabled = true;
                }
                scopeImage.GetComponent<Image>().enabled = false;
                weaponCamera.SetActive(true);
            }
            animator.SetBool("ads", false);
            fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(myFOV, 60, .2f);
        }
    }
    void Shoot()
    {
        if (currentAmmoCount <= 0) return;
        hasFired = true;
        willReset1 = false;
        willReset2 = false;
        secondMotionAnimator.SetBool(fireAnimation, true);
		weaponAnimator.SetBool("Fire", true);
        muzzleFlash.Play();
        fpsController.GetComponent<FirstPersonController>().m_MouseLook.m_CameraTargetRot *= Quaternion.Euler (-recoil, 0f, 0f);
        //fpsCam.transform.Rotate(fpsCam.transform.right, 5.0f);
        fireSound.GetComponent<AudioSource>().Play(0);
        currentAmmoCount--;
        GameManager gm = gameManager.GetComponent<GameManager>();

        //update current ammo count in specific weapon in gamemanager so it will remain
        //consistent through scenes
        gm.ammoInWeapons[gm.playerWeapons[gm.currentWeapon]] = currentAmmoCount;
        if(projectile){
            DoProjectile();
            return;
        }

        RaycastHit hit;

        for(int i = 0; i < bulletCount; i++){
            Vector3 shootDirection = fpsCam.transform.forward;
            shootDirection.x += Random.Range(-1000,1000) / (currentAccuracy * 1000.0f);
            shootDirection.y += Random.Range(-1000,1000) / (currentAccuracy * 1000.0f);
            shootDirection.z += Random.Range(-1000,1000) / (currentAccuracy * 1000.0f);

            shootDirection.Normalize();

            if (Physics.Raycast(fpsCam.transform.position, shootDirection, out hit, range))
            {
                DoBullet(hit);
            }
        }
    }
    void DoProjectile(){
        if(!projObj || projObj == null) return;
        Quaternion projectileDirection = Quaternion.identity;
        projectileDirection.SetLookRotation(fpsCam.transform.forward);
        Instantiate(projObj,this.transform.position + fpsCam.transform.forward * 2, projectileDirection);
    }
    void DoBullet(RaycastHit hit){
            Target target = hit.transform.GetComponent<Target>();
            GameObject myImpact = impactEffect;
            if (target != null)
            {
                target.TakeDamage(damage);
                myImpact = blood;
            }
            else
            {
                GameObject bh = Instantiate(bulletHole, hit.point + (hit.normal * .01f), Quaternion.LookRotation(hit.normal));
                Destroy(bh, 10);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impact = Instantiate(myImpact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1);
    }
    void Reload()
    {
        if (currentAmmoCount == magSize) return;
        int ammoCount = gameManager.GetComponent<GameManager>().CheckAmmo(currentAmmoType);
        if (ammoCount == 0) return;

        if(scopeImage){
            scopeImage.GetComponent<Image>().enabled = false;
            weaponCamera.SetActive(true);
        }

		weaponAnimator.SetBool("Reload", true);
		//weaponAnimator.applyRootMotion = false;

        int ammoChange = Mathf.Min((magSize - currentAmmoCount), ammoCount);
        currentAmmoCount += ammoChange;
        gameManager.GetComponent<GameManager>().AddAmmo(currentAmmoType, -ammoChange);

        reloadTimer = 0;
    }

}
