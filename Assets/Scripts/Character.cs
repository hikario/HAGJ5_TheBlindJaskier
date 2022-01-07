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
    // Start is called before the first frame update
    void Awake()
    {
        _anim = gameObject.GetComponent<Animation>();
                
        var helper = gameObject.GetComponentInParent(typeof(GeneralAnimationHelper), false) as GeneralAnimationHelper;
        if (helper?.Entry != null)
            _anim.AddClip(helper?.Entry, "Entry");

        if (helper?.Accepted != null)
            _anim.AddClip(helper?.Accepted, "Accepted");

        if (helper?.Rejected != null)
            _anim.AddClip(helper?.Rejected, "Rejected");

        if (TakePlace != null)
            _anim.AddClip(TakePlace, "TakePlace");

        if (GoUp != null)
            _anim.AddClip(GoUp, "GoUp");

        if (RaidExit != null)
            _anim.AddClip(RaidExit, "RaidExit");

    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Anim_Enter()
    {
        //Debug.Log("in Anim_Enter");
        switch (characterState)
        {
            case CharacterStates.AtExitDoor:
                _anim.Play("Entry");
                characterState = CharacterStates.AtTheCounter;
                break;
            case CharacterStates.AtTheCounter:
                _anim.Play("Accepted");
                _anim.PlayQueued("TakePlace");
                characterState = CharacterStates.AtPlace;
                break;
//            case CharacterStates.AtHidenDoor:
//                break;
            case CharacterStates.AtPlace:
                _anim.Play("RaidExit");
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
