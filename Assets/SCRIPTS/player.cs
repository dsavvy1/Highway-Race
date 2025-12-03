using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;
    float xMin, xMax;
    [SerializeField] int playerHealth = 100;
   
   


    private void Start()
    {
     
        SetupMoveBounderies();
    }
    public void Update()
    {
      
        Move();
    }
    private void SetupMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f;
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        //Camera.main.transform.position = 
        //new Vector3(newXPos, newYPos, Camera.main.transform.position.z);
        transform.position = new Vector2(newXPos, transform.position.y);
    }
    
    

}
