using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Save_System;
using Save_System.JSON;
using Save_System.Model;
using Save_System.JSON.Model;

public class CarSelection : MonoBehaviour
{

    private IDataService dataService; // Use the IDataService interface

    private UserData userData;

    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button carBuyButton;
    [SerializeField] private Button skateBuyButton;
    [SerializeField] private Button taxiBuyButton;
    [SerializeField] private Button startButton;

    private int currentCar;
    public int carNumber;

    private void Awake()
    {
        dataService = new JsonDataService(); // Initialize the instance with JsonDataService
        SelectCar(0);
        SelectCar(0);
    }

    private void Start()
    {
        // // Retrieve user data using the IDataService interface
        // var userDataTuple = dataService.GetCurrentUserData();

        // UserDataRoot userDataRoot = userDataTuple.Item1;
        // // userData = userDataTuple.Item2;
        // int userDataIndex = userDataTuple.Item3;

        // // Now you can use the retrieved data as needed
        // currentCar = userData.characterData.characterNumber;

        // // Update button interactivity based on userData
        // // ...

        // // Rest of your Start method
    }


    void Update()
    {
        // TO-DO : Bu kısmı update içinde yazmaktan daha iyi bir çözüm bul!!!
        //AYRICA USER DATA character data kısmında id : guest olması kısmını düzenle.
        var userDataTuple = dataService.GetCurrentUserData();

        UserDataRoot userDataRoot = userDataTuple.Item1;
        userData = userDataTuple.Item2;
        int userDataIndex = userDataTuple.Item3;


        if (currentCar == 0)
        {
            carBuyButton.interactable = false;
            skateBuyButton.interactable = false;
            taxiBuyButton.interactable = false;
            startButton.interactable = true;
        }
        else if (currentCar == 1)
        {
            Debug.Log("1.araç için buton aktivasyonu");
            carBuyButton.interactable = true;
            skateBuyButton.interactable = false;
            taxiBuyButton.interactable = false;

            if (userData.characterData.carTaken == true)
            {
                Debug.Log("1.araç için start aktivasyonu");
                startButton.interactable = true;
                carBuyButton.interactable = false;
            }
            else
            {
                Debug.Log("1.araç için Nostart aktivasyonu");
                startButton.interactable = false;
            }

        }
        else if (currentCar == 2)
        {
            Debug.Log("2.araç için buton aktivasyonu");
            skateBuyButton.interactable = true;
            carBuyButton.interactable = false;
            taxiBuyButton.interactable = false;

            if (userData.characterData.skateTaken == true)
            {
                Debug.Log("2.araç için start aktivasyonu");
                startButton.interactable = true;
                skateBuyButton.interactable = false;
            }
            else
            {
                Debug.Log("2.araç için Nostart aktivasyonu");
                startButton.interactable = false;
            }

        }
        else if (currentCar == 3)
        {
            Debug.Log("3.araç için buton aktivasyonu");
            taxiBuyButton.interactable = true;
            skateBuyButton.interactable = false;
            carBuyButton.interactable = false;

            if (userData.characterData.taxiTaken == true)
            {
                Debug.Log("3.araç için start aktivasyonu");
                startButton.interactable = true;
                taxiBuyButton.interactable = false;
            }
            else
            {
                Debug.Log("3.araç için Nostart aktivasyonu");
                startButton.interactable = false;
            }

        }

    }

    private void SelectCar(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }



    public void ChangeCar(int _change)
    {
        // Retrieve user data using the IDataService interface
        // var userDataTuple = dataService.GetCurrentUserData();

        // UserDataRoot userDataRoot = userDataTuple.Item1;
        // userData = userDataTuple.Item2;
        // int userDataIndex = userDataTuple.Item3;

        // Now you can use the retrieved data as needed
        //currentCar = userData.characterData.characterNumber;

        // Update button interactivity based on userData
        // ...

        // Rest of your Start method

        currentCar += _change;
        SelectCar(currentCar);
        Debug.Log(currentCar);
    }

    public void StartWithCar()
    {
        CarNumber.characterNumber = currentCar;
        SceneManager.LoadScene(1);
    }
}

