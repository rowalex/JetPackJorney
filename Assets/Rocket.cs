using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject sign;

    [SerializeField] Vector2 signOffset;

    bool isReady = false;
    Transform pl;
    Environment env;
    GameObject prep = null;

    private void Awake()
    {
        pl = GameObject.Find("Player").transform ? GameObject.Find("Player").transform : null;
        env = FindFirstObjectByType<Environment>();
        if (pl != null)
            StartCoroutine(Prepare());
    }

    IEnumerator Prepare()
    {
        prep = Instantiate(sign);

        yield return new WaitForSeconds(2);
        isReady = true;

        yield return null;
    }


    private void Update()
    {
        if (!isReady)
            transform.Translate(-Vector2.left * env.speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * env.speed * Time.deltaTime);

        if (prep != null && !isReady)
        {
            transform.position = new Vector3(transform.position.x, pl.position.y, transform.position.z);
            prep.transform.position = pl.position + (Vector3)signOffset;
        }

    }

    private void OnDestroy()
    {
        Destroy(prep);
    }


}
