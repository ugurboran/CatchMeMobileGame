using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterSelect : MonoBehaviour
{
    public GameObject biker;
    public GameObject carDriver;
    public GameObject skater;
    public GameObject taxiDriver;
    //public GameObject heartofCarDriver;
    // Start is called before the first frame update

    void Awake(){
        if(CarNumber.characterNumber == 0){
            biker.SetActive(true);
        }
        else if(CarNumber.characterNumber == 1){
            carDriver.SetActive(true);
            //heartofCarDriver.SetActive(true);
        }
        else if(CarNumber.characterNumber == 2){
            skater.SetActive(true);
        }
        else if(CarNumber.characterNumber == 3){
            taxiDriver.SetActive(true);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
