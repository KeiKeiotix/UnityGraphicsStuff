using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaController : MonoBehaviour
{

    public GameObject beta;
    Animator animator;
    Rigidbody rb;

    bool isJumping = false;
    public float jumpHeight = 1;
    float timeSinceLastJump = 0;
    public float timeBetweenJumps = 5;

    bool isGrounded = true;

    List<SkinnedMeshRenderer> meshRenderer = new List<SkinnedMeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        animator = beta.GetComponent<Animator>();
        rb = beta.GetComponent<Rigidbody>();

        beta.GetComponentsInChildren<SkinnedMeshRenderer>(meshRenderer);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(beta.transform.position + Vector3.up * 0.01f, -Vector3.up, out hit, 0.02f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        animator.SetBool("Jump", !isGrounded);
    }



    void FixedUpdate()
    {
        //Debug.Log(isGrounded);



        //Debug.Log(timeSinceLastJump + " > " + timeBetweenJumps + " and " + hit.distance);



        if (timeSinceLastJump > timeBetweenJumps && isJumping && isGrounded && rb.velocity.y <= 0)
        {

            rb.AddForce(Mathf.Sqrt(-2 * jumpHeight * Physics.gravity.y) * Vector3.up, ForceMode.Impulse);
            //rb.AddForce(100 * Vector3.up);

            timeSinceLastJump = 0;
        }




        if (isGrounded)
        {
            timeSinceLastJump += Time.deltaTime;
        }



    }

    public void Jump()
    {
        isJumping = true;
    }
    public void StopJump()
    {
        isJumping = false;
    }

    public void SetPastel(float pastelValue)
    {
        int id = Shader.PropertyToID("pastelness");

        Debug.Log(meshRenderer.Count);

        for (int i = 0; i < meshRenderer.Count; i++)
        {
            meshRenderer[i].material.SetFloat(id, pastelValue);

            Debug.Log(meshRenderer[i].material.GetFloat(id) + " should be " + pastelValue);
        }
    }
}
