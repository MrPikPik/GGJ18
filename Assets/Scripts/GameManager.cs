using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public class Challenge {
        public ResourceType neededResource;
        public int amount;
        public float timeLimit;

        public Challenge() {
            this.neededResource = ResourceType.none;
            this.amount = 0;
            this.timeLimit = 60.0f;
        }
        public Challenge(ResourceType neededResource, int amount) {
            this.neededResource = neededResource;
            this.amount = amount;
            this.timeLimit = 60.0f;
        }
        public Challenge(ResourceType neededResource, int amount, float timeLimit) {
            this.neededResource = neededResource;
            this.amount = amount;
            this.timeLimit = 60.0f;
        }

        public static Challenge GenerateChallenge() {
            Challenge _c = new Challenge();

            _c.neededResource = (ResourceType)Random.Range(1, 5);
            _c.amount = Random.Range(5, 15);
            _c.timeLimit = Mathf.Ceil(Random.Range(0.5f, 1.5f) * _c.amount * 5.0f);

            Debug.Log("NewChallenge:\nGet " + _c.amount.ToString() + " of " + ResourceTypeToString(_c.neededResource) + " in " + _c.timeLimit.ToString() + "seconds.");

            return _c;
        }

        public static string ResourceTypeToString(ResourceType type) {
            switch(type) {
                case ResourceType.Flower:
                    return "flower";
                case ResourceType.FruitBlue:
                    return "blue fruit";
                case ResourceType.FruitGreen:
                    return "green fruit";
                case ResourceType.FruitPink:
                    return "pink fruit";
                case ResourceType.FruitPurple:
                    return "purple fruit";
                case ResourceType.none:
                    return "nothing";
            }
            return null;
        }
    }

    public enum ResourceType {
        none,
        FruitBlue,
        FruitGreen,
        FruitPink,
        FruitPurple,
        Flower
    }

    public static GameManager instance;

    public GameObject player;
    public GameObject altar;
    public bool godmode = false;

    //Inventory & scoring
    public int score = 0;
    public int InvFruitBlue = 0;
    public int InvFruitGreen = 0;
    public int InvFruitPink = 0;
    public int InvFruitPurple = 0;
    public int InvFlower = 0;

    //Challenge stuff
    public float initialWaitTime = 15.0f;
    private Challenge currChallenge;
    public float challengeTimer = 0.0f;
    public int challengesCompleted = 0;
    public float maxDistanceToAltar = 5.0f;

    //UI
    public Text FruitBlueText;
    public Text FruitGreenText;
    public Text FruitPinkText;
    public Text FruitPurpleText;
    public Text FlowerText;
    public Text ChallengeText;
    public Text ScoreText;
    public Text TimerText;

    void Awake () {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        currChallenge = new Challenge(ResourceType.none, -1, initialWaitTime);
	}

    private void Start() {
        currChallenge = new Challenge(ResourceType.none, -1, initialWaitTime);
        currChallenge.timeLimit = 10.0f;
    }
	
	void Update () {
        if(challengeTimer < currChallenge.timeLimit) {
            challengeTimer += Time.deltaTime;
        } else {
            if(CheckChallengeFullfulled()) {              
                challengeTimer = 0.0f;
                challengesCompleted++;
                AwardScore(1000 * challengesCompleted);
                currChallenge = Challenge.GenerateChallenge();
                StartCoroutine("Dabbing");
            } else {
                //GameOver

                //TEMP FIX: Start new challenge, kill combo and give score penalty!
                challengeTimer = 0.0f;
                challengesCompleted = 0;
                AwardScore(-1000);
                currChallenge = Challenge.GenerateChallenge();
                //Debug.Log("Challenge failed!");
            }
        }

        if(godmode) {
            InvFruitBlue = 99;
            InvFruitGreen = 99;
            InvFruitPink = 99;
            InvFruitPurple = 99;
            InvFlower = 99;
        }
	}

    void OnGUI() {
        FruitBlueText.text = InvFruitBlue.ToString() + "x";
        FruitGreenText.text = InvFruitGreen.ToString() + "x";
        FruitPinkText.text = InvFruitPink.ToString() + "x";
        FruitPurpleText.text = InvFruitPurple.ToString() + "x";
        FlowerText.text = InvFlower.ToString() + "x";

        if(currChallenge.neededResource != ResourceType.none) {
            ChallengeText.text = "Challenge: Get " + currChallenge.amount.ToString() + " " + Challenge.ResourceTypeToString(currChallenge.neededResource);
        } else {
            ChallengeText.text = "No challenge active.";
        }

        string scstr = score == 0 ? "0" : score.ToString("#,###");
        ScoreText.text = "Score: " + scstr;
        TimerText.text = "Time left: " + FormatTime(currChallenge.timeLimit - challengeTimer);
    }

    public static string FormatTime(float seconds) {
        int mins = Mathf.FloorToInt(seconds / 60.0f);
        int secs = Mathf.FloorToInt(seconds % 60.0f);
        return mins > 0 ? mins.ToString() + "m " + secs.ToString() + "s" : secs.ToString() + "s";
    }

    public bool CheckChallengeFullfulled() {
        if((player.transform.position - altar.transform.position).magnitude > maxDistanceToAltar && currChallenge.neededResource != ResourceType.none)
            return false;

        switch(currChallenge.neededResource) {
            case ResourceType.FruitBlue:
                if(InvFruitBlue >= currChallenge.amount) {
                    InvFruitBlue -= currChallenge.amount;
                    return true;
                } else {
                    return false;
                }
            case ResourceType.FruitGreen:
                if(InvFruitGreen >= currChallenge.amount) {
                    InvFruitGreen -= currChallenge.amount;              
                    return true;
                } else {
                    return false;
                }
            case ResourceType.FruitPink:
                if(InvFruitPink >= currChallenge.amount) {
                    InvFruitPink -= currChallenge.amount;
                    return true;
                } else {
                    return false;
                }
            case ResourceType.FruitPurple:
                if(InvFruitPurple >= currChallenge.amount) {
                    InvFruitPurple -= currChallenge.amount;
                    return true;
                } else {
                    return false;
                }
            case ResourceType.Flower:
                if(InvFlower >= currChallenge.amount) {
                    InvFlower -= currChallenge.amount;
                    return true;
                } else {
                    return false;
                }
            case ResourceType.none:
                return true;
        }
        return false;
    }

    public static void AwardScore(int score) {
        instance.score += Mathf.FloorToInt(score * ((instance.challengesCompleted + 11) / 10.0f));
    }

    public static void AwardResource(ResourceType type, int amount) {
        switch(type) {
            case ResourceType.FruitBlue:
                instance.InvFruitBlue += amount;
                break;
            case ResourceType.FruitGreen:
                instance.InvFruitGreen += amount;
                break;
            case ResourceType.FruitPink:
                instance.InvFruitPink += amount;
                break;
            case ResourceType.FruitPurple:
                instance.InvFruitPurple += amount;
                break;
            case ResourceType.Flower:
                instance.InvFlower += amount;
                break;
            case ResourceType.none:
                break;
        }
    }

    public static void PlayPickupSound() {
        instance.player.GetComponentInChildren<AudioSource>().Play();
    }

    IEnumerator Dabbing() {
        bool enabled = true;
        while(enabled) {
            player.GetComponentInChildren<Animator>().SetBool("IsAccomplished", true);
            yield return new WaitForSeconds(0.75f);
            enabled = false;
        }
        player.GetComponentInChildren<Animator>().SetBool("IsAccomplished", false);
    }
}