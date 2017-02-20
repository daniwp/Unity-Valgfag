using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    private Vector3 position;
    private float xAxis;
    public Object cylinder;
    public GameObject lastCylinder;
    private bool isSafe = true;
    private GameObject instantiated;
    public GameObject[] enemies;
    public float waittime;
    float timer;
    float multiplier = 1f;
    public UnityEngine.UI.Text text;
    public int score;
    private bool hasBall;
    private float startTime;
    public Material mat;
    private Color[] colors = { Color.black, Color.blue, Color.cyan, Color.clear, Color.red, Color.yellow };

    void Start()
    {
        score = 0;
        hasBall = false;
        startTime = Time.time;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)(Time.time - startTime);
        text.text =  "Score: " + score.ToString();

        timer += Time.deltaTime;
        if (timer > waittime)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        mat.color = colors[(int)Random.Range(0f, colors.Length)];

        transform.position = position;

        position.z += Time.deltaTime * speed;

        position.x += Input.GetAxis("Horizontal") / 10;
        position.y += Input.GetAxis("Vertical") / 10;
    }

    void SpawnEnemy()
    {
        //GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        multiplier += 1;
        foreach (GameObject enemy in enemies)
        {
            Vector3 newPos = new Vector3(transform.position.x + Random.Range(-0.8f, 0.8f), transform.position.y + Random.Range(-0.8f, 0.8f), transform.position.z + 5f);
            Instantiate(enemy, newPos, enemy.transform.rotation);
        }      
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!!");
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
            if (score < 15)
            {
                text.text = "You got " + score + " points.. Scrub score..";
            } else
            {
                text.text = "You got " + score + " points.. Average score..";
            }
            
            Destroy(this);
        }
    }
}
