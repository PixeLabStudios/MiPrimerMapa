using UnityEngine;

public class DrakkarBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    Vector3 firstPos;
    void Start()
    {
        damage = 2;
        speed = 45;
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
        if (Vector3.Distance(firstPos, transform.position) > 250) { Destroy(this.gameObject); }
    }
    

 
}
