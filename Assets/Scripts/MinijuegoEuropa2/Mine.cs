using UnityEngine;

public class Mine : Obstacle

{
   
    private void Update()
    {
        Move();
    }
    public override void Impact(DrakkarScript script)
    {
        script.ChangeHp(-1);
        Debug.Log("Choque con una mina. Quito una vida" );
        Destroy(this.gameObject);
    }
}
