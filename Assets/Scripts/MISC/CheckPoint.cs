using PlayFab;
using PlayFab.ClientModels;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] public Transform p1Respawn;
    [SerializeField] public Transform p2Respawn;

    [SerializeField] private int checkPointNo;
    private ProgressSaveData progress;


    private void Start()
    {
        // Convert to JSON
        progress = new ProgressSaveData();

        progress.checkPointNo = checkPointNo;
        progress.sceneInt = SceneManager.GetActiveScene().buildIndex;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player is IDeath dying)
            {
                SavePlayerProgress();
                if (player.playerNo == 1)
                {

                    dying.RespawnPosition = p1Respawn;

                }
                else
                {
                    dying.RespawnPosition = p2Respawn;
                }
            }

            return;

        }




        if (other.gameObject.GetComponent<IDeath>() != null)
        {
            IDeath death = other.gameObject.GetComponent<IDeath>();
                
            death.RespawnPosition = p1Respawn;


        }

    }



    public void SavePlayerProgress()
    {

        string json = JsonUtility.ToJson(progress);
        var request = new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "PlayerProgress", json }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSaveSuccess, OnDataSaveFailure);
    }

    private void OnDataSaveSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Player progress saved successfully to PlayFab.");
    }

    private void OnDataSaveFailure(PlayFabError error)
    {
        Debug.LogError("Failed to save player progress: " + error.GenerateErrorReport());
    }
}
