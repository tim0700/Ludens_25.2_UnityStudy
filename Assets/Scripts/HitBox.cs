using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
            if(Player.instance != null && Player.instance.IsAttack())
                collision.gameObject.GetComponent<MonsterA>().Damaged(Player.instance.GetDamage());
    }
}
