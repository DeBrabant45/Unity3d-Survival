﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PrototypeScripts
{
    public class BoatController : MonoBehaviour, IUsable
    {
        public GameObject player;
        public Transform placementForPlayer;

        //Speed calculations
        private float currentSpeed;
        private Vector3 lastPosition;

        void FixedUpdate()
        {
            CalculateSpeed();

            //Debug.Log(currentSpeed);
        }

        //Calculate the current speed in m/s
        private void CalculateSpeed()
        {
            //Calculate the distance of the Transform Object between the fixedupdate calls with 
            //'(transform.position - lastPosition).magnitude' Now you know the 'meter per fixedupdate'
            //Divide this value by Time.deltaTime to get meter per second
            currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;

            //Save the position for the next update
            lastPosition = transform.position;
        }

        public void Use()
        {
            player.transform.position = placementForPlayer.position;
        }

        public float CurrentSpeed
        {
            get
            {
                return this.currentSpeed;
            }
        }
    }
}