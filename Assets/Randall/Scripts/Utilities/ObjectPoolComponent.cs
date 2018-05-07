using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gold;
using Gold.Delegates;

public class ObjectPoolComponent : MonoBehaviour {
	private ObjectPool objectPool;
	private bool isSetup;
	private ValueChange<GameObject> Add;

	//Make sure to set the gameObject to inactive before calling this
	public bool Setup(ObjectPool pool, ValueChange<GameObject> del)
	{
		if(!isSetup)
		{
			Add = del;
			objectPool = pool;
			isSetup = true;
			return true;
		}
		return false;
	}

	private void OnDisable()
	{
		//Pass in object to a delagate
		Add(gameObject);
	}

}
