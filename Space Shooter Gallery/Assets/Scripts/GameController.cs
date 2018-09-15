using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    Image target;

    Image playerEnergy;

    Animator playerEnergyAnimator;

    public GameObject gameOver;

    float animationTime;

    float timeElapsed;

    public Dictionary<string, Sprite> enemies;

    Player player;

    Sprite[] playerSprites;

    string currentTarget;

    int timeRemaining;

    float currentTime;

    int score;

    Text timeUI;

    Text scoreUI;

    Text hitUI;

    string[] enemyNames = { "Ghost", "Rhino", "Bat", "Skeleton", "Slime", "CycloBat" };

    void Start()
    {
        gameOver.SetActive(false);
        target = GameObject.Find("Target") != null ? GameObject.Find("Target").GetComponent<Image>() : null;
        timeUI = GameObject.Find("Time") != null ? GameObject.Find("Time").GetComponent<Text>() : null;
        scoreUI = GameObject.Find("Score") != null ? GameObject.Find("Score").GetComponent<Text>() : null;
        hitUI = GameObject.Find("HitState") != null ? GameObject.Find("HitState").GetComponent<Text>() : null;
        timeElapsed = 0;
        score = 0;
        timeRemaining = 60;
        currentTime = 0;
        animationTime = 1.617f;
        SetupPlayer();
        SetupEnemies();
        UpdateTarget(null);
    }

    void Update()
    {
        // Check if times up
        if (timeRemaining > 0) {
            currentTime += Time.deltaTime;
            if (currentTime >= 1)
            {
                timeRemaining--;
                currentTime = 0;
            }

            if (timeUI != null)
            {
                timeUI.text = "Time : " + timeRemaining;
            }
        } else {
            //Game Over
            EndGame();
        }

        if (IsPlayerHitAnimationPLaying())
            timeElapsed += Time.deltaTime;

        if (timeElapsed >= animationTime)
        {
            StopPlayerHitAnimation();
        }
    }

    void SetupPlayer()
    {
        playerEnergy = GameObject.Find("Energy") != null ? GameObject.Find("Energy").GetComponent<Image>() : null;
        playerEnergyAnimator = GameObject.Find("Energy") != null ? GameObject.Find("Energy").GetComponent<Animator>() : null;
        player = GameObject.Find("Player") != null ? GameObject.Find("Player").GetComponent<Player>() : null;
        playerSprites = Resources.LoadAll<Sprite>("meter");
    }

    void SetupEnemies()
    {
        enemies = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Ghost");
        if (sprites != null)
        {
            enemies.Add(enemyNames[0], sprites[0]);
        }
        sprites = Resources.LoadAll<Sprite>("Enemies");
        if (sprites != null)
        {
            enemies.Add(enemyNames[1], sprites[3]);
        }
        sprites = Resources.LoadAll<Sprite>("Bat");

        if (sprites != null)
        {
            enemies.Add(enemyNames[2], sprites[1]);
        }
        sprites = Resources.LoadAll<Sprite>("Skeleton");
        if (sprites != null)
        {
            enemies.Add(enemyNames[3], sprites[1]);
        }
        sprites = Resources.LoadAll<Sprite>("Enemies");
        if (sprites != null)
        {
            enemies.Add(enemyNames[4], sprites[1]);
        }
        sprites = Resources.LoadAll<Sprite>("Enemies");
        if (sprites != null)
        {
            enemies.Add(enemyNames[5], sprites[7]);
        }

    }

    public void UpdateTarget(GameObject enemy)
    {
        int randomPosition = -1;
        if (enemy == null) {
            randomPosition = Random.Range(0, enemyNames.Length);
        } else {
            if (enemy.name.Equals(currentTarget)) {
                do
                {
                    randomPosition = Random.Range(0, enemyNames.Length);
                } while (enemyNames[randomPosition] == currentTarget);

                UpdateScore();
                ShowHitState(true);
                UpdateTimeRemaining(true);
            } else {
                if (player.GetEnergy() > 0)
                {
                    RemoveEnergyPlayer();
                }
                UpdatePlayerUI();
                ShowHitState(false);
                UpdateTimeRemaining(false);
            }
        }
        if (randomPosition > -1) {
            currentTarget = enemyNames[randomPosition];
            target.sprite = enemies[currentTarget];
        }
    }

    void RemoveEnergyPlayer() {
        player.SetEnergy(player.GetEnergy() - 1);
        if (player.GetEnergy() <= 0) {
            playerEnergy.enabled = false;
            EndGame();
        }
    }

    void PlayPlayerHitAnimation () { 
        if (playerEnergyAnimator != null) {
            timeElapsed = 0;
            playerEnergyAnimator.SetBool("hit", true);
        }
    }

    void StopPlayerHitAnimation()
    {
        playerEnergyAnimator.SetBool("hit", false);
    }

    bool IsPlayerHitAnimationPLaying()
    {
        return playerEnergyAnimator.GetBool("hit");
    }

    void UpdateTimeRemaining(bool addTime) {
        if (addTime) {
            timeRemaining++;
        } else {
            timeRemaining--;
        }
    }

    void ShowHitState (bool state) {
        if (hitUI != null) {
            if (state)
            {
                // Good Hit
                hitUI.text = "Good";
                hitUI.color = new Color(46.0f / 255.0f, 204.0f / 255.0f, 113.0f / 255.0f);
            }
            else
            {
                // Bad Hit
                hitUI.text = "Wrong";
                hitUI.color = new Color(231.0f / 255.0f, 76.0f / 255.0f, 60.0f / 255.0f);
            }
            hitUI.enabled = true;
            Invoke("DisableHitStateText",1);
        }
    }

    void DisableHitStateText () {
        if (hitUI != null) {
            hitUI.enabled = false;
        }
    }

    void UpdatePlayerUI()
    {
        if (player != null) {
            int energy = player.GetEnergy();
            if (playerSprites != null) {
                if (playerEnergy != null && playerSprites.Length >= energy) {
                    if (energy > 0) {
                        playerEnergy.sprite = playerSprites[energy - 1];
                        PlayPlayerHitAnimation();
                    }
                }
            }
        }
    }

    void UpdateScore () {
        score += 10;
        scoreUI.text = "Score : " + score;
    }

    public void EndGame()
    {
        Invoke("ClearGame", 0.3f);
    }

    void ClearGame () {
        player.gameObject.SetActive(false);
        target.gameObject.SetActive(false);
        hitUI.gameObject.SetActive(false);
        scoreUI.gameObject.SetActive(false);
        timeUI.gameObject.SetActive(false);
        GameObject targetText = GameObject.Find("CurrentTarget");
        if (targetText != null) {
            targetText.SetActive(false);
        }
        gameOver.SetActive(true);
        Text gameOverScore = GameObject.Find("Points") != null ? GameObject.Find("Points").GetComponent<Text>() : null;
        if (gameOverScore != null) {
            gameOverScore.text = score + "";
        }
        Cursor.visible = true;
    }
}
