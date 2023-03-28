using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceController : MonoBehaviour
{
    public GameObject diceSpeenButton;
    [SerializeField] Rigidbody _rb;
    [SerializeField] LayerMask _layerMask;

    bool _active = true;

    void Update()
    {
        /*if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Dice")
                {
                    Debug.Log("Dice clicked");
                }

                if (raycastHit.collider.CompareTag("Dice"))
                {
                    Debug.Log("Dice clicked");
                }
            }
        }*/


        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                //Select stage    
                if (hit.transform.name == "Dice")
                {
                    Debug.Log("Dice clicked");
                    //playAnimation();
                }
            }
        }
    }

    public void ShowButton()
    {
        diceSpeenButton.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        _active = true;
    }

    public void HideButton()
    {
        diceSpeenButton.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
    }

    public void playAnimation()
    {
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        /* transform.DORotate(pos, 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
         {
             _rb.isKinematic = false;

         });*/
        if (_active)
        {
            _active = false;
            Vector3 rot = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200));
            //Vector3 rot = new Vector3(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            var speed = Random.Range(50, 100);
            var hight = Random.Range(700, 800);
            _rb.AddForce(Vector3.up * hight, ForceMode.Impulse);
            _rb.AddRelativeTorque(rot * speed, ForceMode.Acceleration);
            //_rb.AddTorque(rot * speed, ForceMode.Impulse);
            HideButton();
            StartCoroutine(CheckButton());
        }
    }

    IEnumerator CheckButton()
    {
        //yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => _rb.angularVelocity == Vector3.zero);
        yield return new WaitForSeconds(1);
        ShowButton();
    }
}
