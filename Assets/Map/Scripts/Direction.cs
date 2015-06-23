using UnityEngine;

public enum Direction {
	North,
	East,
	South,
	West
}

public static class Directions {

	public const int Count = 4;
 

	public static Direction RandomValue {
		get {
			return (Direction)Random.Range(0, Count);
		}
	}

	private static Direction[] opposites = {
		Direction.South,
		Direction.West,
		Direction.North,
		Direction.East
	};

	public static Direction GetOpposite (this Direction direction) {
		return opposites[(int)direction];
	}

	public static Direction GetNextClockwise (this Direction direction) {
		return (Direction)(((int)direction + 1) % Count);
	}

	public static Direction GetNextCounterclockwise (this Direction direction) {
		return (Direction)(((int)direction + Count - 1) % Count);
	}
	//north east south west
	public static FVector2[] vectors = {
        new FVector2(0, 1),
        new FVector2(1, 0),
        new FVector2(0, -1),
		new FVector2(-1, 0)
		
	};
	
	public static FVector2 ToIntVector2 (this Direction direction) {
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};
	
	public static Quaternion ToRotation (this Direction direction) {
		return rotations[(int)direction];
	}
  
}