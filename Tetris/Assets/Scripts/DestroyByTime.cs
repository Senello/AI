using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{

    public float Lifetime;
	void Start ()
    {
		Destroy(gameObject,Lifetime);
	}

}
