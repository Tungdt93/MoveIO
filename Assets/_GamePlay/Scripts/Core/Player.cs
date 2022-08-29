using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core
{
    using MoveStopMove.ContentCreation.Weapon;
    using MoveStopMove.Core.Character.NavigationSystem;
    using MoveStopMove.Core.Data;
    using MoveStopMove.Manager;
    public class Player : BaseCharacter
    {
        public const string P_SPEED = "PlayerSpeed";
        public const string P_WEAPON = "PlayerWeapon";

        public const string P_COLOR = "PlayerColor";
        public const string P_HAIR = "PlayerHair";
        public const string P_PANT = "PlayerPant";       
        public const string P_SET = "PlayerSet";

        public const string P_HIGHTEST_SCORE = "PlayerHighestScore";
        public const string P_CURRENT_REGION = "PlayerCurrentRegion";

        public GameData GameData;

        protected override void Awake()
        {
            base.Awake();
            Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
            LogicSystem.SetCharacterInformation(Data, gameObject.transform);
            WorldInterfaceSystem.SetCharacterInformation(Data);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LogicSystem.Event.SetTargetIndicatorPosition += SetIndicatorPosition;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            LogicSystem.Event.SetTargetIndicatorPosition -= SetIndicatorPosition;
        }

        protected void Start()
        {
            LevelManager.Inst.OnWinLevel += Win;
            VFX_Hit = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_Hit));
            VFX_AddStatus = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_AddStatus));
            VFX_Hit.Init(transform, Vector3.up * 0.5f, Quaternion.Euler(Vector3.zero), Vector3.one * 0.3f);
            VFX_AddStatus.Init(transform, Vector3.up * -0.5f, Quaternion.Euler(-90, 0, 0), Vector3.one);
            LoadData();
        }

        public override void OnInit()
        {
            base.OnInit();
            Data.Hp = 10;
            ((InputModule)NavigationModule).Active = true;
        }

        public override void OnDespawn()
        {
            
        }

        public override void ChangeColor(GameColor color)
        {
            base.ChangeColor(color);
            GameData.SetIntData(P_COLOR, ref GameData.Color, (int)color);
        }

        public override void ChangeHair(PoolID hair)
        {
            base.ChangeHair(hair);
            GameData.SetIntData(P_HAIR, ref GameData.Hair, (int)hair);
        }

        public override void ChangePant(PantSkin name)
        {
            base.ChangePant(name);
            GameData.SetIntData(P_PANT, ref GameData.Pant, (int)name);
        }

        public override void ChangeWeapon(BaseWeapon weapon)
        {
            base.ChangeWeapon(weapon);
            GameData.SetIntData(P_WEAPON, ref GameData.Weapon, (int)weapon.Name);
        }

        public void Win()
        {
            ((InputModule)NavigationModule).Active = false;
        }

        private void SetIndicatorPosition(Vector3 pos, bool active)
        {
            GameplayManager.Inst.TargetIndicator.SetActive(active);
            GameplayManager.Inst.TargetIndicator.transform.position = pos + Vector3.up * 0.1f;
        }

        private void LoadData()
        {
            
            Data.Speed = GameData.Speed;
            Data.Weapon = GameData.Weapon;

            Data.Color = GameData.Color;
            Data.Hair = GameData.Hair;
            Data.Pant = GameData.Pant;
            Data.Set = GameData.Set;

            ChangeColor((GameColor)Data.Color);
            ChangeHair((PoolID)Data.Hair);
            ChangePant((PantSkin)Data.Pant);

            if (Weapon != null)
            {
                PrefabManager.Inst.PushToPool(Weapon.gameObject, Weapon.Name);
            }

            GameObject newWeapon = PrefabManager.Inst.PopFromPool((PoolID)Data.Weapon);
            ChangeWeapon(Cache.GetBaseWeapon(newWeapon));
        }
    }
}