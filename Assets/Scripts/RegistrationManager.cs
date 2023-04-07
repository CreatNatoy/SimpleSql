using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _loginPassword;
    [SerializeField] private Button _registrationButton;

    private void Start() => 
        _registrationButton.onClick.AddListener(RegisterUser);

    private void RegisterUser() {
        string login = _loginInput.text;
        string password = _loginPassword.text;

        StartCoroutine(SendRegistrationRequest(login, password));
    }

    private IEnumerator SendRegistrationRequest(string login, string password) {
        WWWForm form = new WWWForm(); 
        form.AddField("login", login);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("https://serwer1875431.home.pl/DatabasePractice/register.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log($"Error: {www.error}");
        else {
            Debug.Log("successful connection register.php");
            Debug.Log($"From database: {www.downloadHandler.text}");
        }
        
        www.Dispose();
    }
}
