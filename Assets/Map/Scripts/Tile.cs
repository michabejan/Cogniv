using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public int x;
    public int z;
    public bool isWall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool getWall()
    {
        return isWall;
    }

    public void setWall(bool p)
    {
        this.isWall = p;
    }
}
