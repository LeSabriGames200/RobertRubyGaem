using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;
    public Camera playerCamera;
    public GameObject tutorRobert;
    public GameObject robert;
    public RobertAI robertScript;
    public GameObject haneythan;
    public TMP_Text rubyText;
    public TMP_Text itemText;
    public int rubys;
    public int exitReached;
    public GameObject exitBlockerIThink;
    public GameObject exitBlockerIThink2;
    public int itemSelected;
    public int fakeRubyRandomisedTexture;
    public int[] item = new int[3];
    public Texture[] itemTextures = new Texture[4];
    public RawImage[] itemSlot = new RawImage[3];
    public RectTransform itemSelect;
    public Sprite[] fakeRubyTextures = new Sprite[2];
    public AudioSource audio;
    public AudioSource houseMusic;
    public AudioSource padMusic;
    public AudioSource robertAudio;
    public AudioClip jumpscareSound;
    public AudioClip exitClosed;
    public GameObject pauseMenu;
    public bool spoopMode;
    public bool finalMode;
    public bool gameIsPaused;

    private bool isGameCanPaused;
    private string[] itemNames = new string[4]
    {
        "Nothing",
        "Ruby Flavored Strawberry",
        "Item 2",
        "Bisque"
    };
    private int[] itemSelectOffset = new int[3]
    {
        386,
        591,
        794
    };


    void Start()
    {
        UpdateRubyCount();
        itemSelected = 0;
        houseMusic.Play();
        fakeRubyRandomisedTexture = Random.Range(0, 2);
        Sprite[] fakeRubyTextures = new Sprite[2];
        Sprite selectedTexture = fakeRubyTextures[fakeRubyRandomisedTexture];
        isGameCanPaused = true;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            itemSelected--;
            if (itemSelected < 0)
            {
                itemSelected = 2;
            }
            UpdateItemSelection();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            itemSelected++;
            if (itemSelected > 2)
            {
                itemSelected = 0;
            }
            UpdateItemSelection();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            itemSelected = 0;
            UpdateItemSelection();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            itemSelected = 1;
            UpdateItemSelection();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            itemSelected = 2;
            UpdateItemSelection();
        }
        if (Input.GetMouseButton(1))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameCanPaused)
            {
                if (gameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void UpdateRubyCount()
    {
        rubyText.text = rubys + "/5 Rubys";

        if (rubys == 5)
        {
            ActivateFinalMode();
        }
    }

    public void CollectRuby()
    {
        rubys++;
        UpdateRubyCount();
    }

    public void ActiveRubyGame()
    {
        if (!spoopMode)
        {
            houseMusic.Stop();
            padMusic.Play();
        }
        player.canMove = false;
        robertScript.enabled = false;
        robert.GetComponent<NavMeshAgent>().enabled = false;
        haneythan.GetComponent<Wanderer>().enabled = false;
        haneythan.GetComponent<NavMeshAgent>().enabled = false;
        robertAudio.Stop();
        isGameCanPaused = false;
    }

    public void DisactiveRubyGame()
    {
        if (!spoopMode)
        {
            houseMusic.Play();
            padMusic.Stop();
        }
        else
        {
            robertScript.Angrier();
        }
        player.canMove = true;
        robertScript.enabled = true;
        haneythan.GetComponent<Wanderer>().enabled = true;
        haneythan.GetComponent<NavMeshAgent>().enabled = true;
        isGameCanPaused = true;
    }

    public void ActivateSpoopMode()
    {
        spoopMode = true;
        tutorRobert.SetActive(false);
        robert.SetActive(true);
        haneythan.SetActive(true);
        exitBlockerIThink.SetActive(true);
        exitBlockerIThink2.SetActive(true);
        houseMusic.Stop();
        padMusic.Stop();
    }

    public void ActivateFinalMode()
    {
        finalMode = true;
        exitBlockerIThink.SetActive(false);
        exitBlockerIThink2.SetActive(false);
    }

    public void CollectItem(int itemId)
    {
        bool inventoryFull = true;

        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] == 0)
            {
                item[i] = itemId;
                UpdateItemName();
                UpdateItemSlotTexture(itemId, i);
                inventoryFull = false;
                return;
            }
        }
        if (inventoryFull)
        {
            item[itemSelected] = itemId;
            UpdateItemName();
            UpdateItemSlotTexture(itemId, itemSelected);
        }
    }

    public void GameOver()
    {
        player.canMove = false;
        playerCamera.transform.localPosition = new Vector3(0, 5, 0);
        StartCoroutine("Jumpscare");
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        player.canMove = true;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        player.canMove = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayExitClosedSound()
    {
        audio.PlayOneShot(exitClosed);
    }

    private void UpdateItemSelection()
    {
        itemSelect.anchoredPosition = new Vector3((float)itemSelectOffset[itemSelected], 420f, 0f);
        UpdateItemName();
    }

    private void UpdateItemName()
    {
        if (itemSelected >= 0 && itemSelected < itemNames.Length)
        {
            itemText.text = itemNames[item[itemSelected]];
        }
        else
        {
            itemText.text = "Invalid Item";
        }
    }

    private void UpdateItemSlotTexture(int itemId, int slotIndex)
    {
        if (itemId >= 0 && itemId < itemTextures.Length && slotIndex >= 0 && slotIndex < itemSlot.Length)
        {
            itemSlot[slotIndex].texture = itemTextures[itemId];
        }
    }

    private void UseItem()
    {
        if (item[itemSelected] != 0)
        {
            if (item[itemSelected] == 1)
            {
                player.stamina = player.maxStamina;
                ResetItem();
            }
            if (item[itemSelected] == 2)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(UnityEngine.Random.Range(-100f, 90f), 5f, UnityEngine.Random.Range(5f, 180f));
                sphere.transform.localScale = new Vector3(4f, 4f, 4f);
                ResetItem();
            }
            if (item[itemSelected] == 3)
            {
                RenderSettings.ambientLight = Color.magenta;
                ResetItem();
            }
        }
    }
    private void ResetItem()
    {
        item[itemSelected] = 0;
        itemSlot[itemSelected].texture = itemTextures[0];
        UpdateItemName();
    }

    IEnumerator Jumpscare()
    {
        for (int i = 0; i < 100; i++)
        {
            isGameCanPaused = false;
            audio.Play();
            playerCamera.farClipPlane -= 10 * Time.deltaTime;
            audio.PlayOneShot(jumpscareSound);
            yield return new WaitForEndOfFrame();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
