using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;
    public List<string> attack = new List<string>();
    public BoxCollider leftHand;
    public BoxCollider rightHand;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Grounded();
            Jump();
            Move();
            Attack();
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(startAttack());
    }

    IEnumerator startAttack()
    {
        int i = Random.Range(0, attack.Count - 1);
        this.anim.SetBool(attack[i], true);
        rightHand.enabled = true;
        leftHand.enabled = true;
        yield return new WaitForSeconds(1);
        this.anim.SetBool(attack[i], false);
        rightHand.enabled = false;
        leftHand.enabled = false;
    }

    public void Grounded()
    {
        if(Physics.CheckSphere(this.transform.position + Vector3.down, 0.2f, layerMask))
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }

        this.anim.SetBool("Jump", !this.grounded);
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && this.grounded)
        {
            this.rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        }
    }

    public void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movement = this.transform.forward * verticalAxis + this.transform.right * horizontalAxis;
        movement.Normalize();

        this.transform.position += movement * 0.0035f;

        this.anim.SetFloat("Vertical", verticalAxis);
        this.anim.SetFloat("Horizontal", horizontalAxis);
    }
}
