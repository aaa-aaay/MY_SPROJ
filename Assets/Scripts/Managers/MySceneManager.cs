using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }
    private Animator animator;
    [SerializeField] GameObject animationCanvas;
    private int nextSceneNo;


    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        animationCanvas.SetActive(true);
        animator = GetComponent<Animator>();
    }

    public void GoNextScene(int sceneNo)
    {
        nextSceneNo = sceneNo;
        animator.SetTrigger("StartEndSceneAnimation");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneNo);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0); 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // Example: activate animationCanvas again after new scene
        animationCanvas.SetActive(true);

        // Optional: trigger animation
        animator.SetTrigger("StartSceneAnimation");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
