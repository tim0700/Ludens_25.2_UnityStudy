using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float invTime;
    [SerializeField] private int attackOrder;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isAttacking;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject HandL;
    [SerializeField] private GameObject HandR;

    private Coroutine resetCoroutine;
    private Coroutine attackCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }

    private void FixedUpdate()
    {
        //if (x != 0 || y != 0)
        //{
        //   rigid.velocity = new Vector2(x , y) * speed;
        //}
        if(isInvincible)
        {
            return;
        }

        if (x != 0)
        {
            rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
        }

        if (y != 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, y * speed);
        }
    }

    public void Damaged(Vector3 dir)
    {
        if (isInvincible)
        {
            return;
        }
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        HandL.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        HandR.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);

        isInvincible = true;

        if(resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
        resetCoroutine = StartCoroutine(ResetInvincible());
    }

    private IEnumerator ResetInvincible()
    {
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        HandL.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        HandR.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private bool IsAttacking()
    {
        return isAttacking; 
    }

    private Vector3 RotationToVector2D(Vector3 euler)
    {
        float rad;
        if(euler.z < 0)
            rad = (-euler.z) * Mathf.Deg2Rad;
        else
            rad = (360 - euler.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0f);
    }

    private void attack()
    {
        if(isAttacking)
        {
            return;
        }

        Vector3 rot = transform.rotation.eulerAngles;
        Vector3 curDir = RotationToVector2D(rot);
        curDir = curDir.normalized * 2;

        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        attackCoroutine = StartCoroutine(AttackMotion(curDir, attackOrder % 2));

        attackOrder++;
    }

    private IEnumerator AttackMotion(Vector3 curDir, int order)
    {
        isAttacking = true;
        float t = 0f;
        float punchTime = 0.2f;

        GameObject moveObj;
        Vector3 forwardPos;
        Vector3 originalPos;

        if(order == 0)
            moveObj = HandL;
        else
            moveObj = HandR;

        originalPos = moveObj.transform.localPosition;
        forwardPos = transform.position + curDir - moveObj.transform.position;

        while(t < punchTime)
        {
            t += Time.deltaTime;
            moveObj.transform.position += forwardPos * (Time.deltaTime / punchTime);
            
            if (t > punchTime)
                moveObj.transform.position -= forwardPos * ((t - punchTime) / punchTime);
            yield return null;
        }
        
        moveObj.transform.localPosition = originalPos;

        isAttacking = false;
    }
}