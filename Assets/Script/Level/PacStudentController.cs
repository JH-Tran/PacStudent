using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] AudioClip emptyMove;
    [SerializeField] AudioClip pelletMove;
    [SerializeField] AudioClip wallMove;
    [SerializeField] ParticleSystem dustParticle;
    [SerializeField] ParticleSystem wallDustParticle;

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
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, Mathf.Pow(timeTaken / activeTween.Duration, 3));
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

        if (collision.gameObject.tag == "Cherry")
        {
            
        }
    }

    IEnumerator audioWait()
    {
        yield return new WaitForSeconds(playerSound.clip.length);
        playerSound.clip = emptyMove;
    }

    public void hitWallAudio(int angle, bool isVertical)
    {
        Debug.Log(angle);
        if(isVertical)
        {
            wallDustParticle.gameObject.transform.rotation = Quaternion.Euler(angle, 0, 0);
        }
        else
        {
            wallDustParticle.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        wallDustParticle.Play();
        playerSound.clip = wallMove;
        playerSound.Play();
        isAudioPlayed = true;
        StartCoroutine(audioWait());
    }

    public void emptyWalkAudio()
    {
        playerSound.Play();
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
