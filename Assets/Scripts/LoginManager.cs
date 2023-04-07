using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private Button _loginButton;
    [Space] 
    [SerializeField] private GameManager _gameManager;

    private void Start() => 
        _loginButton.onClick.AddListener(LoginUser);

    private void LoginUser() {
        string login = _loginInput.text;
        string password = _passwordInput.text;

        StartCoroutine(SendLoginRequest(login, password));
    }

    private IEnumerator SendLoginRequest(string login, string password) {
        var form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", password);
        
        UnityWebRequest www = UnityWebRequest.Post("https://serwer1875431.home.pl/DatabasePractice/login.php", form);
        yield return www.SendWebRequest();
        
        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log($"Error: " + www.error);
        else {
            Debug.Log("successful connection login.php");
            if (int.TryParse(www.downloadHandler.text, out var coins)) {
                gameObject.SetActive(false);
                _gameManager.Initial(coins, login);
                _gameManager.gameObject.SetActive(true);
            }
            
            Debug.Log($"From database: {www.downloadHandler.text}");
        }
        
        
        www.Dispose();
    }
}
