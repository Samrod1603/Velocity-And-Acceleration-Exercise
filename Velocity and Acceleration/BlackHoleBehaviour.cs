using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBehaviour : MonoBehaviour
{
    private MyVector2D displacement;

    private MyVector2D ballPosition;
    private MyVector2D blackHolePosition;


    [SerializeField] private Transform blackHole;

    MyVector2D velocity;
    [SerializeField] MyVector2D acceleration;
    


    

    //[SerializeField] Transform ballTransform;
    private void Start()
    {
        
        ballPosition = new MyVector2D(transform.position.x, transform.position.y);
        
    }

    private void FixedUpdate() // para garantizar un deltatime constante 
    {
        blackHolePosition = new MyVector2D(blackHole.position.x, blackHole.position.y);
        Move();
        acceleration = blackHolePosition - ballPosition;
    }


    private void Update()
    {
        //displacement.Draw(ballPosition, Color.red);
        ballPosition.Draw(Color.blue);
        //velocity.Draw(ballPosition, Color.cyan);
        acceleration.Draw(ballPosition, Color.red);
        
    }



    public void Move()
    {
        // la integral de la aceleracion es la velocidad 
        // la integral de la velocidad es la posicion 

        velocity = velocity + acceleration * Time.fixedDeltaTime;
        displacement = Time.fixedDeltaTime * velocity;
        ballPosition = ballPosition + displacement;

        Debug.Log("The ball is moving");

        transform.position = new Vector3(ballPosition.x, ballPosition.y, 0);
    }

   
}
