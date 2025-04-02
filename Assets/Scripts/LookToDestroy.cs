using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro

public class LookToDestroy : MonoBehaviour
{
    public float lookTimeToDestroy = 1f;
    private float lookTimer = 0f;
    private Transform lastLookedObject = null;

    public Image reticle;
    public Image destructionTimer;
    public TextMeshProUGUI scoreText; // Assign this in the Inspector

    private Camera vrCamera;

    [SerializeField] private float minRotationSpeed = 30f;
    [SerializeField] private float maxRotationSpeed = 200f;
    private float rotationSpeed;

    private int score = 0; // Score variable

    void Start()
    {
        vrCamera = Camera.main;
        destructionTimer.fillAmount = 0f;
        rotationSpeed = minRotationSpeed;
        UpdateScoreText();
    }

    void Update()
    {
        RotateReticle();

        RaycastHit hit;
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, 15f))
        {
            if (hit.collider.gameObject.CompareTag("Destructible"))
            {
                if (lastLookedObject == hit.collider.transform)
                {
                    lookTimer += Time.deltaTime;
                    reticle.color = Color.red;

                    destructionTimer.fillAmount = lookTimer / lookTimeToDestroy;

                    rotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, lookTimer / lookTimeToDestroy);

                    if (lookTimer >= lookTimeToDestroy)
                    {
                        Destroy(hit.collider.gameObject);
                        IncreaseScore(); // Add score
                        ResetLook();
                    }
                }
                else
                {
                    lastLookedObject = hit.collider.transform;
                    lookTimer = 0f;
                    destructionTimer.fillAmount = 0f;
                    rotationSpeed = minRotationSpeed;
                }
            }
            else
            {
                ResetLook();
            }
        }
        else
        {
            ResetLook();
        }
    }

    void RotateReticle()
    {
        if (reticle != null)
        {
            reticle.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    void IncreaseScore()
    {
        score += 10; // Add 10 points per asteroid
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    void ResetLook()
    {
        lookTimer = 0f;
        lastLookedObject = null;
        reticle.color = Color.white;
        destructionTimer.fillAmount = 0f;
        rotationSpeed = minRotationSpeed;
    }
}
