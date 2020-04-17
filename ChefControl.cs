using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefControl : MonoBehaviour
{
    SpriteRenderer spr;

    public float speed;
    public Chef info;

    public Vector2 PrePosition;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        
        StartCoroutine(Move());
        StartCoroutine(EarnMoneyAuto());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EarnMoneyAuto()
    {
        while (true)
        {
            GameManager.money += 5 + 1 * info.level;
            Debug.Log(GameManager.money +", ");

            yield return new WaitForSeconds(1.0f);
        }
    }
    IEnumerator Move()
    {
        while(true)
        {
            float x = transform.position.x + Random.Range(-0.4f, 0.4f);
            float y = transform.position.y + Random.Range(-0.4f, 0.4f);

            Vector2 target = new Vector2(x, y);
            //target = CheckTarget(target);

            PrePosition = transform.position;

            while(Vector2.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed);
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
    Vector2 CheckTarget(Vector2 currentTarget)
    {
        Vector2 temp = currentTarget;

        if (currentTarget.x < GameManager.gm.limitP1.x)
        {
            temp = new Vector2(currentTarget.x + 0.4f, temp.y);
        }
        else if (currentTarget.x > GameManager.gm.limitP2.x)
        {
            temp = new Vector2(currentTarget.x - 0.4f, temp.y);
        }

        if (currentTarget.y > GameManager.gm.limitP1.y)
        {
            temp = new Vector2(temp.x, currentTarget.y - 0.4f);
        }
        else if (currentTarget.y < GameManager.gm.limitP2.y)
        {
            temp = new Vector2(temp.x, currentTarget.y + 0.4f);
        }

        return temp;
    }

    void SetInfo()
    {
        info.level = 1;
        info.name = GameManager.FirstName + GameManager.LastName;

        info.knife = Random.Range(30, 60);
        info.pan = Random.Range(30, 60);
        info.wash = Random.Range(30, 60);

        info.salary = Random.Range(500, 1000);

        int r = Random.Range(0, 2);
        info.gender = (Gender)r;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == true)
        {
            Collider2D col1 = GetComponent<Collider2D>();
            Collider2D col2 = collision.collider;
            Physics2D.IgnoreCollision(col1, col2);
        }
    }

    void SpriteChange()
    {
        Vector2 abs = (Vector2)transform.position - PrePosition;

        //x값으로 움직인 절대값 > y값으로 움직인 절대값
        if (Mathf.Abs(abs.x) > Mathf.Abs(abs.y))
        {
            //왼쪽 또는 오른쪽
            
            if (transform.position.x > PrePosition.x)
            {
                spr.flipX = false;
            }
            else if (transform.position.x < PrePosition.x)
            {
                spr.flipX = true;
            }
        }
    }
}

public enum Gender
{
    Female = 0,
    Male = 1
}

[System.Serializable]
public class Chef
{
    public string name;
    public Gender gender;

    public int level;

    public float knife;
    public float pan;
    public float wash;

    public long salary;
}
