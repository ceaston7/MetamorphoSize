using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateNavMesh : IActivate
{
	override public void ActivateMe()
	{
		transform.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
		Debug.Log("NavMeshBuilt");
	}

	public override void DeactivateMe()
	{
		transform.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
		Debug.Log("NavMeshBuilt");
	}
}
