using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{

    public static BatMovement _inst;
    public GameObject ball;

    public bool isballReleased = false;
    public bool isSwipDone=false;
    public bool isBallHitted=false;

    public float boundaryPointX;
    public Vector3 ballsPositionAtHit;

    public Transform BatHitCampos;

    [HideInInspector]
   public bool isBallHitTheBat = false;

    [HideInInspector]
   public float _batSpeed=0;

    [HideInInspector]
    public Vector3 _batInitialPosoition;

    [HideInInspector]
    public Quaternion _batInitalrotaion;
    private void Awake()
    {
        _inst = this;
        _batInitialPosoition = transform.position;
        _batInitalrotaion = transform.rotation;
    }
  

    private void Update()
    {

        if (isBallHitTheBat)
            return;

        if (!ball.GetComponent<Bowling>().firstPichDone && ball.transform.position.z < transform.position.z && !isBallHitTheBat)
        {
            transform.transform.position = new Vector3(ball.transform.position.x,
                transform.transform.position.y,
                transform.transform.position.z);
           
        }
       

        
        if(!isBallHitTheBat)
        transform.position = new Vector3( Mathf.Clamp(transform.position.x, -boundaryPointX, boundaryPointX), transform.position.y, transform.position.z);

        if (!isBallHitTheBat && ball.GetComponent<Bowling>().firstPichDone && ball.transform.position.z <= transform.position.z)
        {
            transform.position = 
           Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x,Mathf.Clamp( ball.transform.position.y,0.1f,1), transform.position.z-0.1f), Time.deltaTime * 20);
        }

 



     
    }

   public void BatSwipDirection(float drageAngle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, drageAngle, transform.rotation.eulerAngles.z);

    }

    private void OnCollisionEnter(Collision collision)
    {

       // Debug.LogError(collision.gameObject.tag);
        if (collision.gameObject.tag == "ball")
        {
            isBallHitTheBat = true;

           // Vector3 _direction// = transform.TransformDirection;
           // Debug.LogError(ball.transform+"I am Here");

           // ContactPoint contact = collision.contacts[0];

           // Vector3 pos = contact.point;
           // Debug.LogError(pos);


            HitBall(collision.gameObject, (transform.forward), _batSpeed);
            // ball.transform.TransformDirection(transform.forward);
            //     ball.GetComponent<Rigidbody>().AddForce(transform.forward,ForceMode.Impulse);
        }
    }

    void HitBall(GameObject ball,Vector3 hitDirecton,float batHitSpeed)
    {
        ball.GetComponent<Bowling>()._SwipControler.SetActive(false);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Bowling>()._directon = Vector3.Normalize(hitDirecton);
      
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<Rigidbody>().AddForce(-ball.GetComponent<Bowling>()._directon*batHitSpeed, ForceMode.Impulse);

        StartCoroutine(ball.GetComponent<Bowling>().MoveCamera(BatHitCampos));
        
    }


    public void EnabelBatCollider()
    {
        float _distaceBatBall = Vector3.Distance(ball.transform.position, transform.position);
        if (_distaceBatBall < 1.5f)
            transform.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisabelBatCollider()
    {
            transform.GetComponent<BoxCollider>().enabled = false;
    }


}
