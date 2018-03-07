using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCompleteUI : MonoBehaviour {

    public Transform playerEye;
    public GameManager gameManager;
    public Material[] mats;
    public GameObject[] triangleMeshes;

    public bool moveBack;
    public bool done;
    public bool turn;

    public TextMeshPro roundCountText;
    public TextMeshPro roundCompleteText;
    public TextMeshPro enemyEffectsText;
    public int randomTextNum;

    public bool roundCompleteFadeIn;
    public bool roundCompleteFadeOut;

    public bool roundCountFadeIn;
    public bool roundCountFadeOut;

    public bool enemyEffectsFadeIn;
    public bool enemyEffectsFadeOut;

    public GameObject UIWhole;

    public int triangleCurrent;

    public float endDelay;

    public EnemyEffectsManager enemyEffectsManager;

    public GameObject[] triangleOutline;

    public bool[] triangleScale;

    //public GameObject highlightCube;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyEffectsManager = GameObject.Find("EnemyEffectsManager").GetComponent<EnemyEffectsManager>();
        triangleScale = new bool[triangleOutline.Length];
    }

    private void OnEnable()
    {

        roundCompleteFadeIn = false;
        roundCompleteFadeOut = false;
        roundCountFadeIn = false;
        enemyEffectsFadeIn = false;
        enemyEffectsFadeOut = false;

        for (int i = 0; i < triangleScale.Length; i++)
        {
            triangleScale[i] = false;
        }

        foreach(GameObject triangle in triangleOutline)
        {
            triangle.transform.localScale = Vector3.zero;

        }

        foreach (GameObject triangle in triangleOutline)
        {
            triangle.SetActive(false);

        }
        /*triangleCurrent = gameManager.roundCurrent - 1;

        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        for (int i = 0; i <= (triangleCurrent); i++)
        {
            Renderer rend = triangleMeshes[i].GetComponent<Renderer>();
            rend.material = mats[1]; //sets all triangles to complete round UI material
        }*/


        roundCompleteText.alpha = 0;
        roundCountText.alpha = 0;
        enemyEffectsText.alpha = 0;


        transform.position = new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z);


        StartCoroutine(NomralFlash());
        StartCoroutine(TriangleOutlineScale());

        //StartCoroutine(RoundCurrentTriangleFlash());

        //randomTextNum = Random.Range(1, 20);
        //RandomText();

        CheckRound();
        //CheckEnemyEffects();



    }

    // Update is called once per frame
    void Update () {
        if (roundCompleteFadeOut)
        {
            roundCompleteText.alpha -= Time.deltaTime;
        }

        if (roundCompleteFadeIn)
        {
            roundCompleteText.alpha += Time.deltaTime;
            //print("round complete fade in");
        }

        
        if (roundCountFadeIn)
        {
            roundCountText.alpha += Time.deltaTime;
        }
        /*if (roundCountFadeOut)
        {
            roundCountText.alpha -= Time.deltaTime;
        }*/

        if (enemyEffectsFadeIn)
        {
            enemyEffectsText.alpha += Time.deltaTime;
            if (enemyEffectsText.alpha >= 1)
            {
                enemyEffectsFadeIn = false;
                enemyEffectsFadeOut = true;                
            }
        }

        if (enemyEffectsFadeOut)
        {
            enemyEffectsText.alpha -= Time.deltaTime;
            if (enemyEffectsText.alpha <= 0)
            {
                enemyEffectsFadeOut = false;
                enemyEffectsFadeIn = true;
            }
        }


        for (int i = 0; i < triangleScale.Length; i++)
        {
            if (triangleScale[i])
            {
                //print("for loop called");
                triangleOutline[i].transform.localScale = Vector3.Lerp(triangleOutline[i].transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3f);
            }
        }

        


        if (moveBack)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z), Time.deltaTime * 5f);
            done = true;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerEye.transform.position + playerEye.transform.forward * 4f, Time.deltaTime * 2.5f);
        }

        if (turn)
        {
            UIWhole.transform.localRotation = Quaternion.Lerp(UIWhole.transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 3f);
        }

        if (done)
        {
            moveBack = false;
            done = false;
            this.gameObject.SetActive(false);
        }

        transform.LookAt(playerEye);

        CheckEnemyEffects();

    }

    /*void RoundCompleteUIFade()
    {
        roundCompleteText.alpha += Time.deltaTime;
        if (roundCompleteText.alpha >= 1)
        {
            yield return new WaitForSeconds(2f);
            roundCompleteText.alpha -= Time.deltaTime * 2f;
            if (roundCompleteText.alpha <= .5f)
            {
                roundText.alpha += Time.deltaTime;
            }

        }
    }*/

    void CheckRound()
    {
        roundCountText.text = "Round " + gameManager.roundCurrent + " / 10";

        if (gameManager.roundCurrent <= 1)
        {
            roundCompleteText.text = "Destroy All \n Enemies";
        } else
        {
            roundCompleteText.text = "Round Complete";
        }

        /*
        switch (gameManager.roundCurrent)
        {
            case 1:
                roundText.text = "Round 1 / 10";
                break;
            case 2:
                roundText.text = "Round 2 / 10";
                break;
            case 3:
                roundText.text = "Round 3 / 10";
                break;
            case 4:
                roundText.text = "Round 4 / 10";
                break;
            case 5:
                roundText.text = "Round 5 / 10";
                break;
            case 6:
                roundText.text = "Round 6 / 10";
                break;
            case 7:
                roundText.text = "Round 7 / 10";
                break;
            case 8:
                roundText.text = "Round 8 / 10";
                break;
            case 9:
                roundText.text = "Round 9 / 10";
                break;
            case 10:
                roundText.text = "Round 10 / 10";
                break;

        }
        */

        /*
        switch (randomTextNum)
        {
            case 1:
                textSaying.text = "Ha! I kill me!";
                break;
            case 2:
                textSaying.text = "I have the power!!!";
                break;
            case 3:
                textSaying.text = "Ahh..What a rush!";
                break;
            case 4:
                textSaying.text = "Bozhe moi!";
                break;
            case 5:
                textSaying.text = "I'll be back";
                break;
            case 6:
                textSaying.text = "By the power of Greyskull!";
                break;
            case 7:
                textSaying.text = "Cut, It, Out";
                break;
            case 8:
                textSaying.text = "Eat My Shorts";
                break;
            case 9:
                textSaying.text = "Form blazing sword!";
                break;
            case 10:
                textSaying.text = "Freeze! This is Miami Vice!";
                break;
            case 11:
                textSaying.text = "Go, Joe!";
                break;
            case 12:
                textSaying.text = "Go, Go Gadget Go!";
                break;
            case 13:
                textSaying.text = "I Play For Keeps!";
                break;
            case 14:
                textSaying.text = "It's crime fighting time!";
                break;
            case 15:
                textSaying.text = "Let's Get Dangerous";
                break;
            case 16:
                textSaying.text = "Punky Power!";
                break;
            case 17:
                textSaying.text = "Resistance is not futile.";
                break;
            case 18:
                textSaying.text = "Synchronise Swatches!";
                break;
            case 19:
                textSaying.text = "Thundercats, Ho!";
                break;
            case 20:
                textSaying.text = "Holy macanoli!";
                break;
        }
        */
    }

    void CheckEnemyEffects()
    {
        //print("Round Complete UI: " + enemyEffectsManager.randomEffectInt);

        switch (enemyEffectsManager.randomEffectInt)
        {
            case 0:
                enemyEffectsText.text = "ENEMY HEALTH INCREASED";
                break;
            case 1:
                enemyEffectsText.text = "ENEMY DAMAGE INCREASED";
                break;
            case 2:
                enemyEffectsText.text = "ENEMY ACCURACY INCREASED";
                break;
            case 3:
                enemyEffectsText.text = "ENEMY FIRE RATE INCREASED";
                break;
            case 4:
                enemyEffectsText.text = "NEW ENEMY INCOMING";
                break;
        }
    }

    IEnumerator RoundCurrentTriangleFlash()
    {
        UIWhole.SetActive(true);

        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);


        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);

        /*
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);

        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        */



        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(endDelay);

        turn = true;
        yield return new WaitForSeconds(endDelay);


        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);

        /*
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        */

        //moveBack = true;
        //yield return new WaitForSeconds(1f);

        done = true;

    }

    IEnumerator NomralFlash()
    {
        UIWhole.SetActive(true);

        roundCompleteFadeIn = true;
        yield return new WaitForSeconds(2f);
        roundCompleteFadeIn = false;
        roundCompleteFadeOut = true;
        yield return new WaitForSeconds(2f);

        roundCountFadeIn = true;
        enemyEffectsFadeIn = true;
        //yield return new WaitForSeconds(2f);
        

        yield return new WaitForSeconds(endDelay);



        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);

        done = true;
    }

    IEnumerator TriangleOutlineScale()
    {
        yield return new WaitForSeconds(.75f);

        triangleScale[0] = true;
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < triangleScale.Length; i++)
        {
            triangleOutline[i].SetActive(true);
            triangleScale[i] = true;
            yield return new WaitForSeconds(.5f);
            //print("for loop called");

        }

        /*foreach (GameObject triangle in triangleOutline)
        {
            triangle.SetActive(true);
            triangle.transform.localScale = Vector3.Lerp(triangle.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 10f);
            yield return new WaitForSeconds(.5f);
        }*/
    }
}
