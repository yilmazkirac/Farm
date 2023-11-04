using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool _isReadyToPick;
    private Vector3 _originalcale;
    [SerializeField] private ProductData productData;
    private BagConrtoller _bagController;

    private void Awake()
    {
        _isReadyToPick = true;
        _originalcale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&_isReadyToPick)
        {
            _bagController = other.GetComponent<BagConrtoller>();
            _bagController.AddProductToBag(productData.ProductPrefab);
            _isReadyToPick = false;
            StartCoroutine(ProductsPicked());
        }
    }

    IEnumerator ProductsPicked()
    {
        Vector3 targetScale = _originalcale / 3;
        transform.gameObject.LeanScale(targetScale, 1f);
        yield return new WaitForSeconds(5f);
        transform.gameObject.LeanScale(_originalcale, 1f).setEase(LeanTweenType.easeOutBack);
        _isReadyToPick = true;
    }
   // IEnumerator ProductPicked()
   // {
   //     float duration = 1f;
   //     float timer = 0;
   //     Vector3 targetScale = _originalcale / 3;
   //
   //      while (timer<duration)
   //      {
   //          float t = timer / duration;
   //          Vector3 newScale = Vector3.Lerp(_originalcale, targetScale, t);
   //          transform.localScale = newScale;
   //          timer += Time.deltaTime;
   //          yield return null;
   //      }
   //      yield return new WaitForSeconds(5f);
   //      timer = 0;
   //      float growBackDuration = 1f;
   //
   //      while (timer<growBackDuration)
   //      {
   //          float t = timer / growBackDuration;
   //          Vector3 newScale = Vector3.Lerp(targetScale, _originalcale, t);
   //          transform.localScale = newScale;
   //          timer += Time.deltaTime;
   //          yield return null;
   //      }
   //
   //      _isReadyToPick = true;
   //      yield return null;
   //  }
}
