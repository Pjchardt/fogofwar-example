using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public float NumRocks;
    public Vector2 RockSizeRange;
    public Vector2 RockBoundingSquare;
    public Material RockMaterial;

	void Start ()
    {
		for (int i = 0; i < NumRocks; i++)
        {
            Vector3 size = new Vector3(Random.Range(RockSizeRange.x, RockSizeRange.y), Random.Range(RockSizeRange.x, RockSizeRange.y), Random.Range(RockSizeRange.x, RockSizeRange.y));
            Vector3 position = new Vector3(Random.Range(-RockBoundingSquare.x, RockBoundingSquare.x), size.y * .5f, Random.Range(-RockBoundingSquare.y, RockBoundingSquare.y));
            GenerateRock(position, size);
        }
	}
	
	void GenerateRock(Vector3 pos, Vector3 size)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
        obj.transform.localScale = size;
        obj.GetComponent<Renderer>().material = RockMaterial;
    }
}
