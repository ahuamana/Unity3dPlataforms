using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public bool facingRight = true;
    public float moveDireccion;
    private new Rigidbody rigidbody;

    //animaciones
    private Animator anim;

    //velocidad de salto
    public float jumpSpeed = 500.0f;

    public bool grounded = false;
    public Transform groundedCheck;
    public float groundDistance = 0.5f;
    public LayerMask whatIsground;

    public float knifeSpeed = 500.0f;
    public Transform knifeSpawn;
    public Rigidbody knifePrefab;
    Rigidbody clone;

    private float rotatingInput;


    
    public float distToGround = 1f;
    

    void Attack()
    {

        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
    }



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        groundedCheck = GameObject.Find("GroundCheck").transform;
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;

    }

    // Update is called once per frame
    void Update()
    {
        rotatingInput = Input.GetAxis("Horizontal");
        moveDireccion = Input.GetAxis("Vertical");

        //if (Input.GetButtonDown("Jump") && grounded)
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rigidbodyConstraints = RigidbodyConstraints.None;
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            
            //animacion salto
            anim.SetTrigger("isJumping");
        }

        
        transform.Rotate(Vector3.up * Time.deltaTime * 250f * rotatingInput);
    }

    private void FixedUpdate()
    {
        Grounded();
        //Verificar que esta pisando
        //grounded = Physics2D.OverlapCircle(groundedCheck.position, 100f, whatIsground);
        grounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);
        

        transform.Translate(Vector3.forward * Time.deltaTime * maxSpeed * moveDireccion);
        //rigidbody.velocity = new Vector3(,rigidbody.velocity.y * Time.deltaTime);
        //rigidbody.velocity = Vector3.forward * Time.deltaTime * maxSpeed * moveDireccion;



        if (moveDireccion > 0.0f && !facingRight)
        {
            //Flip();

        } else {

            if (moveDireccion < 0.0f && facingRight)
            {
                //Flip();
            }

        }

        ////asignar la velocidad
        ////moveDireccion solo devuelve si positivo si avanza y negativo si no avanza
        anim.SetFloat("Speed", Mathf.Abs(moveDireccion));
        Debug.Log(moveDireccion);


        //Atacar si presiona el boton fire1 ( CTRL )
        if (Input.GetButtonDown("Fire1"))
        {

            Attack();
        }



    }

    private void Grounded()
    {
        
    }

    void Flip() {
        facingRight = !facingRight; //si esta mirando
        transform.Rotate(Vector3.up,180.0f, Space.World);//gira al eje y
        //transform.Rotate(new Vector3(0,1,0), 180.0f, Space.World);//gira al eje y
    }

    

}
