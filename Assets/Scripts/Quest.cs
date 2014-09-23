﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Quest {
	
	public string questName;
	
	public bool questDone = false;
	
	public string questDescription;
	
	public int questID;
	
	public List<Task> questTasks = new List<Task>();
	
	public Quest(){}
	
	public Quest(string name, string description){
		this.questName = name;
		this.questDescription = description;
	}
	
	public Quest(string name, string description, List<Task> questTasks ){
		this.questName = name;
		this.questDescription = description;
		this.questTasks = questTasks;
	}
	
	public bool hasAllTasks(){
		bool ok = true;
		for(int i=0; i< this.questTasks.Count; i++){
			ok = ok && this.questTasks[i].taskDone ;
		}
		return ok;
	}

	public override bool Equals (object obj)
	{
		if (obj == null) return false;
		return this.questName.Equals ( ((Quest)obj).questName  );
	}
	
	public int getTaskID(){
		return this.questID;
	}
	
	
}