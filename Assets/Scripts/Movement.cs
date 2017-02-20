using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed;
    private Vector3 position;
    private float xAxis;
    public Object cylinder;
    public GameObject lastCylinder;
    private bool isSafe = true;
    private GameObject instantiated;
    public GameObject enemy;
    public float waittime;
    float timer;


    // Use this for initialization
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waittime)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        transform.position = position;

        position.z += Time.deltaTime * speed;

        position.x += Input.GetAxis("Horizontal") / 10;
        position.y += Input.GetAxis("Vertical") / 10;
    }

    void SpawnEnemy()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-1f, 1f), transform.position.z + 5f);
        Instantiate(enemy, newPos, transform.rotation);
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("spawn"))
        {
            Vector3 newPos = new Vector3(lastCylinder.transform.position.x, lastCylinder.transform.position.y, lastCylinder.transform.position.z + (5.31f * 2));
            instantiated = (GameObject) Instantiate(cylinder, newPos, lastCylinder.transform.rotation);
        }
        if (other.tag.Equals("destroy"))
        {
            Destroy(lastCylinder);
            lastCylinder = instantiated;
        }
        if (other.tag.Equals("enemy"))
        {
            Destroy(this);
        }
    }
}
