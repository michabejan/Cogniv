using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int sizeX, sizeZ;
    public int components;
    public Tile tilePrefab;
    public Wall wallPrefab;
    public int percentageAreWalls;
    private Cave cave;
    private Room room;
    private List<Tile> activeTiles;
    private List<BoundingBox> boundingboxes;
    private List<Direction> directions;
    private Path path;
    private Floor floor;
    private FVector2 direction;
    private FVector2 lastPos;
    private List<Wall> walls;
    private BoundingBox box;
    private BoundingBox box1;
    private List<FVector2> tileLocations;
    private float numberX = 0;
    private float numberZ = 0;

    private List<int> numbers;

    private const int north = 0;
    private const int east = 1;
    private const int south = 2;
    private const int west= 3;
    
    public void Generate()
    {
        numbers = new List<int>();
        boundingboxes = new List<BoundingBox>();
        walls = new List<Wall>();
        activeTiles = new List<Tile>();
        int number = 0;
        bool fl = false;
        direction = new FVector2(1,1);
        lastPos = new FVector2(0, 0);
        tileLocations = new List<FVector2>();
        box = new BoundingBox();
        box1 = new BoundingBox();
  
        
        for (int i = 0; i < components*2; i++)
        {
            if (lastPos.x == -1000)
            {
                break;
            }
            if(fl){
               // number = Random.Range(0, 1);
                number = 0;
                if (number == 0)
                {
                  floor = new Floor(2,10);

                  CreateCells(2,10,fl);
                  checkBoundingBoxes(2, 10, fl);
                  //lastPos = generateNextCoordinate(fl, boundingboxes.Count - 1);
                
                } else{
                    path = new Path(10,2, 1);
                }
               
                fl = false;
            } else {
                //number = Random.Range(0, 1);
                number = 0;
                if (number == 0)
                {
                    cave = new Cave(sizeX, sizeZ, percentageAreWalls);
                    CreateCells(sizeX, sizeZ,fl);
                    checkBoundingBoxes(sizeX, sizeZ, fl);
                   // lastPos = generateNextCoordinate(fl, boundingboxes.Count - 1);
                 
                }
                else
                {
                    room = new Room(sizeX, sizeZ);
                }
                fl = true;
                cave = null;
            }
            
        }
        generateWalls();
        
        
    }
      

    private void checkBoundingBoxes(int x,int z, bool fl){
        bool temp = true;
        FVector2 last = new FVector2();
        List<BoundingBox> tempList = new List<BoundingBox>();
        tempList = copyBoundingBoxList();
                  for (int j = 0; j < tempList.Count; j++)
                  {
                      if (tempList.Count == 0)
                      {
                          lastPos.x = -1000;
                          break;
                      }
                      BoundingBox boxi = new BoundingBox();
                      for (int k = 0; k < 4; k++)
                      {
                          FVector2 num = lastPos;
                            last = generateNextCoordinate(fl, tempList.Count-1);
                            
                            if (direction.z == 1)
                            {
                                boxi.minX = last.x;
                                boxi.minZ = last.z;
                                boxi.maxZ = last.z + z;
                                boxi.maxX = last.x + x;
                                
                            }
                            //east
                            else if (direction.x == 1)
                            {
                                boxi.minX = last.x;
                                boxi.minZ = last.z;
                                boxi.maxZ = last.z + x;
                                boxi.maxX = last.x + z;
                               
                            }
                            //west
                            else if (direction.x == -1)
                            {
                                boxi.minX = last.x - z;
                                boxi.minZ = last.z;
                                boxi.maxZ = last.z + x;
                                boxi.maxX = last.x;
                                
                            }
                            // south
                            else if (direction.z == -1)
                            {

                                boxi.minZ = last.z - z;
                                boxi.minX = last.x + x;
                                boxi.maxZ = last.z;
                                boxi.maxX = last.x;
                            }
                    
                             if (!checkIntersections(boxi) && checkCoordinatesGreaterZero(boxi))
                                     {
                                     temp = false;
                                     break;
                                     }

                      }

                      numbers.Clear();
                      if (!temp)
                      {
                          break;
                      }
                      tempList.RemoveAt(tempList.Count - 1);
                  }
                  lastPos = last;
                  tempList.Clear();
                  
    }
   
    private void CreateCells(int height, int width, bool f1)
    {
      //  tiles = new Tile[sizeX, sizeZ];
        for (int x = 0; x < height; x++)
        {
            for (int z = 0; z < width; z++)
            {
                if (cave != null)
                {
                    if (cave.tiles[x, z] == 1)
                    {

                        generateCell(x, z, height, width, f1);
                    }
                }
                else
                {
                    generateCell(x, z, height, width, f1);
                }


            }
        }
        boundingboxes.Add(box);
        box = new BoundingBox();
        
    }

    private void generateCell(int x, int z, int height, int width, bool f1)
    {
        
        Tile tile = Instantiate(tilePrefab) as Tile;
        //      tiles[x, z] = tile;

        tile.transform.parent = transform;


        // north
        if (direction.z == 1)
        {
            tile.transform.localPosition = new Vector3(lastPos.x + x, 0f, lastPos.z + z);
            numberX = lastPos.x + x;
            numberZ = lastPos.z + z;
            tile.name = "Tile " + numberX + ", " + numberZ;

        }
        //east
        else if (direction.x == 1)
        {
            tile.transform.localPosition = new Vector3(lastPos.x + z, 0f, lastPos.z + x);
            numberX = lastPos.x + z;
            numberZ = lastPos.z + x;
            tile.name = "Tile " + numberX + ", " + numberZ;
        }
        //west
        else if (direction.x == -1)
        {
            tile.transform.localPosition = new Vector3(lastPos.x - z, 0f, lastPos.z + x);
            numberX = lastPos.x - z;
            numberZ = lastPos.z + x;
            tile.name = "Tile " + numberX + ", " + numberZ;

        }
        // south
        else if (direction.z == -1)
        {
            tile.transform.localPosition = new Vector3(lastPos.x + x, 0f, lastPos.z - z);
            numberX = lastPos.x + x;
            numberZ = lastPos.z - z;
            tile.name = "Tile " + numberX + ", " + numberZ;
        }
        else
        {
            tile.transform.localPosition = new Vector3(lastPos.x + x, 0f, lastPos.z + z);
            tile.name = "Tile " + lastPos.x + x + ", " + lastPos.z + z;

        }

        tileLocations.Add(new FVector2(tile.transform.localPosition.x, tile.transform.localPosition.z));
        activeTiles.Add(tile);

        if (x == 0 && z == 0)
        {
            box.minX = tile.transform.localPosition.x;
            box.minZ = tile.transform.localPosition.z;
            box.maxX = tile.transform.localPosition.x;
            box.maxZ = tile.transform.localPosition.z;

            box1.minX = tile.transform.localPosition.x;
            box1.minZ = tile.transform.localPosition.z;
        
        }
        else
        {


            box.minX = Mathf.Min(tile.transform.localPosition.x, box.minX);
            box.minZ = Mathf.Min(tile.transform.localPosition.z, box.minZ);
            box.maxX = Mathf.Max(tile.transform.localPosition.x, box.maxX);
            box.maxZ = Mathf.Max(tile.transform.localPosition.z, box.maxZ);
        }
        if ((x == (height - 1)) && (z == (width - 1)))
        {
           
            box1.maxX = tile.transform.localPosition.x;
            box1.maxZ = tile.transform.localPosition.z;
            if (f1)
            {
                lastPos.x = tile.transform.localPosition.x;
                lastPos.z = tile.transform.localPosition.z;
            }   
          }
    }

    public void generateWalls()
    {
        
        List<FVector2> vec = new List<FVector2>();
        vec = createWallCoordinates();
        for (int i = 0; i < vec.Count; i++)
        {
            Wall wall = Instantiate(wallPrefab) as Wall;
            wall.name = "Wall " + vec[i].x + ", " + vec[i].z;
            wall.transform.parent = transform;
            wall.transform.localPosition = new Vector3(vec[i].x,0f, vec[i].z);
            walls.Add(wall);
        }
    }
    public List<FVector2> createWallCoordinates()
    {
        FVector2 vector = new FVector2(0,0);
        FVector2 temp = new FVector2(0, 0);
        List<FVector2> result = new List<FVector2>();
        for (int i = 0; i < tileLocations.Count; i++)
        {
            vector = tileLocations[i];
            int number = 0;
            for (int a = 0; a < Directions.vectors.Length; a++)
            {
                number = 0;
                temp = Directions.vectors[a] + vector;
                for (int b = 0; b < tileLocations.Count; b++){
                    if (temp.x == tileLocations[b].x && temp.z == tileLocations[b].z) break;
                    number++;
                    if (number == tileLocations.Count)
                    {
                        result.Add(temp);
                    }
                    
                }

            }
        } 

        
        return result;
    }



    public FVector2 generateNextCoordinate(bool floor, int bbox)
    {
        if (numbers.Count == 4)
        {
            return new FVector2();
        }
        FVector2 nextCoordinate = new FVector2();
        int number = 5;
        
        bool a = true;
       
        
        while (a)
        {
            number = Random.Range(0, Directions.Count);
            a = false;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == number)
                {
                    a =true;
                }
            }
            
        }
        
        
        BoundingBox b = null;
        if (boundingboxes[0] != null)
        {
            b = boundingboxes[bbox];
        }
        
       
        if (floor)
        {
            nextCoordinate = direction + lastPos;
        }
        else
        {
            FVector2 tempCoordinate = new FVector2();
            switch (number)
            {
            case north:
                tempCoordinate = new FVector2();
                tempCoordinate.x = Mathf.Ceil( b.minX + (b.maxX - b.minX)/2) ;
                tempCoordinate.z = b.maxZ;
                direction = Directions.vectors[number];
                nextCoordinate = direction + tempCoordinate;
                break;
            case east: 
                tempCoordinate = new FVector2();
                tempCoordinate.x = b.maxX;
                tempCoordinate.z = Mathf.Ceil(b.minZ + (b.maxZ - b.minZ) / 2) ;
                direction = Directions.vectors[number];
                nextCoordinate = direction + tempCoordinate;
                break;
            case south: 
                tempCoordinate = new FVector2();
                tempCoordinate.x = Mathf.Ceil(b.minX + (b.maxX - b.minX) / 2) ;
                tempCoordinate.z = b.minZ;
                direction = Directions.vectors[number];
                nextCoordinate =  direction + tempCoordinate;
                break;
            case west: 
                tempCoordinate = new FVector2();
                tempCoordinate.x = b.minX;
                tempCoordinate.z = Mathf.Ceil(b.minZ + (b.maxZ - b.minZ) / 2) ;
                direction = Directions.vectors[number];
                nextCoordinate = direction + tempCoordinate;
                break;
            
            }
        }
        numbers.Add(number);
        return nextCoordinate;
    }

    public bool intersectBoundingBoxes(BoundingBox b1, BoundingBox b2){
        if (Mathf.Abs(b1.maxX) < Mathf.Abs(b2.minX)) return false;
        if (Mathf.Abs(b1.minX) > Mathf.Abs(b2.maxX)) return false;
        if (Mathf.Abs(b1.maxZ) < Mathf.Abs(b2.minZ)) return false;
        if (Mathf.Abs(b1.minZ) > Mathf.Abs(b2.maxZ)) return false;
        return true;
    }

    public bool checkIntersections(BoundingBox b)
    {
        for (int i = 0; i < boundingboxes.Count; i++)
        {
            if(intersectBoundingBoxes(b,boundingboxes[i])){
                return true;
            }
        }
        return false;
    }

    public bool checkCoordinatesGreaterZero(BoundingBox b)
    {
        if(b.minX < 0 || b.minZ < 0 || b.maxX < 0 || b.maxZ <0) return false;
        return true;
    }

    public List<BoundingBox> copyBoundingBoxList(){
        List<BoundingBox> result = new List<BoundingBox>();
        for (int i = 0; i < boundingboxes.Count; i++)
        {
            BoundingBox b1 = new BoundingBox();
            b1.maxX = boundingboxes[i].maxX;
            b1.maxZ = boundingboxes[i].maxZ;
            b1.minX = boundingboxes[i].minX;
            b1.minZ = boundingboxes[i].minZ;
            result.Add(b1);
        }
        return result;
        
    }
}
