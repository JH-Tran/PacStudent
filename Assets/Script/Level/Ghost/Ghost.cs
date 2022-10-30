using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private PowerPillsManager powerPillsManager;

    private Animator ghostAnimator;
    private PowerPillsManager powerPillManager;
    private bool ghostDied;
    private float maxDeathTimer = 5;
    private float ghostDeathTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        powerPillManager = GameObject.Find("HUD").GetComponent<PowerPillsManager>();
        ghostAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostDied == false)
        {
            if (powerPillsManager.getGhostScaredTimer() >= 3)
            {
                ghostAnimator.SetInteger("ghostScared", 10);
            }
            else if (powerPillsManager.getGhostScaredTimer() < 3 && powerPillsManager.getGhostScaredTimer() > 0.01f)
            {
                ghostAnimator.SetInteger("ghostScared", 3);
            }
            else if (powerPillsManager.getGhostScaredTimer() < 0.01f)
            {
                ghostAnimator.SetInteger("ghostScared", 0);
            }
        }
        else
        {
            if (ghostDeathTimer > 0)
            {
                ghostDeathTimer -= Time.deltaTime;
            }
            else
            {
                ghostRecovered();
                ghostDied = false;
            }
            if (powerPillsManager.getGhostScaredTimer() == 0)
                ghostDied = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && (powerPillsManager.getGhostScaredTimer() > 0f) && (ghostDied == false))
        {
            ghostDeath();
            collision.GetComponent<PacStudentController>().addPlayerScore(300);
        }
        else if (collision.tag.Equals("Player") && (powerPillsManager.getGhostScaredTimer() <= 0f) && (ghostDied == false))
        {
            collision.GetComponent<PacStudentLives>().playerLoseLife();
        }
    }

    private void ghostDeath()
    {
        ghostAnimator.SetTrigger("ghostDead");
        ghostAnimator.SetInteger("ghostScared", 4);
        ghostDeathTimer = maxDeathTimer;
        ghostDied = true;
        powerPillManager.playGhostDeathAudio(maxDeathTimer);
    }

    private void ghostRecovered()
    {
        ghostAnimator.SetTrigger("ghostNormal");
    }
}
