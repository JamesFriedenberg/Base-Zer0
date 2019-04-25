using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    //The possible recievers that can be attached
	public enum Reciever {
		Default = 0,
		FasterFire,
		HigherDamage
	};
    //The active reciever
    public Reciever myReciever = Reciever.Default;

    //how much does the damage change? Number is added to the weapons current damage. 
    public int damage = 0;

    //how much does the accuracy change? Number is added to the weapons current accuracy. Bigger numbers are more accuracte
    public int accuracy = 0;
    
    //multiplied by the current recoil... on the weapon script "recoil" is the number of angles of recoil
    public float recoil = 1;

    //multiplied by the current fireRate... on the weapon script "fireRate" is the number of rounds fired per second
    public float fireRate = 1;
}
