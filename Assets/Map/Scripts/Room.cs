using UnityEngine;
using System.Collections;

public class Room {

	public int width;
    public int height;

    public Room(int width, int height){
        this.width = Random.Range(0, width);
        this.height = Random.Range(0, height);

    }


}
