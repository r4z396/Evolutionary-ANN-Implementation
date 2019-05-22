using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float sensorLenght = 5f;

    public float leftSensorValue;
    public float centerSensorValue;
    public float rightSensorValue;
    public float rightSensorValueHalf;
    public float leftSensorValueHalf;
    [SerializeField] Vector3 angleLeft;
    [SerializeField] Vector3 angleRight;
    [SerializeField] Vector3 positionRayLunch;
    [SerializeField] GameObject pointRay;
    private NewCarController m_Car;

    [SerializeField] float Fitness = 0;
    int layerMask = 1 << 7;
    [SerializeField] float distanceTraveled =0;
    Vector3 previousPosition = Vector3.zero;
    bool crashed = false;
    public int angle=45;
    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
       
        m_Car = GetComponent<NewCarController>();
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 directionRight = Quaternion.AngleAxis(45, transform.up) * transform.forward;
        Vector3 directionLeft = Quaternion.AngleAxis(-45, transform.up) * transform.forward;
        Vector3 directionRightHalf = Quaternion.AngleAxis(25, transform.up) * transform.forward;
        Vector3 directionLeftHalf = Quaternion.AngleAxis(-25, transform.up) * transform.forward;

        Ray centerRay = new Ray(pointRay.transform.position, transform.forward);
      
        Ray leftRay = new Ray(pointRay.transform.position, directionLeft);
        Ray rightRay = new Ray(pointRay.transform.position, directionRight);
        Ray leftRayHalf = new Ray(pointRay.transform.position, directionLeftHalf);
        Ray rightRayHalf = new Ray(pointRay.transform.position, directionRightHalf);


        // Center
        if (Physics.Raycast(centerRay, out hit, sensorLenght))
        {
            //Vector3 forward = transform.TransformDirection(transform.forward) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, transform.forward*sensorLenght, Color.red);
            //Debug.Log("center" + hit.distance+ hit.transform.tag);
            centerSensorValue = hit.distance;
          //Debug.Log(transform.name+" "+ hit.collider.name);

        }
        else
        {
            //Vector3 forward = transform.TransformDirection(transform.forward) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position,transform.forward*sensorLenght, Color.green);
            centerSensorValue = sensorLenght;
            //Debug.Log("noo"+ transform.name);
        }

        //left
        if (Physics.Raycast(leftRay, out hit, sensorLenght))
        {
           // Vector3 forward = transform.TransformDirection((transform.forward - transform.right).normalized) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionLeft*sensorLenght, Color.red);
            //Debug.Log("left " + hit.distance + hit.transform.tag);
            leftSensorValue = hit.distance;
            //Debug.Log("LEFT " + transform.name + " " + hit.collider.name);

        }
        else
        {
            //Vector3 forward = transform.TransformDirection((transform.forward - transform.right).normalized) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionLeft*sensorLenght, Color.green);
            leftSensorValue = sensorLenght;
        }
        //right
        if (Physics.Raycast(rightRay, out hit, sensorLenght))
        {
            //Vector3 forward = transform.TransformDirection(angleRight) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionRight*sensorLenght, Color.red);
            //Debug.Log("right " + hit.distance + hit.transform.tag);
            rightSensorValue = hit.distance;
            //Debug.Log("RIGHT " + transform.name + " " + hit.collider.name);
        }
        else
        {
            //Vector3 forward = transform.TransformDirection(angleRight) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionRight*sensorLenght, Color.green);
            rightSensorValue = sensorLenght;
        }

        //left half
        if (Physics.Raycast(leftRayHalf, out hit, sensorLenght))
        {
            // Vector3 forward = transform.TransformDirection((transform.forward - transform.right).normalized) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionLeftHalf * sensorLenght, Color.red);
            //Debug.Log("left " + hit.distance + hit.transform.tag);
            leftSensorValueHalf = hit.distance;
            //Debug.Log("LEFT HALF" + transform.name + " " + hit.collider.name);

        }
        else
        {
            //Vector3 forward = transform.TransformDirection((transform.forward - transform.right).normalized) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionLeftHalf * sensorLenght, Color.green);
            leftSensorValueHalf = sensorLenght;
        }

        //right half
        if (Physics.Raycast(rightRayHalf, out hit, sensorLenght))
        {
            //Vector3 forward = transform.TransformDirection(angleRight) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionRightHalf * sensorLenght, Color.red);
            //Debug.Log("right " + hit.distance + hit.transform.tag);
            rightSensorValueHalf = hit.distance;
            //Debug.Log("RIGHT HALF"+transform.name + " " + hit.collider.name);
        }
        else
        {
            //Vector3 forward = transform.TransformDirection(angleRight) * sensorLenght;
            Debug.DrawRay(pointRay.transform.position, directionRightHalf * sensorLenght, Color.green);
            rightSensorValueHalf = sensorLenght;
        }

        if (!crashed)
        {
            distanceTraveled += Vector3.Distance(transform.position, previousPosition);
            previousPosition = transform.position;
            //Debug.Log(distanceTraveled);
        }

        //m_Car.Move(h, v, v, 0);
        //  this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 20);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        if (collision.transform.tag == "Wall")
        {
            //Debug.Log("crash");
            crashed = true;
            m_Car.m_Rigidbody.isKinematic = true;
        }

        
       
    }
    public float getFitness()
    {
        Fitness = distanceTraveled;
        //if (!crashed)
        //    Fitness = Fitness*1.5f;

        //Fitness = (Mathf.Pow(2, Fitness) - 1) / (2 - 1);
        //Debug.Log(Fitness);
        return Fitness;
    }
    private void FixedUpdate()
    {
        //h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");

        //float handbrake = Input.GetAxis("Jump");

        
    }

    public void Reset()
    {
        h = 0;
        v = 0;
        m_Car.Move(0, 0, 0, 0);
        m_Car.Reset();
        Fitness = 0;
        distanceTraveled = 0;
        crashed = false;
        m_Car.m_Rigidbody.isKinematic = false;
    }
    public double[] getInputs()
    {
        double[] inputs = { centerSensorValue, leftSensorValue,rightSensorValue,leftSensorValueHalf,rightSensorValueHalf ,m_Car.CurrentSpeed};

        return inputs;
    }

    public void activate(double[] outputs)
    {
        if (!crashed)
        {
            if (outputs[0] > 0.5)
            {
                //0.5 -------1
                //0----------1
               
                h = (float)outputs[0]-(1 - (float)outputs[0]);
            }
            else if (outputs[0] < 0.5)
            {
                //0.499------ 0
                //0 ---------- 1
                
                    
                

                h = -(( 0.5f-(float)outputs[0] )*2);
            }
            else 
                h= 0;

            //if (outputs[1] >= 0.5)
            //{
            //    v = (float)outputs[1];
            //}
            //else
            //{
            //    v = -(float)outputs[1];
            //}
            // v = (float)outputs[1];
            if (outputs[1] > 0.5)
            {
                //0.5 -------1
                //0----------1

                v = (float)outputs[1] - (1 - (float)outputs[1]);
            }
            else if (outputs[1] < 0.5)
            {
                //0.499------ 0
                //0 ---------- 1




                v = -((0.5f - (float)outputs[1]) * 2);
            }
            else
                v = 0;

        }
        else
        {
            h = 0;
            v = 0;
        }
        m_Car.Move(h, v, v, 0);

    }
}
