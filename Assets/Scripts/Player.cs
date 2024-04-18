using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask wallLayer;
    public GameObject particles;

    public AudioClip dashSound;
    public AudioClip wallHitSound;
    public AudioClip collectCoinSound;
    public AudioClip collectStarSound;

    public RuleTile tile;
    public Tilemap tileMap;

    public string nextLevel;

    Rigidbody2D rb;
    AudioSource source;
    Vector2 input = Vector2.right;
    RaycastHit2D hitCopy;
    Sprite coinSprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

        coinSprite = tile.m_DefaultSprite;
        tile.m_DefaultSprite = null;
        tileMap.RefreshAllTiles();
    }

    private void Update()
    {
        var newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var hit = Physics2D.Raycast(transform.position, input, 0.6f, wallLayer);

        if (hitCopy.collider == null && hit.collider != null)
        {
            Instantiate(particles, transform.position, transform.rotation);
            source.PlayOneShot(wallHitSound);
        }
        if (hitCopy.collider != null && hit.collider == null)
        {
            source.PlayOneShot(dashSound, 0.5f);
        }
        hitCopy = hit;

        if (newInput != Vector2.zero && hit.collider != null)
        {
            input = newInput;
            transform.up = -input;
        }

        rb.velocity = input * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            source.PlayOneShot(collectCoinSound);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Star"))
        {
            source.PlayOneShot(collectStarSound);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void OnDestroy()
    {
        tile.m_DefaultSprite = coinSprite;
    }
}
