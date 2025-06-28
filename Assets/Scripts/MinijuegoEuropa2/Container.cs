using UnityEngine;

public class Container : Obstacle
{
    
    int slowValue;
    public override void Impact(DrakkarScript ship)
    {
        ship.ChangeSpeed(slowValue);
        Debug.Log("Choque con un contenedor. Baja la velocidad");

        Destroy(gameObject);
    }

    private void Start()
    {
        slowValue = -5;
    }

    private void Update()
    {
        Move();
    }
    public void ChangeMaterial(Material material) 
    {
        
        gameObject.GetComponent<Renderer>().material = material;
    }
}
