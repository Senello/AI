using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
	void Start ()
    {
		DestroyObject(gameObject,gameObject.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
	}

}
