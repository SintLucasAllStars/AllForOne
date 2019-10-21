using UnityEngine;

namespace MechanicFever
{
    public class UnitTemplateButton : Button
    {
        [SerializeField]
        private UnitData _unitData;

        public override void Press()
        {
            CharacterCreationManager.Instance.UpdateValues(_unitData.Health, _unitData.Strength, _unitData.Speed, _unitData.Defense, _unitData.Type);
        }
    }
}
