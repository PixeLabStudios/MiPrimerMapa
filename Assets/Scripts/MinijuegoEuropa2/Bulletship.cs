using UnityEngine;

public class Bulletship : Obstacle
{
    
    public float speed;
    Vector3 direction;
    Vector2 initialPos;
    public GameObject explosion;
    
    // Update is called once per frame
    void Update()
    {
        Move();   
    }
    public void SetTarget(Vector3 objective,Vector3 spawn) 
    {
        direction = (objective -spawn).normalized;
        initialPos = transform.position;
    }
    public override void Move() 
    {
        float delta = Time.deltaTime * speed;
        transform.position += direction * delta;
        if (Vector3.Distance(initialPos, transform.position) > 100f) 
        {
           
        }
    }

    public override void Impact(DrakkarScript script)
    {
        Debug.Log("Choque con una bala");
        script.ChangeHp(-1);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("agua")) 
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
