using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float start_rot;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, start_rot);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }

    
}
