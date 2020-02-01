using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private FaceScript _emotion;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    private bool _screamAnimation;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _emotion = GetComponentInChildren<FaceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_screamAnimation)
            return;
        
        if (gameObject.transform.position.y > 3)
        {
            _emotion.SwitchFace(Enums.FaceType.Happy);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("IsScreaming");
        }
        
        
    }

    public IEnumerator IsScreaming()
    {
        if (_screamAnimation)
            yield break;

        _screamAnimation = true;
        _emotion.SwitchFace(Enums.FaceType.Scream);
        yield return new WaitForSeconds(1);
        _screamAnimation = false;
        _emotion.SwitchFace(Enums.FaceType.Neutral);
    }
    
}
