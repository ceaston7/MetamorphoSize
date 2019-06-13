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
        public bool jumping = false;

	    // Start is called before the first frame update
	    void Start()
	    {
            currentTool = Tool.SizeGun;
            if(spawn == null)
            {
                spawn = GameObject.Find("Spawn").transform;
            }
            transform.position = spawn.position;
	    }       
    }

    public enum Tool{
		    SizeGun = 0,
		    SizeSelf = 1
    }
}