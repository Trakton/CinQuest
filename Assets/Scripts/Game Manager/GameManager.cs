﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	/*
		Developed by: Higor

		Description: The GameManager is a prefab responsible for storing information about
		all the quests and overall game progress. To do it, we call DontDestroyOnLoad() for
		keeping it alive across the scenes. However, there must never be more than one instance
		of the GameManager on the game, that's why we make it be a singleton. A singleton is an object
		that is instantiated once the game starts, and then never again.

		How to use it: Attach it to the GameManger prefab. But you can't just drag the GameManager prefab on the scenes, because of it's
		singleton behavior. You have to instantiate him on the first scenes, but using a script, the so called Loader.
		*See the Loader script for more information
		
	 */

	public static GameManager instance = null;
    private static Vector2 screenSize = new Vector2(1024.0f, 768.0f);
	private QuestManager questManager; 
	private GameConfiguration gameConfiguration;
	// TODO: LOAD FROM DATA THESE PROPERTIES BELLOW.
	public static List<IPreCondition> preConditions = new List<IPreCondition>();
	public static List<Item> items = new List<Item> ();

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Update () 
	{
	
	}

    /// <summary>
    /// Developed by: Lucas (lss5);
    /// Adjusts width and height of a GUI for it stays in proportion with the user's screen dimensions, using as base the dimensions of the author's screen (Vector2 screenSize).
    /// It's a VERY important method and MUST be used in every GUI component created in this project.
    /// </summary>
    public static void AutoResize()
    {
        Vector2 resizeVector = new Vector2((float)Screen.width / screenSize.x, (float)Screen.height / screenSize.y);

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeVector.x, resizeVector.y, 1.0f));
    }

	/// <summary>
	/// Developed by: Peao (rngs);
	/// Create the Configuration Class of Game. Should be called at the begining of the game.
	/// </summary>
	private void loadAppConfiguration(){
		this.gameConfiguration = new GameConfiguration ();
	}

	/// <summary>
	/// Developed by: Peao (rngs);
	/// Method where we should initiate all the systems managers of the Game.
	/// </summary>
	private void startManagers(){
		
		// QUEST MANAGER
		this.questManager = new QuestManager (this.createQuestRepository(this.gameConfiguration.databaseType));
		this.questManager.loadQuestsFromRepository (this.gameConfiguration.questCollectionPath);
	}

	/// <summary>
	/// Developed by: Peao (rngs);
	/// Method to instantiate the QuestRepository based on DatabaseStorageType.
	/// </summary>
	private IQuestRepository createQuestRepository(EDatabaseStorageType type){
		switch (type) {
		case EDatabaseStorageType.XML:
			return new RepositoryXMLFactory().createQuestRepository();
		default:
			return null;
		}
	}
}
