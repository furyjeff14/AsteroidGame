using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private int lives = 3;
    private int minSpeed = 0;
    private int maxSpeed = 0;
    private int score = 10;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void InitializeAsteroid()
    {
        SetAsteroidSize();
        SetRandomRotation();
        AddForce();
    }

    public void SetScriptableSettings(AsteroidScriptable _setting)
    {
        lives = _setting.lives;
        minSpeed = _setting.minSpeed;
        maxSpeed = _setting.maxSpeed;
        score = _setting.score;
    }

    public void InitializeSplitAsteroid(Vector3 _position, Asteroid _asteroid)
    {
        minSpeed = _asteroid.minSpeed;
        maxSpeed = _asteroid.maxSpeed;
        transform.position = _position;
        lives = _asteroid.lives;
        InitializeAsteroid();
    }

    private void SetAsteroidSize()
    {
        int size = lives;
        if(lives > 3)
        {
            size = 3;
        }
        transform.localScale = new Vector3(size,  size, 1);
    }

    private void SetRandomRotation()
    {
        int randRot = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, 0, randRot);
    }

    public void AddForce()
    {
        int speed = Random.Range(minSpeed, maxSpeed);
        myRigidBody.AddForce(transform.up * speed);
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == StringConstants.asteroidTag)
        {
            Vector2 _normal = collision.contacts[0].normal;
            myRigidBody.velocity = Vector2.Reflect(myRigidBody.velocity, _normal);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if(_collider.transform.tag == StringConstants.playerTag)
        {
            _collider.transform.SendMessage("GetHit");
        }

        if (_collider.transform.tag == StringConstants.projectileTag)
        {
            GameObject.Destroy(_collider.gameObject);
            GetHit();
        }

    }

    private void GetHit()
    {
        lives--;
        AsteroidGenerator.Instance.RemoveAsteroid(gameObject);

        if (lives >= 1 && lives < 3)
        {
            AsteroidGenerator.Instance.SplitAsteroid(transform, this);
        } else if(lives == 0)
        {
            PlayerStats.Instance.AddScore(score);
        }
        GameObject.Destroy(gameObject);

    }
}
