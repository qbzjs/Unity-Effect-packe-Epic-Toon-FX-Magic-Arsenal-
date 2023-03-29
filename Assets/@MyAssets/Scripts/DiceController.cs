using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DiceController : MonoBehaviour
{
    public Button diceSpeenButton;
    [SerializeField] GameObject _camera;
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
        var pos = transform.position;
        pos.y += 1;
        _camera.transform.position = pos;

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
            diceSpeenButton.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                diceSpeenButton.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear);
                diceSpeenButton.interactable = false;
            });
            _active = false;
            Vector3 rot = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200));
            if (rot == Vector3.zero)
            {
                Debug.Log("zero");
                rot.x = 150;
                rot.y = -90;
                rot.z = 50;
            }
            //Vector3 rot = new Vector3(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            //var speed = Random.Range(50, 100);
            var hight = Random.Range(1000, 2000);
            _rb.AddForce(Vector3.up * hight, ForceMode.Impulse);
            _rb.AddRelativeTorque(rot * hight, ForceMode.Acceleration);
            //_rb.AddTorque(rot * speed, ForceMode.Impulse);
            //HideButton();
            StartCoroutine(CheckButton());
        }
    }

    IEnumerator CheckButton()
    {
        //yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => (_rb.velocity.y < 0 ? true : false));
        yield return new WaitUntil(() => _rb.velocity == Vector3.zero);
        //yield return new WaitForSeconds(1);
        var gameController = GameController.instance;
        gameController.allGunController[gameController.currentDiceNumber - 1].Shoot();
        diceSpeenButton.interactable = true;
        _active = true;
        //ShowButton();
    }
}
