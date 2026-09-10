using UnityEngine;

public class Mine : Obstacle

{
    public GameObject explosion;
   
    private void Update()
    {
        Move();
    }
    public override void Impact(DrakkarScript script)
    {
        script.ChangeHp(-1);
        Debug.Log("Choque con una mina. Quito una vida" );
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(this.gameObject);
    }
}
