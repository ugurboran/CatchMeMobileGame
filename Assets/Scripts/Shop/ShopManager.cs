using System;
using Save_System;
using Save_System.Cloud;
using Save_System.JSON;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
	public static ShopManager Instance { get; private set; }
	//[SerializeField] private CloudSaveSystem cloudSaveSystem;
	[SerializeField] private CloudSave cloudSave;
	private readonly IDataService dataService = new JsonDataService();

	public TMP_Text textGoldCoin;
	public TMP_Text textGoldCoinLobby;
	public TMP_Text textExtraHealthPrice;
	public TMP_Text textNoZombiesPrice;
	public TMP_Text textInfiniteAmmoPrice;
	
	public int goldCoinValue;
	public int extraHealthPrice;
	public int noZombiesPrice;
	public int infiniteAmmoPrice;

	private void Awake()
	{
		Instance = this;
	}
	
	private void Start()
	{
		goldCoinValue = int.Parse(textGoldCoin.text);
		extraHealthPrice = int.Parse(textExtraHealthPrice.text);
		noZombiesPrice = int.Parse(textNoZombiesPrice.text);
		infiniteAmmoPrice = int.Parse(textInfiniteAmmoPrice.text);
	}
	
	public void OnGoldChanged(int purchasedValue)
	{
		goldCoinValue += purchasedValue;
		textGoldCoin.text = goldCoinValue.ToString();
		textGoldCoinLobby.text = goldCoinValue.ToString();
	}

	// public void BuyExtraHealth()
	// {
	// 	if (goldCoinValue >= extraHealthPrice)
	// 	{
	// 		SoundManager.Instance.CoinSoundOn();
	// 		// Map.Objects.PowerUps.extraHealth = 1;
	// 		var (userDataRoot, userData, index) = dataService.GetCurrentUserData();
	// 		userData.powerUps.extraLifeQuantity += 1;
	// 		userData.gold.goldQuantity -= extraHealthPrice;
	// 		//PowerUpManager.Instance.ControlPowerUps();
	// 		PowerUpManager.Instance.buttonExtraHealthGameObject.SetActive(true);
	// 		userDataRoot.userDatas[index] = userData;
	// 		if (dataService.SaveData(userDataRoot, index, false))
	// 		{
	// 			PowerUpManager.Instance.PowerUpExtraHealthCountText.text = userData.powerUps.extraLifeQuantity.ToString();
	// 			goldCoinValue -= extraHealthPrice;
	// 			textGoldCoin.text = goldCoinValue.ToString();
	// 			textGoldCoinLobby.text = goldCoinValue.ToString();
	// 			//cloudSaveSystem.OpenSave(true);

	// 			Debug.Log($"json save completed!");

	// 			Debug.Log($"userData: {userData}");

	// 			Debug.Log($"index: {index}");
	// 			_ = cloudSave.SaveData();
	// 			Debug.Log("json save completed 2!");
	// 		}
	// 	}
	// 	else
	// 	{
	// 		SoundManager.Instance.NegativeSoundOn();
	// 	}
	// }

	// public void BuyNoZombies()
	// {
	// 	if (goldCoinValue >= noZombiesPrice)
	// 	{
	// 		SoundManager.Instance.CoinSoundOn();
	// 		// Map.Objects.PowerUps.noZombie = true;
	// 		var (userDataRoot, userData, index) = dataService.GetCurrentUserData();
	// 		userData.powerUps.noZombieQuantity += 1;
	// 		userData.gold.goldQuantity -= noZombiesPrice;
	// 		// PowerUpManager.Instance.ControlPowerUps();
	// 		PowerUpManager.Instance.buttonNoZombiesGameObject.SetActive(true);
	// 		if (dataService.SaveData(userDataRoot, index, false))
	// 		{
	// 			PowerUpManager.Instance.PowerUpNoZombieCountText.text = userData.powerUps.noZombieQuantity.ToString();
	// 			goldCoinValue -= noZombiesPrice;
	// 			textGoldCoin.text = goldCoinValue.ToString();
	// 			textGoldCoinLobby.text = goldCoinValue.ToString();
	// 			//cloudSaveSystem.OpenSave(true);

	// 			Debug.Log($"json save completed!");
	// 			_ = cloudSave.SaveData();
	// 		}
	// 	}
	// 	else
	// 	{
	// 		SoundManager.Instance.NegativeSoundOn();
	// 	}
	// }

	// public void BuyInfiniteAmmo()
	// {
	// 	if (goldCoinValue >= infiniteAmmoPrice)
	// 	{
	// 		SoundManager.Instance.CoinSoundOn();
	// 		// Map.Objects.PowerUps.infiniteAmmo = true;
	// 		var (userDataRoot, userData, index) = dataService.GetCurrentUserData();
	// 		userData.powerUps.infiniteAmmoQuantity += 1;
	// 		userData.gold.goldQuantity -= infiniteAmmoPrice;
	// 		// PowerUpManager.Instance.ControlPowerUps();
	// 		PowerUpManager.Instance.buttonInfiniteAmmoGameObject.SetActive(true);
	// 		if (dataService.SaveData(userDataRoot, index, false))
	// 		{
	// 			PowerUpManager.Instance.PowerUpAmmoCountText.text = userData.powerUps.infiniteAmmoQuantity.ToString();
	// 			goldCoinValue -= infiniteAmmoPrice;
	// 			textGoldCoin.text = goldCoinValue.ToString();
	// 			textGoldCoinLobby.text = goldCoinValue.ToString();
	// 			//cloudSaveSystem.OpenSave(true);

	// 			Debug.Log($"json save completed!");
	// 			_ = cloudSave.SaveData();
	// 		}
	// 	}
	// 	else
	// 	{
	// 		SoundManager.Instance.NegativeSoundOn();
	// 	}
	// }
}