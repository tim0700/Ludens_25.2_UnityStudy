using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterA : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float offset;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        //플레이어의 위치 - 몬스터의 위치(vector3)를 통해 방향을 구하고
        //방향을 구하면 normalize를 통해 방향을 정규화하고(1로 만들어준다)
        //방향에 따른 각도를 구해야 함.
        //방향에 따라 구한 각도를 transform의 rotation에 적용
        //방향을 토대로  transform의 position을 변경

        if (Player.instance != null)
        {
            direction = (Player.instance.gameObject.transform.position - transform.position).normalized;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + offset);
        
        transform.position += direction * speed;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && Player.instance != null)
        {
            Player.instance.Damaged(direction);    
        }
    }
}
