using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected EnemyData enemyData;
    public Animator anim { get; private set; }
    public Core core { get; private set; }

    #region Bools
    private bool canMove;
    private bool isFollowing;
    private bool isWandering;
    private bool isIdling;
    #endregion

    #region Smart AI
    [SerializeField] private float attackRange, attackDelay, detectDelay, aiDelay;

    [SerializeField] private List<Detector> detectors;
    [SerializeField] private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField] private Transform randomPosTransform;

    private AIData aiData;
    private ContextSolver contextSolver;
    #endregion


    private float idleStartTime;
    private float wanderStartTime;
    private float wanderTime = 1f;
    private float idleTime = 3f;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        aiData = GetComponent<AIData>();
        contextSolver = GetComponent<ContextSolver>();

        anim = GetComponent<Animator>();
        core = transform.Find("Core").GetComponent<Core>();
    }

    protected virtual void Start()
    {
        canMove = true;
        wanderTime = 2f; // In Seconds

        InvokeRepeating("PerformDetection", 0, detectDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            if (detector.name == "Target" && isWandering)
            {
                return;
            }

            detector.Detect(aiData);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // First check if there is a target
        // If there is a target, then reset all the bools and immediately follow the target
        if (aiData.currentTarget != null && !isWandering)
        {
            if (!isFollowing)
            {
                isFollowing = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0 && !isWandering)
        {
            aiData.currentTarget = aiData.targets[0];
        }

        if (!isFollowing)
        {
            if (Time.time >= idleStartTime + idleTime && !canMove)
            {
                canMove = true;
                isIdling = false;

                anim.SetBool("idle", false);
                anim.SetBool("move", true);

                // Generate random world pos
                Vector2 randomPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                // Set animation bools for movement

                randomPosTransform.position = randomPos;
                aiData.targets = new List<Transform>() { randomPosTransform };
                aiData.currentTarget = aiData.targets[0];
                wanderStartTime = Time.time;

                StartCoroutine(Wander());
                isWandering = true;
            }
            else if (Time.time >= wanderStartTime + wanderTime && canMove)
            {
                isWandering = false;
                canMove = false;
                Idle();
            }
        }
    }

    protected virtual void Idle()
    {
        if (aiData.targets != null)
        {
            aiData.targets.Remove(randomPosTransform);
            aiData.currentTarget = null;
        }
        core.movement.SetVelocityZero();
        // Set animation bools for idle
        anim.SetBool("move", false);
        anim.SetBool("idle", true);
        idleStartTime = Time.time;
        isIdling = true;
    }

    protected virtual IEnumerator Wander()
    {
        if (Time.time >= wanderStartTime + wanderTime)
        {
            yield break;
        }
        core.movement.SetVelocity(enemyData.movementSpeed, contextSolver.GetDirectionToMove(steeringBehaviours, aiData), 1);
        yield return new WaitForSeconds(aiDelay);
        StartCoroutine(Wander());
    }

    protected virtual void Attack()
    {
        anim.SetBool("move", false);
        anim.SetBool("attack", true);
    }

    protected virtual IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            Debug.Log("Stopping");
            isFollowing = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackRange)
            {
                //Attack logic
                core.movement.SetVelocityZero();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                core.movement.SetVelocity(enemyData.movementSpeed, contextSolver.GetDirectionToMove(steeringBehaviours, aiData), 1);
                yield return new WaitForSeconds(aiDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }
    }

    public void Damage(float pAmount, float mAmount)
    {
        Debug.Log(pAmount + " Physical, " + mAmount + " Magic");
    }
}
