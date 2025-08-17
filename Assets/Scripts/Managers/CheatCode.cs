using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private ProgressSaveData progress;

    [SerializeField] private TMP_InputField checkPointNoInput;
    [SerializeField] private TMP_InputField sceneNoInput;

    public void SaveCheatData()
    {
        progress = new ProgressSaveData();

        // Convert input field text into integers
        int.TryParse(checkPointNoInput.text, out progress.checkPointNo);
        int.TryParse(sceneNoInput.text, out progress.sceneInt);

        string json = JsonUtility.ToJson(progress);

        var request = new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "PlayerProgress", json }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSaveSuccess, OnDataSaveFailure);

        MySceneManager.Instance.GoNextScene(1);
    }

    private void OnDataSaveSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Data saved successfully!");
    }

    private void OnDataSaveFailure(PlayFabError error)
    {
        Debug.LogError("Failed to save data: " + error.GenerateErrorReport());
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
