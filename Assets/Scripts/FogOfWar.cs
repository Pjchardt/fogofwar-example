using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar Instance;

    [Tooltip("Layer Fog of War mesh is on")]
    public LayerMask RaycastMask;
    [Tooltip("Size of brush in pixels")]
    public float BrushSize;
    [Tooltip("Texture to use to uncover map")]
    public Texture2D Brush;
    [Tooltip("Render texture")]
    public RenderTexture rt;

    Vector2 RTSize;
    Vector3 lastPos;
    RenderTexture temp;
    Renderer r;

    private void Awake()
    {
        Instance = this;
        RTSize = new Vector2(rt.width, rt.height);
    }

    private void Start()
    {
        r = GetComponent<Renderer>();

        RenderTexture.active = rt;
        //Work in the pixel matrix of the texture resolution.
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, RTSize.x, RTSize.y, 0);
        GL.Clear(false, true, Color.black);
        //Revert the matrix and active render texture.
        GL.PopMatrix();
        RenderTexture.active = null;

        lastPos = Vector3.positiveInfinity;
    }

    private void OnDestroy()
    {
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, RTSize.x, RTSize.y, 0);
        GL.Clear(false, true, Color.black);
        GL.PopMatrix();
        RenderTexture.active = null;
    }

    public void RevealMap(Vector3 pos)
    {
        Vector3 location = Camera.main.transform.position;

        if (Vector3.Distance(location, lastPos) < .1f)
        {
            return;
        }

        lastPos = location;
        Vector3 direction = (pos - location).normalized;

        Ray inputRay = new Ray(location, direction);

        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity, RaycastMask))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            Vector2 uv = hit.textureCoord;
            Debug.Log(uv.ToString());

            //Activate the render texture (the one that's on your model)
            RenderTexture.active = rt;
            //Work in the pixel matrix of the texture resolution.
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, RTSize.x, RTSize.y, 0);

            //Paint the desired texture onto the desired coordinates with the desired size.
            //the "hit" variable is the RaycastHit
            Vector2 coord = new Vector2(uv.x * RTSize.x, RTSize.y - uv.y * RTSize.y);
            Graphics.DrawTexture(new Rect(coord.x - BrushSize / 2, (coord.y - BrushSize / 2), BrushSize, BrushSize), Brush);

            //Revert the matrix and active render texture.
            GL.PopMatrix();
            RenderTexture.active = null;
        }
    }
}
