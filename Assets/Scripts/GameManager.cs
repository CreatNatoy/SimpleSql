using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Button _addCoin; 
    [SerializeField] private Button _removeCoin;

    private int _currentCoin;
    private string _currentLogin;

    private void Start() {
        _addCoin.onClick.AddListener(AddCoin);
        _removeCoin.onClick.AddListener(RemoveCoin);
    }

    private IEnumerator SaveCoin() {
        var form = new WWWForm();
        form.AddField("login", _currentLogin);
        form.AddField("new_data", _currentCoin);

        UnityWebRequest www = UnityWebRequest.Post("Link", form);
        yield return www.SendWebRequest();
        
        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log($"Error: " + www.error);
        else {
            Debug.Log("successful connection php");
            Debug.Log($"From database: {www.downloadHandler.text}");
        }
    }

    private void RemoveCoin() {
        _currentCoin--;
        UpdateText();
        StartCoroutine(SaveCoin());
    }

    private void AddCoin() {
        _currentCoin++;
        UpdateText(); 
        StartCoroutine(SaveCoin());
    }

    public void Initial(int coins, string login) {
        _currentLogin = login;
        _currentCoin = coins;
        UpdateText();
    }

    private void UpdateText() {
        _coinsText.text = $"Coins: {_currentCoin}";
    }
}
