using UnityEngine;

public class DestroyObject : MonoBehaviour
{
   Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.position.x < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
