using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSpawner : MonoBehaviour
{
    public GameObject hold;
    List<GameObject> holds;
    public float maxtime = 3, timer = 0, lowestpoint = 0, spacing = 2;
    int holdcount = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        holds = new List<GameObject>();
        PreWarm();
    }

    // Update is called once per frame
    void Update()
    {


        if (timer < maxtime)
        {
            timer += Time.deltaTime;
            RemoveFirstHold();
        }
        else
        {
            holdcount = Random.Range(1, 5);
            timer = 0;
            for(int i = 0; i < holdcount; i++)
            {
                SpawnHolds(Random.Range(1f, 4f));
            }
        }
    }

    void SpawnHolds(float i)
    {
        Vector3 newpos = transform.position;
        GameObject temp = (GameObject)Instantiate(hold, newpos + (transform.right * Random.Range(0.3f,1.3f) * i), hold.transform.rotation);
        holds.Add(temp);
    }
    void PreWarm() {

        Vector3 pos = transform.position;

        transform.position = new Vector3(pos.x,0,pos.z);

        while(transform.position.y < pos.y ) {

            transform.position += Vector3.up * spacing;
            holdcount = Random.Range(1, 5);
            for (int i = 0; i < holdcount; i++)
            {
                SpawnHolds(Random.Range(1f, 4f));
            }
        }

    }
    void RemoveFirstHold()
    {
        if (holds.Count>0)
        {
            if(holds[0].transform.position.y < lowestpoint) {
                Destroy(holds[0]);
                holds.RemoveAt(0);
            }
        }
    }
}
