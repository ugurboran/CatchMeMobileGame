using System.Collections;
using System.Collections.Generic;
using Save_System;
using Save_System.Cloud;
using Save_System.JSON;
using Save_System.Model;
using Save_System.JSON.Model;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.Purchasing;
using Product = UnityEngine.Purchasing.Product;
using UnityEngine.UI;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour
{
    //[SerializeField] private CloudSaveSystem cloudSaveSystem;
    [SerializeField] private CloudSave cloudSave;
    private const string carBuy = "com.xnggames.catchme.carteam";
    private const string skateBuy = "com.xnggames.catchme.skateteam";
    private const string taxiBuy = "com.xnggames.catchme.taxiteam";
    private const string goldPack100 = "com.xnggames.catchme.goldpack100";
	private const string goldPack600 = "com.xnggames.catchme.goldpack600";
	private const string goldPack1000 = "com.xnggames.catchme.goldpack1000";
    public Button StartButton;

    private readonly IDataService dataService = new JsonDataService();
    // Start is called before the first frame update

    public void OnPurchaseComplete(Product product)
    {

        // kayıtlı altın yoksa yenisini oluşturuyor.
        var userInfo = dataService.GetCurrentUserData();
        UserDataRoot userDataRoot = userInfo.Item1;
        UserData userData = userInfo.Item2;
        int index = userInfo.Item3;



        if (product.definition.id == carBuy)
        {
            //CarNumber.characterNumber = 1;
            //CarNumber.carTaken = true;
            userData.characterData.characterNumber = 1;
            CarNumber.characterNumber = userData.characterData.characterNumber;
            userData.characterData.carTaken = true;
            Debug.Log("Purchase Car DONE!");
        }
        else if (product.definition.id == skateBuy)
        {
            //CarNumber.characterNumber = 2;
            //CarNumber.skateTaken = true;
            userData.characterData.characterNumber = 2;
            CarNumber.characterNumber = userData.characterData.characterNumber;
            userData.characterData.skateTaken = true;

        }
        else if (product.definition.id == taxiBuy)
        {
            //CarNumber.characterNumber = 3;
            //CarNumber.taxiTaken = true;
            userData.characterData.characterNumber = 3;
            CarNumber.characterNumber = userData.characterData.characterNumber;
            userData.characterData.taxiTaken = true;

        }

        // *********************************

        //TO-DO == Commentli kısımlar eklenecek.

        // *********************************************
        
        else if (product.definition.id == goldPack100)
        {
            Debug.Log("100 gold purchased.");
			//MoneyManager.Instance.AddMoney(100);
			//userData.money.moneyAmount += 100;
        }
        else if (product.definition.id == goldPack600)
        {
            Debug.Log("600 gold purchased.");
			//MoneyManager.Instance.AddMoney(600);
			//userData.money.moneyAmount += 600;
        }
        else if (product.definition.id == goldPack1000)
        {
            Debug.Log("1000 gold purchased.");
			//MoneyManager.Instance.AddMoney(1000);
			//userData.money.moneyAmount += 1000;
        }


        // *********************************************

        userDataRoot.userDatas[index] = userData;
        if (dataService.SaveData(userDataRoot, index, false))
        {
            //cloudSaveSystem.OpenSave(true);
            _ = cloudSave.SaveData();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription reason)
    {
        Debug.Log("Your purchase failed! " + reason + product.definition.id);
    }


    public void FakePurchaseCompletedCar(){

        // kayıtlı altın yoksa yenisini oluşturuyor.
        var userInfo = dataService.GetCurrentUserData();
        UserDataRoot userDataRoot = userInfo.Item1;
        UserData userData = userInfo.Item2;
        int index = userInfo.Item3;


        userData.characterData.characterNumber = 1;
        CarNumber.characterNumber = userData.characterData.characterNumber;
        userData.characterData.carTaken = true;
        Debug.Log("Fake Purchase Car DONE!");

        userDataRoot.userDatas[index] = userData;
        if (dataService.SaveData(userDataRoot, index, false))
        {
            //cloudSaveSystem.OpenSave(true);
            _ = cloudSave.SaveData();
        }
    }

    public void FakePurchaseCompletedSkate(){

        // kayıtlı altın yoksa yenisini oluşturuyor.
        var userInfo = dataService.GetCurrentUserData();
        UserDataRoot userDataRoot = userInfo.Item1;
        UserData userData = userInfo.Item2;
        int index = userInfo.Item3;

        userData.characterData.characterNumber = 2;
        CarNumber.characterNumber = userData.characterData.characterNumber;
        userData.characterData.skateTaken = true;

        userDataRoot.userDatas[index] = userData;
        if (dataService.SaveData(userDataRoot, index, false))
        {
            //cloudSaveSystem.OpenSave(true);
            _ = cloudSave.SaveData();
        }
    }
    public void FakePurchaseCompletedTaxi(){

        // kayıtlı altın yoksa yenisini oluşturuyor.
        var userInfo = dataService.GetCurrentUserData();
        UserDataRoot userDataRoot = userInfo.Item1;
        UserData userData = userInfo.Item2;
        int index = userInfo.Item3;

        userData.characterData.characterNumber = 3;
        CarNumber.characterNumber = userData.characterData.characterNumber;
        userData.characterData.taxiTaken = true;

        userDataRoot.userDatas[index] = userData;
        if (dataService.SaveData(userDataRoot, index, false))
        {
            //cloudSaveSystem.OpenSave(true);
            _ = cloudSave.SaveData();
        }
    }
}
