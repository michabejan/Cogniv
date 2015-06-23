using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Map mapPrefab;
    private Map mapInstance;

	// Use this for initialization
	void Start () {
        BeginGame();
        mapInstance.Generate();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void BeginGame()
    {
        mapInstance = Instantiate(mapPrefab) as Map;
    }
}
