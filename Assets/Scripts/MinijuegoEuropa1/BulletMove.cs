using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public int damage;
    public float speed;
    Vector3 direction;
    Vector2 initialPos;
    public void SetTarget(Vector3 objective, Vector3 spawn)
    {
        direction = (objective - spawn).normalized;
        direction.y = 0;
        initialPos = transform.position;
    }
   
    public void Move()
    {
        float delta = Time.deltaTime * speed;
        transform.position += direction * delta;
        if (Vector3.Distance(initialPos, transform.position) > 300f)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
