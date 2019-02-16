using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSetActive : IActivate
{
		override public void ActivateMe()
		{
				transform.gameObject.SetActive(true);
		}

		public override void DeactivateMe()
		{
				transform.gameObject.SetActive(false);
		}
}
