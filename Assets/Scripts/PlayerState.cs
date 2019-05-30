using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame;

namespace OurGame
{
		public class PlayerState : MonoBehaviour
		{
				public bool[] haveTool = { false, false };
				public Tool currentTool;
				public Transform spawn;

				// Start is called before the first frame update
				void Start()
				{
						currentTool = Tool.SizeGun;
						transform.position = spawn.position;
				}
		}

		public enum Tool{
				SizeGun = 0,
				SizeSelf = 1
		}
}