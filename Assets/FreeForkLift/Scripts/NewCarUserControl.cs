using System;
using UnityEngine;

    [RequireComponent(typeof (NewCarController))]
public class NewCarUserControl : MonoBehaviour
    {
        private NewCarController m_Car; // the car controller we want to use

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<NewCarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = Input.GetAxis("Horizontal");
            //float v = Input.GetAxis("Vertical");

            //float handbrake = Input.GetAxis("Jump");

            //m_Car.Move(h, v, v, handbrake);
            

        }
    }

