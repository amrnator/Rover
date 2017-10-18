using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script attached to anomalies to research
/// </summary>
public class Anomaly : MonoBehaviour {

    public int codexID;

    //when rover is in range
    void OnTriggerEnter(Collider other)
    {
        //prompt player to being research
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().promptResearch(codexID);
    }

	void OnTriggerExit(Collider other)
	{
		//prompt player to end research
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().exitResearch();
	}

}
