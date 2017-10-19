using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

	public CodexDataBase codexDatabase;

	public Animator PromptAnimator;
	public Animator CodexAnimator;

	public TextMeshProUGUI codexName;
	public TextMeshProUGUI codexInfo;

	bool inResearch = false;
	bool codexOut = false;

	int currentCodex = 0;

	public void Update(){
		if (Input.GetButtonDown ("Fire1") && inResearch) {
			print("Beginning Research");

            showCodex();
        }

		if (Input.GetButtonDown ("Fire2") && codexOut) {
			CodexAnimator.SetBool ("isOpen", false);
		}

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

	public void promptResearch(int codexID){
		currentCodex = codexID;
		inResearch = true;
		PromptAnimator.SetBool ("isOpen", true);
		print ("research entered");
	}

	public void exitResearch(){
		inResearch = false;
		PromptAnimator.SetBool ("isOpen", false);
		print ("research exited");
	}

	IEnumerator ResearchAnimation(){
		//begin UI and rover animation before showing codex
		beginAnimation();

		yield return new WaitForSeconds (3);

		showCodex ();
	}


	void showCodex(){
		Codex codex = codexDatabase.dataBase [currentCodex];

		codexName.text = codex.name;
		codexInfo.text = codex.information;

		CodexAnimator.SetBool ("isOpen", true);

		codexOut = true;
	}

	void beginAnimation(){
		
	}
}
