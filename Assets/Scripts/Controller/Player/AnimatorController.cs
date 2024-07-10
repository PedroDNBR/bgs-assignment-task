using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        Animator anim;
        AnimationClip animationClip;
        AnimatorOverrideController animatorOverrideController;
        AnimationClipOverrides defaultClipAnimations;

        public GameObject[] BodyParts;

        private void Start()
        {
            anim = GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = animatorOverrideController;

            defaultClipAnimations = new AnimationClipOverrides(animatorOverrideController.overridesCount);
            animatorOverrideController.GetOverrides(defaultClipAnimations);
        }

        public void UpdateAnimationAxis(float horizontal, float vertical)
        {
            if (horizontal <= -0.49)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            anim.SetFloat("Horizontal", horizontal);
            anim.SetFloat("Vertical", vertical);
        }
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    { 
        public AnimationClipOverrides(int capacity) : base(capacity) { }   

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1) 
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}