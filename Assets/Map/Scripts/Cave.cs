using UnityEngine;
using System.Collections;

public class Cave {

    public int width, height;

    public int percentageAreWalls;

    public byte[,] tiles;

    // 1 = isWall 
    // 0 = noWall

    public Cave(int width, int height, int percentageAreWalls)
    {
        this.percentageAreWalls = percentageAreWalls;
        this.width = width;
        this.height = height;
        tiles = new byte[width,height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++ )
            {
                tiles[i, j] = 0;
            }
        }
        randomFillCave();
        generateCaverns();

    }

    public void randomFillCave()
    {


        int mapMiddle = 0;
        for (int column = 0, row = 0; row < height; row++)
        {
            for (column = 0; column < width; column++)
            {


                if (column == 0)
                {
                    // =1
                    tiles[column, row] = 1;
                }
                else if (row == 0)
                {
                    tiles[column, row] = 1;
                }
                else if (column == width - 1)
                {
                    tiles[column, row] = 1;
                }
                else if (row == height- 1)
                {
                    tiles[column, row] = 1;
                }
                else
                {
                    mapMiddle = (height / 2);

                    if (row == mapMiddle)
                    {
                        tiles[column, row] = 0;
                    }
                    else
                    {
                        int result = 0;

                        result = Random.Range(0, 101);
                        if (percentageAreWalls >= result)
                        {
                            tiles[column, row] = 1;
                        }
                        else
                        {
                            tiles[column, row] = 0;
                        }

                    }
                }
            }
        }

    }

    public void generateCaverns()
    {

        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                int numWalls = getAdjacentWalls(column, row);
                if (tiles[column, row] == 1)
                {
                    if (numWalls >= 4)
                    {
                        tiles[column, row] = 1;
                    }
                    else
                    {
                        tiles[column, row] = 0;
                    }

                }
                else
                {
                    if (numWalls >= 5)
                    {
                        tiles[column, row] = 1;
                    }
                    else
                    {
                        tiles[column, row] = 0;
                    }
                }


            }
        }
    }
 

    public int getAdjacentWalls(int width, int height)
    {
        int result = 0;
        if (!isOutofBounds(width, height + 1))
        {
            if (IsWall(width, height + 1))
                result++;
        }
        else
        {
            result++;
        }

        if (!isOutofBounds(width, height - 1))
        {
            if (IsWall(width, height - 1))
            {
                result++;
            }
        }
        else
        {
            result++;
        }

        if (!isOutofBounds(width + 1, height))
        {
            if (IsWall(width + 1, height))
            {
                result++;
            }
        }
        else
        {
            result++;
        }


        if (!isOutofBounds(width + 1, height + 1))
        {
            if (IsWall(width + 1, height + 1))
            {
                result++;
            }
        }
        else
        {
            result++;
        }



        if (!isOutofBounds(width + 1, height - 1))
        {
            if (IsWall(width + 1, height - 1))
            {
                result++;
            }
        }
        else
        {
            result++;
        }


        if (!isOutofBounds(width - 1, height + 1))
        {
            if (IsWall(width - 1, height + 1))
            {
                result++;
            }
        }
        else
        {
            result++;
        }


        if (!isOutofBounds(width - 1, height))
        {
            if (IsWall(width - 1, height))
            {
                result++;
            }
        }
        else
        {
            result++;
        }


        if (!isOutofBounds(width - 1, height - 1))
        {
            if (IsWall(width - 1, height - 1))
            {
                result++;
            }
        }
        else
        {
            result++;
        }

        return result;
    }

    public bool isOutofBounds(int width, int height)
    {
        if (width >= this.width || width < 0 || height < 0 || height >= this.height)
        {
            return true;
        }
        return false;
    }

    public bool IsWall(int width, int height)
    {
        if (tiles[width, height] == 1)
        {
            return true;
        }
        return false;
    }
}
