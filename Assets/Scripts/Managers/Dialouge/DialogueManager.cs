using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private Queue<string> sentences;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text sentenceText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;

    [SerializeField] private float typingSpeed = 0.03f;

    private bool haveDialouge;

    private TMP_Text defaultNameText, defaultSentanceText;
    private GameObject defaultCanvas;
    private Image defaultImage;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }

    private void Start()
    {

        canvas.SetActive(false);
        if(image != null)  image.enabled = false;

        defaultNameText = nameText;
        defaultSentanceText = sentenceText;
        defaultCanvas = canvas;
        defaultImage = image;
    }

    public void StartDialogue(Dialogue dialouge, string audioName = "")
    {
        SetDisplayFields(true);
        sentences.Clear();
        canvas.SetActive(true);
        animator.SetBool("isOpen", true);
        haveDialouge = true;
        nameText.text = dialouge.name;

        if(image != null && dialouge.icon != null)
        {
            image.enabled = true;
            image.sprite = dialouge.icon;
        }


        foreach (string sentence in dialouge.sentences) { 
            sentences.Enqueue(sentence);
        
        
        
        }

        if (audioName.Length > 0)
        AudioManager.instance.PlaySFX(audioName, PlayerManager.Instance.GetPlayer1().gameObject.transform.position);

        DisplayNextSentence();
    }

    public void DisplayStoryDialouge(Dialogue dialouge)
    {
        sentences.Clear();
        canvas.SetActive(true);
        haveDialouge = true;

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);

        }
        DisplayNextSentence(true);
    }

    public void DisplayNextSentence(bool haveSoundEffect = false, string audioText = "")
    {

        AudioManager.instance.StopSounds(false,2);
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        
        
        }



        string sentence = sentences.Dequeue();
        StopAllCoroutines();

        if (haveSoundEffect) {

            if (audioText == "")
            {
                StartCoroutine(TypeSentence(sentence, "TypingSFX"));
            }
            else if (audioText.Length > 0)
            {
                AudioManager.instance.PlaySFX(audioText);
                StartCoroutine(TypeSentence(sentence));
            }


        }

        else StartCoroutine(TypeSentence(sentence) );
    }

    IEnumerator TypeSentence(string sentence, string audioText = "")
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;


               
            // Play typing/talk sound
            if (!char.IsWhiteSpace(letter) && audioText.Length > 0)
            {
                AudioManager.instance.PlaySFX(audioText, PlayerManager.Instance.GetPlayer1().gameObject.transform.position);
            }

            yield return new WaitForSeconds(typingSpeed);


        }
    }

    void EndDialogue()
    {
        haveDialouge = false;
        if (image != null )image.enabled = false;
        canvas.SetActive(false);
        animator.SetBool("isOpen", false);
        SetDisplayFields(true);

    }
    public bool StillHaveDialogue()
    {
        return haveDialouge;
    }

    public void SetDisplayFields(bool useDefault, TMP_Text nameText = null, TMP_Text sentenceText = null, GameObject canvas = null, Image image = null)
    {
        if (useDefault)
        {
            this.nameText = defaultNameText;
            this.sentenceText = defaultSentanceText;
            this.canvas = defaultCanvas;
            this.image = defaultImage;
        }
        else
        {
            this.nameText = nameText;
            this.sentenceText = sentenceText;
            this.canvas = canvas;
            this.image = image;
        }
    }

}
