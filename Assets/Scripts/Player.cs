using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed;

    private void Update()
    {
        Vector2 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input *= WalkSpeed * Time.deltaTime;

        Vector3 newPos = transform.position + new Vector3(input.x, 0f, input.y);
        newPos.x = Mathf.Clamp(newPos.x, -21.75f, 21.75f);
        newPos.z = Mathf.Clamp(newPos.z, -21.75f, 21.75f);

        transform.position = newPos;

        FogOfWar.Instance.RevealMap(transform.position);
    }
}
