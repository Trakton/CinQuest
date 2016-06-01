﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour 
{
	/*
		Developed by: Higor

		Description: The GameManager is a prefab responsible for storing information about
		all the quests and overall game progress. To do it, we call DontDestroyOnLoad() for
		keeping it alive across the scenes. However, there must never be more than one instance
		of the GameManager on the game, that's why we make it be a singleton. A singleton is an object
		that is instantiated once the game starts, and then never again.

        The GameManager is also responsible for activating and disabling some children objects, such as the Minimap,
        the PauseMenuUI, the QuestManagerUI and the InventoryUI. For that, you have a list of string called "diableChildrenInScene".
        You must populate it with the name of every scene that those object mustn't be present, such as the initial menu, and the game
        manager will disable them in these scenes. Of course, you must also populate the "childrenObjets" list for the game manager
        to know wich object he must keep track and diable/enable between scenes.

		How to use it: Attach it to the GameManger prefab. But you can't just drag the GameManager prefab on the scenes, because of it's
		singleton behavior. You have to instantiate him on the first scenes, but using a script, the so called Loader.
		*See the Loader script for more information
		
	 */

	public static GameManager instance = null;
    private static Vector2 screenSize = new Vector2(1024.0f, 768.0f);
	public QuestManager questManager { get; set; } 
	public GameConfiguration gameConfiguration { get; set; } 
	public PreConditionManager preConditionManager { get; set; }
	// TODO: LOAD FROM DATA THESE PROPERTIES BELLOW.
	public static List<Item> items = new List<Item> ();

    public List<string> disableChildrenInScene;
    public List<GameObject> childrenObjects;

	void Awake () 
	{
		if (instance == null) {
			this.loadAndStartGame ();
		}
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
        ActivateChildren();
    }

    void OnLevelWasLoaded(int level)
    {
        ActivateChildren();
    }

    /// <summary>
    /// Enable/Disable the children objects such as InventoryUI and QuestUI between scenes.
    /// </summary>
    void ActivateChildren()
    {
        bool activate = true;
        string currentScene = SceneManager.GetActiveScene().name;

        foreach(string scene in disableChildrenInScene)
            if (currentScene == scene)
                   activate = false;

        if (activate)
            foreach (GameObject child in childrenObjects)
                child.SetActive(true);

        else if (!activate)
            foreach (GameObject child in childrenObjects)
                child.SetActive(false);
    }

	void Update () 
	{
		
	}

	/// <summary>
	/// Developed by: Peao (rngs);
	/// Loads and start the game.
	/// </summary>
	private void loadAndStartGame() {
		instance = this;
		this.loadAppConfiguration();
		this.startManagers ();
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

		// TODO: LOAD GAME ITEMS
		items.Add(new Item("ItemTeste",1,"Item de teste",Item.ItemType.Weapon));

		// TODO: LOAD GAME PRECONDITIONS
		this.preConditionManager = new PreConditionManager(RepositoriesFactory.createPreConditionRepository(this.gameConfiguration.databaseType));
		this.preConditionManager.loadPreConditionsFromFile (this.gameConfiguration.preConditionCollectionPath);
		// DEBUG: REMOVE LATER
		print ("# DEBUG: There were load: "+this.preConditionManager.getPreConditions ().Count+" PreConditions");

		// QUEST MANAGER
		this.questManager = new QuestManager (RepositoriesFactory.createQuestRepository(this.gameConfiguration.databaseType));
		this.questManager.loadQuestsFromFile (this.gameConfiguration.questCollectionPath);
		// DEBUG: REMOVE LATER
		print ("# DEBUG: There were load: "+this.questManager.getQuests ().Count+" Quests.");

		// TODO: LOAD USER STATE - HOW TO STORE USER INFORMATION OUTSIDE THE PROJECT? OR COULD IT BE INSIDE?
	}


}
