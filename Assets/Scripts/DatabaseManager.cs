using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    private void Start() {
        StartCoroutine(Send());
    }
    
    private IEnumerator Send() {
        WWWForm form = new WWWForm();
        form.AddField("WelcomeMsg", "Hello, Denis");
        var www =  new WWW("link/index.php", form);
        yield return www;

        if (www.error != null) {
            Debug.Log($"Error: {www.error}" );
            yield break;
        }
        
        Debug.Log($"Answer server { www.text}");
    }

    private IEnumerator SendUnityWeb() {
        WWWForm form = new WWWForm();
        form.AddField("WelcomeMsg", "Hello, Denis");
        var www =  UnityWebRequest.Post("link/index.php", form);
        yield return www.SendWebRequest();

        if (www.error != null) {
            Debug.Log($"Error: {www.error}" );
            yield break;
        }
        
        Debug.Log($"Answer server { www.downloadHandler.text}");
    }
}
