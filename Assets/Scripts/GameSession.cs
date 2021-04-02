using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // init params
    [Range(0f, 5f)] [SerializeField] float GameTimeScale = 1f;
    [SerializeField] int score = 0;
    [SerializeField] int pointsPerDestroyedBlock = 80;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // This is a simpleton class
    // Awake() is being called before start
    // https://docs.unity3d.com/Manual/ExecutionOrder.html
    private void Awake()
    {
        // make sure to type Object's' for multiple objects
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DestroyOnReset()
    {
        Destroy(gameObject);
    }

    public void UpdateScore()
    {
        score += pointsPerDestroyedBlock;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = GameTimeScale;
        scoreText.text = score.ToString();
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

}
