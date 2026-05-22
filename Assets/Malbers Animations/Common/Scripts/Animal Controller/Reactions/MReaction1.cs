using UnityEngine;

namespace MalbersAnimations.Controller.Reactions
{
    public class MReaction : ScriptableObject
    {
        public bool active = true;

        public float delay = 0f;

        public string fullName => name;

        [TextArea]
        public string description;

        public virtual void React(MAnimal animal)
        {
        }
    }
}