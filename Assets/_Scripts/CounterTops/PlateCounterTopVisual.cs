using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSimulator.CounterTops
{
    public class PlateCounterTopVisual : MonoBehaviour
    {
        [SerializeField] private PlateCounterTop _plateCounterTop;
        [SerializeField] private Transform _counterTopSpawnPoint;
        [SerializeField] private Transform _plateVisualPrefab;
        private List<GameObject> _plateVisualGameObjectList;

        private void Awake()
        {
            _plateVisualGameObjectList = new List<GameObject>();
        }

        private void Start()
        {
            _plateCounterTop.OnPlateSpawned += OnPlateSpawned;
            _plateCounterTop.OnPlateRemoved += OnPlateRemoved;
        }

        private void OnPlateSpawned(object sender, EventArgs eventArgs)
        {
            var plateVisual = Instantiate(_plateVisualPrefab, _counterTopSpawnPoint);

            var plateOffsetY = 0.1f;
            plateVisual.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);
            _plateVisualGameObjectList.Add(plateVisual.gameObject);
        }
        
        private void OnPlateRemoved(object sender, EventArgs eventArgs)
        {
            var plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
            _plateVisualGameObjectList.Remove(plateGameObject);
            Destroy(plateGameObject);
        }
    }
}
