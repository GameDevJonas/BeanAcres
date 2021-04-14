using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static bool inDialogue;

    private float textSpeed;
    public float slowSpeed;
    public float normalSpeed;

    private Queue<string> sentences;
    private Queue<Mood> moods;
    private Dialogue currentDialogue;

    public Animator anim;
    public TextMeshProUGUI nameText, continueButtonText;
    public Transform portraitPosition;
    private Animator portraitAnimator;
    private GameObject portraitClone;

    public TextMeshAnimator animatorText;

    public enum Mood { happy, normal, sad };
    private List<AudioClip> activeVoicePool = new List<AudioClip>();
    public AudioSource voiceSource;

    public bool inTag = false;
    public bool finishedSentence = false;
    public bool fastForwarded = false;
    private string currentSentence;
    private int index;

    public AudioSource popupSource, continueSource, endSource;

    private DialogueTrigger trigger;

    void Start()
    {
        sentences = new Queue<string>();
        moods = new Queue<Mood>();
        textSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMood(Mood mood, Dialogue dialogue)
    {
        switch (mood)
        {
            case Mood.happy:
                activeVoicePool = dialogue.character.happyVoice;
                portraitAnimator.SetTrigger("Happy");
                //Change voice clip list to happy
                break;
            case Mood.normal:
                activeVoicePool = dialogue.character.normalVoice;
                //portrait.sprite = moodSprites[1];
                portraitAnimator.SetTrigger("Normal");
                //Change voice clip list to normal
                break;
            case Mood.sad:
                activeVoicePool = dialogue.character.sadVoice;
                //portrait.sprite = moodSprites[2];
                portraitAnimator.SetTrigger("Sad");
                //Change voice clip list to sad
                break;
        }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger dTrigger)
    {
        inDialogue = true;
        trigger = dTrigger;
        anim.SetTrigger("In");
        if (portraitClone != null)
        {
            Destroy(portraitClone);
        }
        currentDialogue = dialogue;
        //DialogueUI in
        animatorText.BeginAnimation();
        nameText.text = dialogue.character.name;
        portraitClone = Instantiate(dialogue.character.portraitPrefab, portraitPosition.transform);
        //portraitClone.transform.parent = portraitClone.transform;
        portraitAnimator = portraitClone.GetComponent<Animator>();
        //ChangeMood(dialogue.startingMood);
        continueButtonText.text = "Tap to continue";
        animatorText.text = "";
        sentences.Clear();
        moods.Clear();

        foreach (SentenceElements sentenceElement in dialogue.dialogue)
        {
            sentences.Enqueue(sentenceElement.sentence);
            moods.Enqueue(sentenceElement.mood);
        }

        DisplayNextSentence(false);
    }

    public void DisplayNextSentence(bool fromButton)
    {
        if (fromButton) continueButtonText.GetComponent<Animator>().Play("Text_Out");
        if (fastForwarded)
        {
            return;
        }
        if (!finishedSentence && fromButton && !fastForwarded)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence));
            textSpeed = .00000000000001f;
            fastForwarded = true;
            //finishedSentence = true;
            return;
        }
        finishedSentence = false;
        textSpeed = normalSpeed;

        if (fromButton && sentences.Count > 0)
        {
            continueSource.Play();
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        } //If no sentences left, end dialogue

        currentSentence = sentences.Dequeue();
        animatorText.text = currentSentence;
        Mood mood = moods.Dequeue();
        ChangeMood(mood, currentDialogue);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    public void CheckForTags(string sentence, char character, int characterIndex)
    {
        if (character == '<')
        {
            inTag = true;
        }

        if (character == '|' && !finishedSentence)
        {
            if (textSpeed == slowSpeed)
            {
                textSpeed = normalSpeed;
            }
            else
            {
                textSpeed = slowSpeed;
            }
        }

        if (characterIndex > 0 && sentence[characterIndex - 1] == '>')
        {
            Debug.Log("Out of tag");
            inTag = false;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        inTag = false;
        index = 0;
        animatorText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            CheckForTags(sentence, letter, index);
            animatorText.text += letter;
            animatorText.UpdateText();
            voiceSource.clip = activeVoicePool[Random.Range(0, activeVoicePool.Count)];
            voiceSource.pitch = Random.Range(.8f, 1.2f);

            if (inTag)
            {
                yield return new WaitForSeconds(.00000001f);
            }
            else
            {
                voiceSource.Play();
                yield return new WaitForSeconds(textSpeed);
            }

            index++;
        }
        if (sentences.Count == 0)
        {
            continueButtonText.text = "Tap to end";
        } //If last sentence is next, change continue to end
        continueButtonText.GetComponent<Animator>().Play("Text_In");
        //voiceSource.Stop();
        finishedSentence = true;
        fastForwarded = false;
    }

    void EndDialogue()
    {
        //DialogueUI out
        StopAllCoroutines();
        anim.SetTrigger("Out");
        currentDialogue = null;
        animatorText.text = " ";
        portraitAnimator.SetTrigger("Out");
        Destroy(portraitClone, 2f);
        inDialogue = false;
        trigger.finishedDialogueEvents.Invoke();
    }
}
