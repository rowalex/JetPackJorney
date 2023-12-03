using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] Player pl;

    [SerializeField] Transform rightPoint;
    [SerializeField] Transform leftPoint;

    [SerializeField] List<GameObject> listOfEnvironment = new List<GameObject>();
    [SerializeField] float Y_edge;

    private List<GameObject> createdEnviroment = new List<GameObject>();


    [Serializable]
    enum expType
    {
        Exponential,
        Linear
    }

    [Header("Spawn")]
    [SerializeField] float spawnGap;

    [SerializeField] expType spawnGapType;

    [SerializeField] float spawnAccelerator;
    [Header("Speed")]
    [SerializeField] public float speed;

    [SerializeField] expType speedType;

    [SerializeField] float speedAccelerator;



    private void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {

        while (pl.isAlive)
        {
            Vector2 pos = new Vector2(rightPoint.position.x, UnityEngine.Random.Range(-Y_edge, Y_edge));

            int index = UnityEngine.Random.Range(0, listOfEnvironment.Count);


            GameObject obj = Instantiate(listOfEnvironment[index], pos, Quaternion.identity);

            if (obj.GetComponentInChildren<Spinning>())
            {
                float spinningSpeed;

                if (UnityEngine.Random.Range(0, 2) == 0)
                    spinningSpeed = UnityEngine.Random.Range(50, 150);
                else 
                    spinningSpeed = -UnityEngine.Random.Range(50, 150);


                obj.GetComponentInChildren<Spinning>().speed = spinningSpeed;
                obj.GetComponentInChildren<Spinning>().start_rot = UnityEngine.Random.Range(0, 90);
            }

            createdEnviroment.Add(obj);


            switch (spawnGapType)
            {
                case expType.Exponential: spawnGap *= spawnAccelerator; break;
                case expType.Linear: spawnGap += spawnAccelerator; break;
            }
            if (spawnGap < 1) spawnGap = 1;
            if (speed > 20 ) speed = 20;
            switch (speedType)
            {
                case expType.Exponential: speed *= speedAccelerator; break;
                case expType.Linear: speed += speedAccelerator; break;
            }

            yield return new WaitForSeconds(spawnGap);
        }
    }


    private void Update()
    {
        for (int i = 0; i < createdEnviroment.Count; i++)
        {
            createdEnviroment[i].transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (createdEnviroment[i].transform.position.x < leftPoint.position.x)
            {
                GameObject obj = createdEnviroment[i];
                createdEnviroment.RemoveAt(i);
                Destroy(obj);
                i--;
            }
        }
    }

}
