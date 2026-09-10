using UnityEngine;

public class WaterCurrent : Obstacle
{
    public GameObject particula;
    private void Update()
    {
        Move();
    }
    public override void Impact(DrakkarScript ship)
    {
        ship.ChangeSpeed(5);
        Debug.Log("choque con una corriente");
        Instantiate(particula, transform.position, particula.transform.rotation);
        Destroy(gameObject);
    }
}
