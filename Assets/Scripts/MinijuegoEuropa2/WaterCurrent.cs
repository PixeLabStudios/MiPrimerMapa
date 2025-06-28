using UnityEngine;

public class WaterCurrent : Obstacle
{
    private void Update()
    {
        Move();
    }
    public override void Impact(DrakkarScript ship)
    {
        ship.ChangeSpeed(5);
        Debug.Log("choque con una corriente");
        Destroy(gameObject);
    }
}
