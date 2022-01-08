using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    enum CharacterStates
    {
        AtExitDoor,
        AtTheCounter,
        AtHidenDoor,
        AtPlace,
    }

    public AnimationClip TakePlace;
    public AnimationClip GoUp;
    public AnimationClip RaidExit;

    private CharacterStates characterState = CharacterStates.AtExitDoor;
    private Animation _anim;

    private GameObject _emote_Neutral;
    private GameObject _emote_Happy;
    private GameObject _emote_Sad;

    private GameObject _currentEmotion;


    TimeSpan _emotionDelay = TimeSpan.FromSeconds(5);
    System.Diagnostics.Stopwatch emotionTimer;

    private Queue<Action> jobs = new Queue<Action>();


    #region thread magic
    private int _threadId = 0;
    bool InvokeRequired { get => _threadId != System.Threading.Thread.CurrentThread.ManagedThreadId; }
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        _threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        #region Init Animation
        _anim = gameObject.GetComponent<Animation>();

        var helper = gameObject.GetComponentInParent(typeof(GeneralAnimationHelper), false) as GeneralAnimationHelper;
        if (helper?.Entry != null)
            _anim.AddClip(helper?.Entry, "Entry");

        if (helper?.Accepted != null)
            _anim.AddClip(helper?.Accepted, "Accepted");

        if (helper?.Rejected != null)
            _anim.AddClip(helper?.Rejected, "Rejected");

        if (helper?.Exit != null)
            _anim.AddClip(helper?.Exit, "Exit");

        if (TakePlace != null)
            _anim.AddClip(TakePlace, "TakePlace");

        if (GoUp != null)
            _anim.AddClip(GoUp, "GoUp");

        if (RaidExit != null)
            _anim.AddClip(RaidExit, "RaidExit");
        #endregion

        #region Init Emotions
        emotionTimer = new System.Diagnostics.Stopwatch();
        
        //ME_Customer_F1_Emot1
        //ME_Customer_F1_Emot2
        //ME_Customer_F1_Emot3
        foreach (Transform child in transform)
        {
            if (child.name.EndsWith("Emot1"))
               _emote_Neutral = child.gameObject;
            if (child.name.EndsWith("Emot2"))
                _emote_Happy = child.gameObject;
            if (child.name.EndsWith("Emot3"))
                _emote_Sad = child.gameObject;
        }

        _emote_Neutral?.SetActive(false);
        _emote_Happy?.SetActive(false);
        _emote_Sad?.SetActive(false);
        #endregion
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (emotionTimer == null)
            return;

        if (emotionTimer.IsRunning && emotionTimer.Elapsed > _emotionDelay)
        {
            emotionTimer.Stop();
            _currentEmotion.SetActive(false);
        }

        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }

    void AddJob(Action newJob)
    {
        jobs.Enqueue(newJob);
    }
    void PlayAction(string action, bool enqueue = false)
    {
        if (InvokeRequired)
        {
            AddJob(() => PlayAction(action, enqueue));
            return;
        }

        if (enqueue)
            _anim.PlayQueued(action);
        else
            _anim.Play(action);
    }
    #region monving animations
    public void Anim_EnterToShop()
    {
        PlayAction("Entry", true);
    }

    public void Anim_Accepted()
    {
        PlayAction("Accepted", true);
        PlayAction("TakePlace", true);
    }

    public void Anim_Rejected()
    {
        PlayAction("Rejected", true);
    }

    public void Anim_Exit()
    {
        PlayAction("GoUp", true);
        PlayAction("Exit", true);
    }

    public void Anim_RaidExit()
    {
        PlayAction("RaidExit", true);
    }

    #endregion

    #region emotions
    public void Emotion_Neutral()
    {
        setEmotion(_emote_Neutral);
    }
    public void Emotion_Happy()
    {
        setEmotion(_emote_Happy);
    }
    public void Emotion_Sad()
    {
        setEmotion(_emote_Sad);
    }

    private void setEmotion(GameObject newEmotion)
    {
        if (newEmotion == null) // nothing to set
            return;

        if (InvokeRequired)
        {
            AddJob(() => setEmotion(newEmotion));
            return;
        }

        // disable old emotion if it still shown
        if (_currentEmotion != null && _currentEmotion.activeInHierarchy)
            _currentEmotion.SetActive(false);

        _currentEmotion = newEmotion;
        _currentEmotion.SetActive(true);
        emotionTimer.Restart();
    }
    #endregion

    public void ManualTrigger()
    {
        //Debug.Log("in Anim_Enter");
        switch (characterState)
        {
            case CharacterStates.AtExitDoor:
                Anim_EnterToShop();
                characterState = CharacterStates.AtTheCounter;
                break;
            case CharacterStates.AtTheCounter:
                Anim_Accepted();
                characterState = CharacterStates.AtPlace;
                break;
//            case CharacterStates.AtHidenDoor:
//                break;
            case CharacterStates.AtPlace:
                Anim_RaidExit();
                characterState = CharacterStates.AtExitDoor;
                break;
        }

        
        // Get a list of the animation clips
        //Animator anim = GetComponent<Animator>();
        //AnimationClip[] animationClips = anim.runtimeAnimatorController.animationClips;
        //AnimationClip[] animationClips = anim. .animationClips;
        //
        //// Iterate over the clips and gather their information
        //foreach (AnimationClip animClip in animationClips)
        //{
        //    Debug.Log(animClip.name + ": " + animClip.length);
        //}

        //_anim.AddClip()

        int n = _anim.GetClipCount();
    }
}
