using KitchenSimulator.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenSimulator.UI
{
    public class PlateIconsSingleUI : MonoBehaviour
    {
        [SerializeField] private Image _ingredientImage;
        
        public void SetIngredientSO(IngredientSO ingredientSO)
        {
            _ingredientImage.sprite = ingredientSO.spriteIcon;
        }
    }
}
