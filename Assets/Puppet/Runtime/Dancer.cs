using UnityEngine;
using Unity.Mathematics;

namespace Puppet
{
    public sealed partial class Dancer : MonoBehaviour
    {
        #region Editable attributes

        [SerializeField] float _footDistance = 0;
        [SerializeField] float _stepFrequency = 0;
        [SerializeField] float _stepHeight = 0;
        [SerializeField] float _stepAngle = 0;
        [SerializeField] float _maxDistance = 0;

        [SerializeField] float _hipHeight = 0;
        [SerializeField] float _hipPositionNoise = 0;
        [SerializeField] float _hipRotationNoise = 0;

        [SerializeField] float _spineBend = 0;
        [SerializeField] Vector3 _spineRotationNoise = Vector3.zero;

        [SerializeField] Vector3 _handPosition = Vector3.zero;
        [SerializeField] Vector3 _handPositionNoise = Vector3.zero;

        [SerializeField] float _headMove = 0;

        [SerializeField] float _noiseFrequency = 0;
        [SerializeField] uint _randomSeed = 0;

        #endregion

        #region Private variables

        Animator _animator;

        // Chest bone matrix cache
        // Used to calculate the hand positions. Bone matrices are not
        // accessible from OnAnimatorIK, so we cache them in Update.
        float4x4 _chestMatrix;

        #endregion

        #region MonoBehaviour implementation

        void Start()
        {
            _animator = GetComponent<Animator>();

            InitializeAnimationParameters();
        }

        void Update()
        {
            UpdateAnimationParameters();

            // Update the chest matrix cache.
            var chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
            _chestMatrix = chest.localToWorldMatrix;
        }

        void OnAnimatorIK(int layerIndex)
        {
            // Feet
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootPosition);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootPosition);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

            var footr = PivotFootRotation;
            var lfw = GetFootRotationWeight(Side.Left);
            var rfw = GetFootRotationWeight(Side.Right);
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, footr);
            _animator.SetIKRotation(AvatarIKGoal.RightFoot, footr);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lfw);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rfw);

            // Body (hip)
            _animator.bodyPosition = GetBodyPosition();
            _animator.bodyRotation = GetBodyRotation();

            // Spine
            var spine = GetSpineRotation();
            _animator.SetBoneLocalRotation(HumanBodyBones.Spine, spine);
            _animator.SetBoneLocalRotation(HumanBodyBones.Chest, spine);
            _animator.SetBoneLocalRotation(HumanBodyBones.UpperChest, spine);

            // Hands
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandPosition);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandPosition);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

            // Left wrist and fingers
            var lfinger = GetFingerRotation(Side.Left);
            _animator.SetBoneLocalRotation(HumanBodyBones.LeftHand, lfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.LeftIndexProximal, lfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.LeftIndexIntermediate, lfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.LeftIndexDistal, lfinger);

            // Right wrist and fingers
            var rfinger = GetFingerRotation(Side.Right);
            _animator.SetBoneLocalRotation(HumanBodyBones.RightHand, rfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.RightIndexProximal, rfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.RightIndexIntermediate, rfinger);
            _animator.SetBoneLocalRotation(HumanBodyBones.RightIndexDistal, rfinger);

            // Head
            _animator.SetLookAtPosition(GetLookAtPosition());
            _animator.SetLookAtWeight(1);
        }

        #endregion
    }
}
