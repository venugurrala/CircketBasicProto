using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bowling : MonoBehaviour
{


    public GameObject _SwipControler;
    public GameObject _camera;
    public GameObject setCamPositonMidel;
    public GameObject SetCamPosBhWickets;
    

    public Transform _bouncePosition;

    public GameControls _Gamecontrols;

    public GameObject[] wickets;
    Vector3[] WicketInitalPosition;
    Quaternion[] WicketRotaton;

    public float ballSpeed=20;
    public float offval = 0.5f;
    [HideInInspector]
    public Vector3 _ballInitialposition;

    Rigidbody _rigidbody;
    [HideInInspector]
    public Vector3 _directon;


   
    

    [HideInInspector]
  public  bool firstPichDone = false;

    private float spin;

    void Start()
    {


        WicketInitalPosition = new Vector3[wickets.Length];
        WicketRotaton = new Quaternion[wickets.Length];

        for (int i = 0; i < wickets.Length; i++)
        {
          WicketInitalPosition[i]=  wickets[i].transform.position;
            WicketRotaton[i] = wickets[i].transform.rotation;
        }
        _ballInitialposition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();

    }

    

    public void ThrowBall()
    {

        _SwipControler.SetActive(true);
        BatMovement._inst.isballReleased = true;
        for (int i = 0; i < wickets.Length; i++)
        {
            wickets[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        
        GetComponent<TrailRenderer>().enabled = true;
        _directon = Vector3.Normalize(_bouncePosition.position - transform.position);
        _rigidbody.AddForce(_directon * ballSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //   Debug.LogError(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {

            if (!firstPichDone) { 
                firstPichDone = true;
            //  Debug.LogError("venu");
            StartCoroutine(MoveCamera(setCamPositonMidel.transform));
            _rigidbody.useGravity = true;

                

                if (offval !=5)
                {
                    spin = offval/ballSpeed;
                }
                
                else {
                    spin = _directon.x;
                }
                

               
                
                _directon = new Vector3(spin, -_directon.y * (0.08f * ballSpeed), _directon.z);
            _directon = Vector3.Normalize(_directon);
            _rigidbody.AddForce(_directon * ballSpeed, ForceMode.Impulse);
             }
        }

        if (collision.gameObject.tag == "Wicket")
        {
           // Debug.LogError("Wicket down");

            //  _directon = new Vector3(0, 0, 0);
            //  _rigidbody.isKinematic = true;
            // _rigidbody.isKinematic = false;
           // _rigidbody.mass = 10;
           
            Vector3 wicketDir = new Vector3(0,0,1);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(wicketDir,ForceMode.Impulse);
            for (int i = 0; i < wickets.Length; i++)
            {
                wickets[i].GetComponent<BoxCollider>().size= new Vector3(1, 1, 1);
            }
            
           // _directon = new Vector3(_directon.x, -_directon.y * (0.08f * ballSpeed), -_directon.z);
            //_directon = Vector3.Normalize(_directon);
            _rigidbody.AddForce(-wicketDir*10,ForceMode.Impulse);
        }

     /*   if (collision.gameObject.tag == "camTrigger")
        {
            
            Debug.LogError("I am trigger");
        }*/
    }


    public void ReSetToThrowTheBall()
    {
        firstPichDone = false;
      _SwipControler.SetActive(false);
        BatMovement._inst.isBallHitTheBat = false;
        BatMovement._inst.transform.position = BatMovement._inst._batInitialPosoition;
        BatMovement._inst.transform.rotation = BatMovement._inst._batInitalrotaion;
        //  _rigidbody.mass = 0;
        for (int i = 0; i < wickets.Length; i++)
        {
            wickets[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            wickets[i].GetComponent<Rigidbody>().isKinematic = true;
            wickets[i].transform.position = WicketInitalPosition[i];
            wickets[i].transform.rotation = WicketRotaton[i];

            wickets[i].GetComponent<BoxCollider>().size = new Vector3(3, 1, 28);

        }
       
        
        GetComponent<TrailRenderer>().enabled = false;
       transform.position = _ballInitialposition;
      _rigidbody.useGravity = false;
       _rigidbody.velocity = new Vector3(0, 0, 0);

        StartCoroutine(MoveCamera(SetCamPosBhWickets.transform));
    }


  public  IEnumerator MoveCamera(Transform target)
    {
       // Debug.Log(Vector3.Distance(_camera.transform.position, target.position));
        while (Vector3.Distance(_camera.transform.position, target.position) > 0.5f)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position,target.position,20*Time.deltaTime);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation,target.rotation,5*Time.deltaTime);
           yield return null;

        }
        
    }
}
