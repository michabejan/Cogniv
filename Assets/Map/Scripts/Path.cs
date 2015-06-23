using UnityEngine;
using System.Collections;
using System;

public class Path : MonoBehaviour {

	

	public int width;
	public int height;
    public int factor;

	public byte[,] path;

    public Path(int height, int width, int factor)
    {
        this.height = height;
        this.width = width;
        this.factor = factor;
    }

    public void initPath()
    {
        path = new byte[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                path[width, height] = 0;
            }
        }
        cosinusFloors();
    }

	public void cosinusFloors() {
		int x = width / 2;
		int y = height -1;

		int int_fields_y = height;
		double value_per_field_y = factor * Math.Cos(0) / int_fields_y;
		double value_per_field_x = value_per_field_y * Math.PI;

	
		double temp = 0;
		double temp2 = 0;
		
		x++;
		while (!isOutOfBounds(x, y)) {
			
			temp += value_per_field_x;
			temp2 = factor * Math.Cos(temp);
			double lala = Math.Ceiling(temp2 * height/2);
			double lala2 = height/2;
			y =  (int) (lala + lala2);
			if(y == height)
			y--;
			if(y== -1)
			y++;

			if(!isOutOfBounds(x, y))
			path[y,x] = 0;
			if(!isOutOfBounds(x-1, y))
			path[y,x-1] = 0;
			if(!isOutOfBounds(x-2, y))
			path[y,x-2] = 0;
			if(!isOutOfBounds(x+1, y))
			path[y,x+1] = 0;
			if(!isOutOfBounds(x+2, y))
			path[y,x+2] = 0;
			
			x++;
		}
		temp = 0;
		x = width / 2;
		y = height -1;
		while (!isOutOfBounds(x, y)) {
			
			temp -= value_per_field_x;
			temp2 = factor * Math.Cos(temp);
			double lala = Math.Ceiling(temp2 * height/2);
			double lala2 = height/2;
			y =  (int) (lala + lala2);
			if(y == height)
			y--;
			if(y== -1)
			y++;

			if(!isOutOfBounds(x, y))
			path[y,x] = 0;
			if(!isOutOfBounds(x-1, y))
			path[y,x-1]= 0;
			if(!isOutOfBounds(x-2, y))
			path[y,x-2]= 0;
			if(!isOutOfBounds(x+1, y))
			path[y,x+1]= 0;
			if(!isOutOfBounds(x+2, y))
			path[y,x+2]= 0;
			x--;
		}
	}

	public bool isOutOfBounds(int x, int y) {
		return (x <= 0 || x >= width || y < 0 || y >= height);
	}
}


