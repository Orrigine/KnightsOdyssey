using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private StructPlayer _structPlayer;
    [SerializeField] private Image _iconAttack;
    [SerializeField] private Image _iconRoll;
    [SerializeField] private Image _iconShield;

    private void LateUpdate()
    {
        ChangeIconUI(_structPlayer.CanAttacking, _iconAttack);
        ChangeIconUI(_structPlayer.CanRoll, _iconRoll);
        ChangeIconUI(_structPlayer.CanShield, _iconShield);
    }

    private void ChangeIconUI(bool value, Image icon) 
    {
        if (value)
        {
            icon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            icon.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
