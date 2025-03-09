using UnityEngine;
using UnityEngine.AI;

public class FoxAI : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    public Transform player; 

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // animator didn't allow to loop. so forced it in code
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Gallop") 
            {
                clip.wrapMode = WrapMode.Loop;
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); 
        }

        float speed = agent.velocity.magnitude; 
        anim.SetFloat("Speed", speed); 

        //loop
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Gallop") && stateInfo.normalizedTime >= 1.0f) 
        {
            anim.Play("Gallop", 0, 0f);
        }
    }
}
