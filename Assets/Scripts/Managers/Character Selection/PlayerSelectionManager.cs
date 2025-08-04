using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager Instance;

    [Header("Player Prefabs")]
    public GameObject[] characterPrefabs;

    private int[] selectedCharacterIndices = new int[2] { -1, -1 };

    private InputDevice[] playerDevices = new InputDevice[2];

    public int[] SelectedCharacterIndices => selectedCharacterIndices;

    private int loadedSceneNo;
    public int loadedCheckPointNo;
    public bool loadedData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        loadedData = false;

    }


    private void Start()
    {
    }

    public void SetCharacterForPlayer(int playerIndex, int characterIndex, InputDevice device)
    {
        selectedCharacterIndices[playerIndex] = characterIndex;
        playerDevices[playerIndex] = device;

        if (AreBothCharactersSelected())
        {
            Debug.Log("StartGame");
            MySceneManager.Instance.GoNextScene(loadedSceneNo);
        }
    }


    public void DestoryGameObject() { Destroy(gameObject); }


    public bool AreBothCharactersSelected()
    {
        return selectedCharacterIndices[0] != -1 && selectedCharacterIndices[1] != -1;
    }

    public GameObject GetCharacterPrefabForPlayer(int playerIndex)
    {
        return characterPrefabs[selectedCharacterIndices[playerIndex]];
    }


    public InputDevice GetDeviceForPlayer(int playerIndex)
    {
        return playerDevices[playerIndex];
    }










    public void LoadPlayerProgress()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataLoadSuccess, OnDataLoadFailure);
    }

    private void OnDataLoadSuccess(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("PlayerProgress"))
        {
            string json = result.Data["PlayerProgress"].Value;
            ProgressSaveData progress = JsonUtility.FromJson<ProgressSaveData>(json);

            loadedSceneNo = progress.sceneInt;
            if(loadedSceneNo < 2) loadedSceneNo = 2;
            loadedCheckPointNo = progress.checkPointNo;
            Debug.Log("Loaded progress: Level = " + progress.sceneInt + ", Coins = " + progress.checkPointNo);
        }
        else
        {
            loadedSceneNo = 2;
            loadedCheckPointNo = 0;
            Debug.Log("No player progress found.");
        }
    }

    private void OnDataLoadFailure(PlayFabError error)
    {
        loadedSceneNo = 2;
        loadedCheckPointNo = 0;

        Debug.LogError("Failed to load player progress: " + error.GenerateErrorReport());
    }


}
