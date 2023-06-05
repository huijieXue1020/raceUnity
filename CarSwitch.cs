using UnityEngine;
using System.Collections;

public class CarSwitch : MonoBehaviour
{

    public Transform[] Cars;
    public Transform MyCamera;
    public Transform MyCamera2;

    private int changeCar = 0;
    private bool last_change = false;
    private bool last_change_map = false;
    private int index_map = 0;
    private int currentCar = 0;


    public void CurrentCarActive(int current)
    {
        int amount = 0;

        foreach (Transform Car in Cars)
        {
            if (current == amount)
            {
                MyCamera.GetComponent<VehicleCamera>().target = Car;

                MyCamera.GetComponent<VehicleCamera>().Switch = 0;
                MyCamera.GetComponent<VehicleCamera>().cameraSwitchView = Car.GetComponent<VehicleControl>().carSetting.cameraSwitchView;
                MyCamera2.GetComponent<VehicleCamera>().target = Car;

                MyCamera2.GetComponent<VehicleCamera>().Switch = 0;
                MyCamera2.GetComponent<VehicleCamera>().cameraSwitchView = Car.GetComponent<VehicleControl>().carSetting.cameraSwitchView;
                Car.GetComponent<VehicleControl>().activeControl = true;
            }
            else
            {
                Car.GetComponent<VehicleControl>().activeControl = false;
            }

            amount++;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && Input.GetButton("Fire3") && !last_change || Input.GetKeyDown(KeyCode.Q))
        {
            CurrentCarActive(changeCar);
            currentCar = changeCar;
            changeCar ++;
            if (changeCar == 3)
            {
                changeCar = 0;
            }
        }
        if (Input.GetButton("Jump") && Input.GetButton("Fire3"))
        {
            last_change = true;
        }
        else
        {
            last_change = false;
        }

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetButton("Jump") && Input.GetButton("Fire1")) && !last_change_map)
        {
            changeMap();
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButton("Jump") && Input.GetButton("Fire1"))
        {
            last_change_map = true;
        }
        else
        {
            last_change_map = false;
        }


    }

    public float hoverAmount;

    private void changeMap()
    {
        //foreach (Transform Car in Cars)
        //{
        //Car.position = new Vector3(380, 2, 121);
        //"x": 463 "y": 12 "z": 1292{"position":{"x":469.9195556640625,"y":11.80190372467041,"z":1299.7869873046875}
        Cars[currentCar].position = new Vector3(470, 12, 1300);
        //transform.localScale += Vector3.one * hoverAmount;
        //}
    }



}
