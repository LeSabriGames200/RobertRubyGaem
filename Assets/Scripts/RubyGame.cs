using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RubyGame : MonoBehaviour
{

    public GameController gc;
    public Transform rubyPosition;
    public TMP_Text result;
    public Image robertReaction;
    public Image ruby;
    public Sprite robertHappy;
    public Sprite robertAngry;
    public Sprite wtfisthatbisque;
    public AudioClip rob_instruction;
    public AudioClip rob_wow;

    private AudioSource audio;
    private int randomRubyPosition;
    private float exitTime = 3f;
    private bool isGameEnded;
    private bool isBisqueThere;
    private string[] hint = new string[3]
    {
        "you've collected the ruby but im still chasing you!",
        "get back here ma boy!",
        "you arent going anywhere!"
    };

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        audio = GetComponent<AudioSource>();
        if (gc.rubys < 2)
        {
            audio.PlayOneShot(rob_instruction);
        }
        if (gc.spoopMode)
        {
            robertReaction.gameObject.SetActive(false);
        }
        result.text = "...";
        randomRubyPosition = Random.Range(0, 4);
        if (randomRubyPosition == 1)
        {
            rubyPosition.transform.localPosition = new Vector2(313, -265);
        }
        else if (randomRubyPosition == 2)
        {
            rubyPosition.transform.localPosition = new Vector2(-254, -267);
        }
        else if (randomRubyPosition == 3)
        {
            rubyPosition.transform.localPosition = new Vector2(313, 294);
        }
        isGameEnded = false;
        if(gc.rubys == 2)
        {
            isBisqueThere = true;
            ruby.sprite = gc.fakeRubyTextures[gc.fakeRubyRandomisedTexture];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded == true)
        {
            exitTime -= 1f * Time.unscaledDeltaTime;
            if(exitTime <= 0f)
            {
                GameObject.Destroy(gameObject);
                gc.DisactiveRubyGame();
            }
        }
    }

    public void CheckifRuby()
    {
        audio.Stop();
        isGameEnded = true;
        if(isBisqueThere == true)
        {
            result.text = "thats not a ruby..";
            robertReaction.sprite = robertAngry;
            gc.ActivateSpoopMode();
        }
        else
        {
            audio.PlayOneShot(rob_wow);
            result.text = "wow! you collected the ruby!";
            robertReaction.sprite = robertHappy;
        }
        if (gc.spoopMode & !isBisqueThere)
        {
            int num = Mathf.RoundToInt(Random.Range(0f, 2f));
            result.text = hint[num];
        }
    }
}
