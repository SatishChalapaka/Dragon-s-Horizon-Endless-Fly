using MalbersAnimations.Weapons;
using UnityEngine;

namespace MalbersAnimations.Conditions
{
    [System.Serializable]
    public abstract class C2_MWeaponManagerConditions : ConditionCore
    {
        [Hide(nameof(LocalTarget))] public MWeaponManager Target;
        public virtual void SetTarget(MWeaponManager n) => Target = n;
        protected override void _SetTarget(Object target) => VerifyComponent(target, ref Target);
    }


    [System.Serializable, AddTypeMenu("Weapon Manager/Has Weapon Equipped")]
    public class C2_WM_HasWeaponEquipped : C2_MWeaponManagerConditions
    {
        [Tooltip("Weapon ID to check if is equipped")]
        public WeaponID weaponID;

        public override string DynamicName => $"WM: Has Weapon Equipped: {(weaponID == null ? "Any Weapon" : weaponID.name)}";

        protected override bool _Evaluate() => Target.Weapon != null && weaponID == null || Target.Weapon.WeaponID == weaponID;
    }

    [System.Serializable, AddTypeMenu("Weapon Manager/Current Weapon Action")]
    public class C2_WM_WeaponAction : C2_MWeaponManagerConditions
    {
        [Tooltip("Weapon ID to check if is equipped")]
        public Weapon_Action CurrentAction = Weapon_Action.Attack;

        public override string DynamicName => $"WM: Current Action: {CurrentAction}";

        protected override bool _Evaluate() => Target.WeaponAction == CurrentAction;
    }


}