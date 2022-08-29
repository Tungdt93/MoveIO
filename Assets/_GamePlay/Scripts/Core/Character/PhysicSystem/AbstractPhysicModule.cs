using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.PhysicSystem
{
    public abstract class AbstractPhysicModule : AbstractModuleSystem<PhysicData,PhysicParameter>,IInit
    {

        protected PhysicData Data;
        protected PhysicParameter Parameter;
        public override void Initialize(PhysicData Data,PhysicParameter Parameter)
        {
            this.Data = Data;
            this.Parameter = Parameter;
        }
        public abstract void SetVelocity(Vector3 velocity);
        public abstract void SetRotation(GameConst.Type type,Quaternion rotation);
        public abstract void SetSmoothRotation(GameConst.Type type, Vector3 direction);

        public abstract void SetScale(GameConst.Type type, Vector3 scale);
        public abstract void SetScale(GameConst.Type type, float ratio);
        public abstract void SetActive(bool value);

        public abstract void OnInit();

    }
}
