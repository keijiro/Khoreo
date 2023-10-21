using UnityEngine;
using Unity.Mathematics;
using Klak.Math;

namespace Puppet
{

public sealed partial class Dancer
{
    #region Animation variables

    XXHash _hash;
    float3 _noise;

    // Foot positions
    float3[] _feet = new float3[2];

    // Global step parameter
    float _step;

    // Direction flip variable
    float _stepFlip = 1;

    #endregion

    #region Local enum

    // Left / Right
    enum Side { Left, Right }

    #endregion

    #region Utility functions

    static float3 Up => math.float3(0, 1, 0);

    static float  deg2rad(float  x) => math.radians(x);
    static float3 deg2rad(float3 x) => math.radians(x);

    #endregion

    #region Foot animation

    // Step count: What number the current step is
    int StepCount => (int)_step;

    // Step parameter: Progress of the current step
    float StepParam => _step - math.floor(_step);

    // Random seed of the current step
    uint StepSeed => (uint)StepCount * 100;

    // Which side the current/next pivot is on
    Side PivotSide => (Side)(StepCount & 1);
    Side NonPivotSide => (Side)((StepCount & 1) ^ 1);

    // Angle of the pivot rotation of the current step
    float StepAngle
      => _hash.Float(0.5f, 1.0f, StepSeed + 1) * _stepAngle * _stepFlip;

    // Pivot rotation at the end of the current step
    quaternion StepRotationFull
      => quaternion.RotateY(deg2rad(StepAngle));

    // Pivot rotation at the current frame
    quaternion StepRotation
      => quaternion.RotateY(deg2rad(StepAngle * StepParam));

    // The original height of the foot point
    float3 FootBias => Up * _animator.leftFeetBottomHeight;

    // Foot position
    float3 GetFootPosition(Side side)
    {
        var thisFoot = _feet[(int)side];
        var thatFoot = _feet[(int)side ^ 1];

        // If it's the pivot foot, return it immediately.
        if (side == PivotSide) return thisFoot + FootBias;

        // Horizontal move: Rotation around the pivot foot
        var rp = math.mul(StepRotation, thisFoot - thatFoot);

        // Horizontal move: Cosine wave (open - close - open)
        rp *= math.cos(StepParam * math.PI * 2) * 0.3f + 0.7f;

        // Vertical move: Sine wave with smooth step
        var offs = math.sin(math.smoothstep(0, 1, StepParam) * math.PI);
        offs *= _stepHeight;

        return thatFoot + rp + Up * offs + FootBias;
    }

    float3 LeftFootPosition => GetFootPosition(Side.Left);
    float3 RightFootPosition => GetFootPosition(Side.Right);

    // Direction vector of the current step
    float3 CurrentStepDirection
      => math.normalize(_feet[(int)NonPivotSide] - _feet[(int)PivotSide]);

    // Destination point of the current step
    float3 CurrentStepDestination
      => _feet[(int)PivotSide]
         + math.mul(StepRotationFull, CurrentStepDirection) * _footDistance;

    // Rotation of the pivot foot
    quaternion PivotFootRotation
      => quaternion.LookRotation
           (math.cross(RightFootPosition - LeftFootPosition, Up), Up);

    // Weight parameter for each foot
    float GetFootRotationWeight(Side side)
    {
        var p = (_step + (int)side) % 2;
        var down = math.smoothstep(1, 1.1f, p);
        var up = math.smoothstep(1.9f, 2, p);
        return math.max(1 - down, up) * 0.9f;
    }

    #endregion

    #region Lower body animation

    // Body (hip) position
    float3 GetBodyPosition()
    {
        // Horizontal move: Sine interpolation between the left/right feet
        var param = (1 - math.sin((_step % 2) * math.PI)) / 2;
        var pos = math.lerp(LeftFootPosition, RightFootPosition, param);

        // Vertical move: Two waves for a step
        var dy1 = math.cos(StepParam * math.PI * 4) * _stepHeight / 2;

        // Vertical noise
        var dy2 = noise.snoise(_noise) * _hipPositionNoise;

        return math.float3(pos.x, _hipHeight + dy1 + dy2, pos.z);
    }

    // Body (hip) rotation
    quaternion GetBodyRotation()
    {
        // Base offset
        var r1 = quaternion.RotateY(-math.PI / 2);

        // Right direction vector based on the foot positions
        var right = RightFootPosition - LeftFootPosition;
        right.y = 0;
        right = math.normalize(right);

        // Horizontal rotation
        var r2 = quaternion.LookRotation(right, Up);

        // Noise
        var r3 = Noise.Rotation(_noise, deg2rad(_hipRotationNoise), 0);

        return math.mul(math.mul(r1, r2), r3);
    }

    #endregion

    #region Upper body animation

    // Spine (spine/chest/upper chest) rotation
    quaternion GetSpineRotation()
    {
        // Constant bending
        var r1 = quaternion.RotateX(deg2rad(_spineBend));

        // Noise
        var r2 = Noise.Rotation(_noise, deg2rad(_spineRotationNoise), 1);

        return math.mul(r1, r2);
    }

    // Hand positions
    float3 GetHandPosition(Side side)
    {
        // Relative position
        var pos = (float3)_handPosition;
        if (side == Side.Left) pos.x *= -1;

        // Noise
        pos += Noise.Float3(_noise, 2 + (uint)side) * _handPositionNoise;

        // Chest transform
        pos = math.mul(_chestMatrix, math.float4(pos, 1)).xyz;

        return pos;
    }

    float3 LeftHandPosition => GetHandPosition(Side.Left);
    float3 RightHandPosition => GetHandPosition(Side.Right);

    // Finger rotations
    quaternion GetFingerRotation(Side side)
      => quaternion.Euler
           (Noise.Float3(_noise, 4 + (uint)side)
            * math.float3(0.7f, 0.3f, 0.3f) + math.float3(0.35f, 0, 0));

    // Look at position (for head movement)
    float3 GetLookAtPosition()
    {
        // Z plane constraint noise
        var pos = Noise.Float3(_noise, 6) * _headMove;
        pos.z = 2;

        // Body transform
        pos = math.mul(_animator.bodyRotation, pos);
        pos += (float3)_animator.bodyPosition;

        return pos;
    }

    #endregion

    #region Common methods

    void InitializeAnimationParameters()
    {
        // Random number/noise generators
        _hash = new XXHash(_randomSeed);

        // Initial foot positions
        var origin = transform.position * math.float3(1, 0, 1);
        var foot = (float3)transform.right * _footDistance / 2;
        _feet[0] = origin - foot;
        _feet[1] = origin + foot;
    }

    void UpdateAnimationParameters()
    {
        var dt = Time.deltaTime;

        // Update the noise parameter.
        _noise += _hash.Float3(0.9f, 1.1f, 0) * _noiseFrequency * dt;

        // Step parameter delta
        var delta = _stepFrequency * dt;

        // Check if the current step still continues.
        if (StepCount == (int)(_step + delta))
        {
            // The current step is still going:

            // Recalculate the non-pivot foot position.
            _feet[(int)NonPivotSide] =
              _feet[(int)PivotSide] + CurrentStepDirection * _footDistance;

            // Update the step parameter.
            _step += delta;
        }
        else
        {
            // The current step ends in this frame:

            // Put the foot at the destination.
            _feet[(int)NonPivotSide] = CurrentStepDestination;

            // Update the step parameter.
            _step += delta;

            // Randomly flip the direction.
            if (_hash.Float(StepSeed) > 0.5f) _stepFlip *= -1;

            // Check if the next destination is in the maxDistance range.
            var origin = transform.position;
            var dist = math.distance(CurrentStepDestination, origin);
            if (dist > _maxDistance)
            {
                // Try flipping the turn direction. Revert it if it makes the
                // destination further from the origin.
                _stepFlip *= -1;
                if (dist < math.distance(CurrentStepDestination, origin))
                    _stepFlip *= -1;
            }
        }
    }

    #endregion
}

}
