using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Tween activeTween;
    private AudioSource playerSound;
    private Animator playerAnimator;
    [SerializeField] AudioClip emptyMove;
    private float timeTaken;

    // Start is called before the first frame update
    void Start()
    {
        playerSound = gameObject.GetComponent<AudioSource>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if(activeTween == null)
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
    }

    // Update is called once per frame
    void Update()
    {
        if(activeTween != null && !playerSound.isPlaying)
        {
            playerSound.clip = emptyMove;
            playerSound.Play();
        }
            
        if (activeTween != null && Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
        {
            timeTaken += Time.deltaTime;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, Mathf.Pow((timeTaken / activeTween.Duration), 3));
            if(activeTween.EndPos.y > activeTween.Target.position.y)
            {
                playerAnimator.Play("PlayerUp");
            }
            else if (activeTween.EndPos.y < activeTween.Target.position.y)
            {
                playerAnimator.Play("PlayerDown");
            }
            else if (activeTween.EndPos.x > activeTween.Target.position.x)
            {
                playerAnimator.Play("PlayerRight");
            }
            else if (activeTween.EndPos.x < activeTween.Target.position.x)
            {
                playerAnimator.Play("PlayerLeft");
            }
        }
        else if (activeTween != null && Vector3.Distance(activeTween.Target.position, activeTween.EndPos) < 0.1f)
        {
            activeTween.Target.position = activeTween.EndPos;
            timeTaken = 0;
            playerSound.Stop();
            activeTween = null;
        }
    }
}
