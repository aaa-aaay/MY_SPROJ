using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "1D25BB";
        }

        string deviceId = SystemInfo.deviceUniqueIdentifier;
        var request = new LoginWithCustomIDRequest
        {
            CustomId = deviceId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Device-based login successful! Player ID: " + result.PlayFabId);

        PlayerSelectionManager.Instance.LoadPlayerProgress();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Device-based login failed.");
        Debug.LogError(error.GenerateErrorReport());
    }
}
