using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    //public Joystick joystick;
    public Animator Manimator;
    public Animator Fanimator;
    //public GameObject joystickPanel;
    public Transform carryPoint;

    private CharacterController controller;
    //private Vector3 moveDirection;
    private GameObject carriedObject;
    private PlayerDash dashScript;
    private ItemCarrier itemCarrier;

    public Transform respawnPoint;
    public float knockbackForce = 5f;
    [HideInInspector]public bool isStunned = false;

    public bool IsCarrying => carriedObject != null;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        dashScript = GetComponent<PlayerDash>();
        itemCarrier = GetComponent<ItemCarrier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashScript.IsDashing()) return;

        //Vector3 input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //moveDirection = input.normalized;
        //controller.Move(moveSpeed * Time.deltaTime * moveDirection);

        //if (input != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveDirection);
        //}

        //animator.SetFloat("Speed", moveDirection.magnitude);
    }

    public void PickUpObject(GameObject obj)
    {
        if (carriedObject != null) return;

        carriedObject = obj;
        carriedObject.GetComponent<Rigidbody>().isKinematic = true;
        carriedObject.transform.SetParent(carryPoint);
        carriedObject.transform.localPosition = Vector3.zero;
        carriedObject.transform.localRotation = Quaternion.identity;
    }

    public void DropObject()
    {
        if (carriedObject == null) return;

        carriedObject.transform.SetParent(null);
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    public bool CanAct() => !IsCarrying && !dashScript.IsDashing();

    public void OnHit(Vector3 hitDirection)
    {
        if (isStunned) return;

        isStunned = true;
        //animator.SetTrigger("Hurt"); // Asegurate de tener un Trigger llamado "Hurt"
        //VictoryManager.instance.RegisterHit();

        // Empuje
        StartCoroutine(HitReaction(hitDirection));
    }

    IEnumerator HitReaction(Vector3 direction)
    {
        float timer = 0.5f;
        float elapsed = 0f;

        while (elapsed < timer)
        {
            transform.position += direction * knockbackForce * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Espera a que termine la animación (opcionalmente podés usar anim events)
        yield return new WaitForSeconds(2f);

        TeleportToStart();
        isStunned = false;
    }

    void TeleportToStart()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("enemigo"))
        {
            Debug.Log("zzzzzzzzzzzzzzzzzzzzzzzz");
            //OnHit(transform.position);
        }
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("enemigo"))
        {
            Debug.Log("embestido");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemigo"))
        {
            Manimator.SetTrigger("stuned");
            Fanimator.SetTrigger("stuned");
            // Calcula la dirección base del empuje
            Vector3 hitDirection = (transform.position - other.transform.position);

            // Anula la componente 'y' para que no haya empuje vertical
            hitDirection.y = 0f;

            // Normaliza el vector para asegurar una fuerza consistente
            hitDirection = hitDirection.normalized;

            OnHit(hitDirection);
        }
    }
}
