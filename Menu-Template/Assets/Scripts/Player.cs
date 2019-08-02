using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    float runSpeed = 5f;
    float jumpSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D mybodyCollider;
    BoxCollider2D myfeetCollider;

    // Start is called before the first frame update
    void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mybodyCollider = GetComponent<CapsuleCollider2D>();
        myfeetCollider = GetComponent<BoxCollider2D>();



    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
    }

    void Run()
    {
        //obtener el float(decimal) del control que va del -1 a 1
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        //inicializar un vector de dos dimensiones que solo modifica el componente x
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);

        //Asignar la nueva velocidad a mi rigid body
    
        myRigidBody.velocity = playerVelocity;


        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > 0;

        //aplicar animacion de correr seteando la condicion de running del animator
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
        

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > 0;

        //preguntamos si la condicion es verdadera
        if (playerHasHorizontalSpeed)
        {
            //si es verdadera, toma el signo de la velocidad en x y a ltera la escala en esa dimension
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void Jump()
    {
        if(!myfeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }



        //obtener el booleano (true/false) del boton representado por el tag jump
        bool isJumpButtonPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        if(isJumpButtonPressed)
        {
            Vector2 jumpVelocity = new Vector2(0,jumpSpeed);
            //sumarle a la velocidad que ya tiene mi nuevo vector de velocidad
            myRigidBody.velocity += jumpVelocity;


        }

    }

}
