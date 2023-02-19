using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSkin : MonoBehaviour
{
    private int playerSkin;
    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator= GetComponent<Animator>();
        playerSkin = PlayerPrefs.GetInt("playerSkin");
        CharachterSelection();

    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("skin number:" + playerSkin);
    }

    private void CharachterSelection()
    {
        switch (playerSkin)
        {
            case 0:
                animator.SetBool("isSpaceman", true);
                animator.SetBool("isFrog", false);
                animator.SetBool("isAdventureTime", false);
                animator.SetBool("isTiki", false);
                break;
            case 1:
                animator.SetBool("isSpaceman", false);
                animator.SetBool("isFrog", true);
                animator.SetBool("isAdventureTime", false);
                animator.SetBool("isTiki", false);
                break;
            case 2:
                animator.SetBool("isSpaceman", false);
                animator.SetBool("isFrog", false);
                animator.SetBool("isAdventureTime", true);
                animator.SetBool("isTiki", false);
                break;
            case 3:
                animator.SetBool("isSpaceman", false);
                animator.SetBool("isFrog", false);
                animator.SetBool("isAdventureTime", false);
                animator.SetBool("isTiki", true);
                break;
        }
    }

    public void SetCharachterSkin(int skin)
    {
        playerSkin = skin;
        PlayerPrefs.SetInt("playerSkin", skin);
        animator.SetTrigger("FakeDeath");
        CharachterSelection();
    }
}
