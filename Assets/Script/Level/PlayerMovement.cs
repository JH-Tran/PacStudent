using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioClip emptyMove;
    [SerializeField] AudioClip pelletMove;
    [SerializeField] AudioClip wallMove;
    [SerializeField] ParticleSystem dustParticle;

    private Tween activeTween;
    private Animator playerAnimator;
    private AudioSource playerSound;
    private float timeTaken;
    private bool isAudioPlayed = false;

    public bool isLerping = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerSound = gameObject.GetComponent<AudioSource>();
        dustParticle = gameObject.GetComponent<ParticleSystem>();
    }
    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if(activeTween == null)
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
    }
    // Update is called once per frame
    void Update()
    {          
        if (activeTween != null && Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
        {
            timeTaken += Time.deltaTime;
            isLerping = true;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, Mathf.Pow((timeTaken / activeTween.Duration), 3));
            playerAnimator.enabled = true;
            if (activeTween.EndPos.y > activeTween.Target.position.y)
            {
                playerAnimator.SetInteger("aniY", 1);
                playerAnimator.SetInteger("aniX", 0);
            }
            else if (activeTween.EndPos.y < activeTween.Target.position.y)
            {
                playerAnimator.SetInteger("aniY", -1);
                playerAnimator.SetInteger("aniX", 0);
            }
            else if (activeTween.EndPos.x > activeTween.Target.position.x)
            {
                playerAnimator.SetInteger("aniY", 0);
                playerAnimator.SetInteger("aniX", 1);
            }
            else if (activeTween.EndPos.x < activeTween.Target.position.x)
            {
                playerAnimator.SetInteger("aniY", 0);
                playerAnimator.SetInteger("aniX", -1);
            }
        }
        else if (activeTween != null && Vector3.Distance(activeTween.Target.position, activeTween.EndPos) < 0.1f)
        {
            playerAnimator.enabled = false;
            activeTween.Target.position = activeTween.EndPos;
            timeTaken = 0;
            playerSound.Stop();
            isLerping = false;
            activeTween = null;
            if (isAudioPlayed == false)
            {
                playerSound.Play();
            }
            else
            {
                isAudioPlayed = false;
            }
            dustParticle.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pellets")
        {
            playerSound.clip = pelletMove;
            playerSound.Play();
            isAudioPlayed = true;
            StartCoroutine(audioWait());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator audioWait()
    {
        yield return new WaitForSeconds(playerSound.clip.length);
        playerSound.clip = emptyMove;
    }

    public void hitWallAudio()
    {
        playerSound.clip = wallMove;
        playerSound.Play();
        isAudioPlayed = true;
        StartCoroutine(audioWait());
    }

    public bool isRaycastHit(RaycastHit2D ray)
    {
        //Raycast collision with wall
        if (ray.collider != null)
        {
            if (ray.distance <= 1.25f && ray.collider.tag.Equals("Walls"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
