using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private MyVector2D displacement;
    [SerializeField] MyVector2D ballPosition;
    [SerializeField] Camera camera;

    MyVector2D velocity;
    [SerializeField] MyVector2D acceleration;
    private float elapsedTime;


    [Range(0f, 1f)] [SerializeField] float dampingFactor = 0.9f;

    //[SerializeField] Transform ballTransform;
    private void Start()
    {
        ballPosition = new MyVector2D(transform.position.x, transform.position.y);

        
    }

    private void FixedUpdate() // para garantizar un deltatime constante 
    {
        Move();
    }


    private void Update()
    {
        //displacement.Draw(ballPosition, Color.red);
        ballPosition.Draw(Color.blue);
        //velocity.Draw(ballPosition, Color.cyan);
        acceleration.Draw(ballPosition, Color.red);
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            acceleration = AccelerationChange(acceleration);
            Debug.Log("Acceleration changed"); 
        }
    }



    public void Move()
    {
        // la integral de la aceleracion es la velocidad 
        // la integral de la velocidad es la posicion 

        velocity = velocity + acceleration * Time.fixedDeltaTime;
        displacement = Time.fixedDeltaTime * velocity;
        ballPosition = ballPosition + displacement;

        Debug.Log("The ball is moving");

        //horizontal bounce check

        if (Mathf.Abs(ballPosition.x) > camera.orthographicSize)
        {
            velocity.x = velocity.x * -1;
            ballPosition.x = Mathf.Sign(ballPosition.x) * camera.orthographicSize;
            velocity *= dampingFactor; // damping factor
        }

        //Vertical bounce check

        if (Mathf.Abs(ballPosition.y) > camera.orthographicSize)
        {
            velocity.y = velocity.y * -1;
            ballPosition.y = Mathf.Sign(ballPosition.y) * camera.orthographicSize;
            velocity *= dampingFactor; // damping factor
        }

        transform.position = new Vector3(ballPosition.x, ballPosition.y, 0);
    }

    private MyVector2D AccelerationChange(MyVector2D acceleration) 
    
    {
        if (acceleration.x != 0)
        {
            acceleration.y = acceleration.x;
            acceleration.x = 0;
            velocity.x = 0; // para cambiar eje instantaneamente
        }
        else if (acceleration.y != 0)
        {
            acceleration.x = -acceleration.y;
            acceleration.y = 0;
            velocity.y = 0; // para cambiar eje instantaneamente
        }

        return acceleration;
    }
}
