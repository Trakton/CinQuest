﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Feedback : MonoBehaviour {

	public InputField emailFeed;
	public InputField bodyFeed;
	private MenuStatus menuStatus;

	void Start () {
		menuStatus = GameManager.Instance.menuStatus;
	}

	public void closeFeedback() {
		menuStatus.close ("Feedback");
		PauseMenu.Instance.CloseFeedback();
	}

	public void sendEmail(){
		//email Id to send the mail to
		string email = emailFeed.text;
		//subject of the mail
		string subject = "Feedback";
		//body of the mail which consists of Device Model and its Operating System
		string body = bodyFeed.text;
		print (email + "/" + body);
		//Open the Default Mail App
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
		//print ("enviou???");
		closeFeedback();
	}
}
