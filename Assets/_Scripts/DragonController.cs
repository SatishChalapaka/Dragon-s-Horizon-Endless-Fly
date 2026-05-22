using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DragonGame;

public class DragonController : MonoBehaviour
{
    public static DragonController instance;
    public float forwardSpeed;
    
    [HideInInspector]public Animator anim;
    [HideInInspector] public Rigidbody rigidbody;
    public float rotationAmount;
    public float touchControlRangePixels = 180f;
    public float horizontalFlySpeed = 12f;
    public float verticalFlySpeed = 8f;
    public float pitchAmount = 25f;
    public float yawAmount = 20f;
    public float rollAmount = 35f;
    public float rotationSmoothSpeed = 8f;
    public LayerMask ringLayerMask;
    public ScoreManager scoreManagerScript;
    public bool isMove;
    public float minX, maxX, minY, maxY;
    
    public Material dragonMaterialNormal;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Color _emissionColorValue;
    public Color emmisionColor;
    public float _intensity;
    Material skinMeshMaterial;
    Coroutine jetpackStopCoroutine;
    Vector2 flyInput;
    Vector2 touchStartPosition;
    Vector3 cameraOffset;
    public CinemachineVirtualCamera cinemachineVirtualCamera; 
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        skinMeshMaterial = skinnedMeshRenderer.material;
        if (cinemachineVirtualCamera != null)
        {
            cameraOffset = cinemachineVirtualCamera.transform.position - transform.position;
            cinemachineVirtualCamera.m_Follow = null;
        }
    }
    [SerializeField] [Range(0, 1)] float progress = 0;
    public float percentage;
    private void FixedUpdate()
    {
        if (isMove)
        {
            
            //Boost
            //if (GameManager.instance.isBoost)
            //{
            //    forwardSpeed = 1500f;
            //    StartCoroutine(WaitForBoostStop());
            //}else
            //{
            //    if (forwardSpeed >= 600)
            //    {
            //        forwardSpeed = 600;
            //    }
            //    else
            //    {
            //        forwardSpeed += 2f * Time.deltaTime;
            //    }
            //}
            //Jetpack
            if (GameManager.instance.isJetpack)
            {
                if (GameManager.instance.isJetPackCompleted == true)
                {
                    Fly(flyInput.x, flyInput.y, -2f, 16);
                }
                else
                {
                    Fly(flyInput.x, flyInput.y, 20f, 16);
                    if (jetpackStopCoroutine == null)
                    {
                        jetpackStopCoroutine = StartCoroutine(WaitForJetpacktStop());
                    }
                }
            }
            else
            {
                scoreManagerScript.CurrentGameScore += 1;
                scoreManagerScript.currentGameScoreText.text = scoreManagerScript.CurrentGameScore.ToString();
                scoreManagerScript.currentGameScoreTextGameover.text = scoreManagerScript.CurrentGameScore.ToString();
                //if (forwardSpeed >= 600)
                //{
                //    forwardSpeed = 600;
                //}
                //else
                //{
                //    forwardSpeed += 2f * Time.deltaTime;
                //}

                //Dragon Animation
                DragonController.instance.anim.SetBool("isMove", true);
                Movement(flyInput.x, flyInput.y);
                //UIController.instance.healthBar.value -= 0.05f * Time.deltaTime;

                //Dragon Movement
                Fly(flyInput.x, flyInput.y, 0, maxY);
            }

            // if (UIController.instance.healthBar.value >= 0.3f)
            // {
            //     UIController.instance.dangerIndication.SetActive(false);
            // }
            // else
            // {
            //     UIController.instance.dangerIndication.SetActive(true);
            //     SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("dangerindication"));
            // }
            // if (UIController.instance.healthBar.value <= 0)
            // {
            //     GameFailed();
            // }
        }
        
    }
    private void Update()
    {
        ReadTouchInput();

        if (isMove)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1.5f, ringLayerMask) && isMove || Physics.Raycast(transform.position, Vector3.left, out hit, 1.5f, ringLayerMask) && isMove)
            {
                TutorialManager.instance.increaseHealthText.SetActive(false);
                PlayerPrefs.SetInt("Tutorial", 1);
                //UIController.instance.healthBar.value += 0.5f * Time.deltaTime;
                // scoreManagerScript.CurrentGameScore += 1;
                // scoreManagerScript.currentGameScoreText.text = scoreManagerScript.CurrentGameScore.ToString();
                // scoreManagerScript.currentGameScoreTextGameover.text = scoreManagerScript.CurrentGameScore.ToString();
                // if (Physics.Raycast(transform.position, Vector3.right, out hit, 1.5f, ringLayerMask))
                // {
                //     Debug.DrawRay(transform.position, Vector3.right, Color.green);
                //     skinMeshMaterial.SetColor("_EmissionColor", emmisionColor);
                //     skinMeshMaterial.SetVector("_EmissionColor", _emissionColorValue * _intensity);
                // }
                // else if (Physics.Raycast(transform.position, Vector3.left, out hit, 1.5f, ringLayerMask))
                // {
                //     Debug.DrawRay(transform.position, Vector3.left, Color.green);
                //     skinMeshMaterial.SetColor("_EmissionColor", emmisionColor);
                //     skinMeshMaterial.SetVector("_EmissionColor", _emissionColorValue * _intensity);
                // }
            }
            else
            {
                skinMeshMaterial.SetColor("_EmissionColor", Color.white);
                skinMeshMaterial.SetVector("_EmissionColor", _emissionColorValue * 0);
            }
        }
    }
    private void LateUpdate()
    {
        if (cinemachineVirtualCamera == null || !isMove)
        {
            return;
        }
        cinemachineVirtualCamera.Follow =  this.transform;
        Vector3 cameraPosition = cinemachineVirtualCamera.transform.position;
        cameraPosition.z = transform.position.z + cameraOffset.z;
        cinemachineVirtualCamera.transform.position = cameraPosition;
    }
    private void Movement(float x, float y)
    {
        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);
    }
    private void ReadTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isMove = true;
                touchStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                flyInput = Vector2.ClampMagnitude((touch.position - touchStartPosition) / touchControlRangePixels, 1f);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                flyInput = Vector2.zero;
            }

            return;
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            isMove = true;
            touchStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            flyInput = Vector2.ClampMagnitude(((Vector2)Input.mousePosition - touchStartPosition) / touchControlRangePixels, 1f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            flyInput = Vector2.zero;
        }
#else
        //flyInput = Vector2.zero;
#endif
    }
    private void Fly(float horizontalInput, float verticalInput, float verticalBoost, float maxHeight)
    {
        // FORWARD MOVEMENT
        Vector3 forwardMove =
            transform.forward * forwardSpeed * Time.deltaTime;

        // UP / DOWN MOVEMENT
        Vector3 verticalMove =
            transform.up *
            ((verticalInput * verticalFlySpeed) + verticalBoost)
            * Time.deltaTime;

        rigidbody.velocity =
            (forwardMove + verticalMove);

        // OPTIONAL HEIGHT LIMIT
        rigidbody.position = new Vector3(
            rigidbody.position.x,
            Mathf.Clamp(rigidbody.position.y, minY, maxHeight),
            rigidbody.position.z);

        // ROTATION
        float targetPitch =
    verticalInput * -pitchAmount;

        float targetYaw =
            horizontalInput * yawAmount;

        Quaternion targetRotation =
            Quaternion.Euler(
                targetPitch,
                transform.eulerAngles.y + targetYaw,
                0);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSmoothSpeed * Time.deltaTime);

    //    float tilt =
    //horizontalInput * -10f;

    //    Camera.main.transform.localRotation =
    //        Quaternion.Lerp(
    //            Camera.main.transform.localRotation,
    //            Quaternion.Euler(0, 0, tilt),
    //            2f * Time.deltaTime);
    }
    private void OnParticleCollision()
    {
        GameFailed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rings")
        {
            if (GameManager.instance.isBoost)
            {
                //collision.gameObject.GetComponent<MeshCollider>().convex = enabled;
                //collision.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                collision.gameObject.SetActive(false);
                return;
            }
            GameManager.instance.Vibrate();
            GameFailed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            scoreManagerScript.AddCoinBalance(1);
            GameManager.instance.coinParticleCollect.transform.SetParent(gameObject.transform);
            GameManager.instance.coinParticleCollect.transform.position = new Vector3(transform.position.x,transform.position.y + 0.6f,transform.position.z);
            GameManager.instance.coinParticleCollect.Play();
            SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("JetPack"))
        {
            GameManager.instance.JetpackEnable();
            int randomNum = Random.Range(3, 6);
            other.gameObject.transform.GetChild(randomNum).gameObject.SetActive(true);
        }
    }
    public void GameFailed()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        GameManager.instance.isJetpack = false;
        jetpackStopCoroutine = null;
        Challenges.instance.Challenge1_1(scoreManagerScript.CurrentCoins);
        Challenges.instance.Challenge2_1(ScoreManager.instance.CurrentCoins);
        Challenges.instance.Challenge3_1(scoreManagerScript.CurrentGameScore);
        Challenges.instance.Challenge4_1(scoreManagerScript.CurrentGameScore);
        Challenges.instance.Challenge6_1(1);
        Challenges.instance.ActivateChallenges6_1();
        Challenges.instance.CheckChallenge6_1();
        if (PlayerPrefs.GetInt("SavedPlayerNumber") == 10 || PlayerPrefs.GetInt("SavedPlayerNumber") == 11 || PlayerPrefs.GetInt("SavedPlayerNumber") == 12 || PlayerPrefs.GetInt("SavedPlayerNumber") == 13 || PlayerPrefs.GetInt("SavedPlayerNumber") == 14)
        {
            Challenges.instance.Challenge7_1(scoreManagerScript.CurrentGameScore);
        }
        SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("gameover"));
        UIController.instance.dangerIndication.SetActive(false);
        //ObjectPooler.current.ResetBullets();
        AdsInitializer.instance.interstitialAdExample.ShowAd();
        skinMeshMaterial.SetColor("_EmissionColor", Color.white);
        skinMeshMaterial.SetVector("_EmissionColor", _emissionColorValue * 0);
        isMove = false;
        flyInput = Vector2.zero;
        DragonController.instance.anim.SetBool("isMove", false);
        Movement(0, 0);
        //UIController.instance.healthBar.value = 1f;
        //anim.SetBool("isDead", true);
        //cinemachineVirtualCamera.m_Follow = null;
        cinemachineVirtualCamera.transform.position = new Vector3(0, 2, -2);
        UIController.instance.gameoverPanel.SetActive(true);
        GameManager.instance.confettiParticleGameover.Play();
        UIController.instance.gamePanel.SetActive(false);
        //rigidbody.useGravity = true;
        rigidbody.isKinematic = true;
        DragonController.instance.transform.position = new Vector3(0, 1, 0);
        DragonController.instance.transform.rotation = Quaternion.identity;
        
        scoreManagerScript.coinCountText.text = scoreManagerScript.CurrentCoins.ToString();
        scoreManagerScript.coinCountTextGameover.text = scoreManagerScript.CurrentCoins.ToString();

        if (scoreManagerScript.CurrentGameScore >= PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreManagerScript.CurrentGameScore);
            scoreManagerScript.highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            scoreManagerScript.highScoreTextGameover.text = PlayerPrefs.GetInt("HighScore").ToString();
        }

        scoreManagerScript.currentGameScoreText.text = scoreManagerScript.CurrentGameScore.ToString();
        scoreManagerScript.currentGameScoreTextGameover.text = scoreManagerScript.CurrentGameScore.ToString();
        PlayfabManager.instance.SendLeaderboard(PlayerPrefs.GetInt("HighScore"));
    }
    public IEnumerator WaitForBoostStop()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.isBoost = false;
    }
    public IEnumerator WaitForJetpacktStop()
    {
        yield return new WaitForSeconds(7f);
        GameManager.instance.isJetPackCompleted = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.isJetpack = false;
        GameManager.instance.isJetPackCompleted = false;
        jetpackStopCoroutine = null;
    }
    public IEnumerator WaitForJetpacktJumpUpAnimationStop()
    {
        DragonController.instance.anim.SetBool("isJetpack", true);
        yield return new WaitForSeconds(0.5f);
        DragonController.instance.anim.SetBool("isJetpack", false);
        DragonController.instance.anim.SetBool("isMove", true);
    }
}
