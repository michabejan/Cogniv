[System.Serializable]
public struct FVector2 {

	public float x, z;

	public FVector2 (float x, float z) {
		this.x = x;
		this.z = z;
	}

	public static FVector2 operator + (FVector2 a, FVector2 b) {
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}