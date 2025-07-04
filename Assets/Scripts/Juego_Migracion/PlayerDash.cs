using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    public GameObject joystickPanel;

    private bool canDash = true;
    private bool isDashing = false;
    private Vector3 dashDirection;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing)
        //{
        //    Debug.Log("Hizo Dash");
        //    StartCoroutine(Dash());
            
        //}
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (joystickPanel != null)
            joystickPanel.SetActive(false);

        dashDirection = transform.forward;
        float distanceTraveled = 0;

        while (distanceTraveled < dashDistance)
        {
            float step = dashSpeed * Time.deltaTime;
            transform.position += dashDirection * step;
            distanceTraveled += step;
            yield return null;
        }

        if (joystickPanel != null)
            joystickPanel.SetActive(true);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void HacerDash()
    {
        StartCoroutine(Dash());
    }

    public bool IsDashing() => isDashing;

}
