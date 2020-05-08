using UnityEngine;

namespace Puppet
{
    public sealed partial class Dancer
    {
        public float footDistance {
            get => _footDistance;
            set => _footDistance = value;
        }

        public float stepFrequency {
            get => _stepFrequency;
            set => _stepFrequency = value;
        }

        public float stepHeight {
            get => _stepHeight;
            set => _stepHeight = value;
        }

        public float stepAngle {
            get => _stepAngle;
            set => _stepAngle = value;
        }

        public float maxDistance {
            get => _maxDistance;
            set => _maxDistance = value;
        }

        public float hipHeight {
            get => _hipHeight;
            set => _hipHeight = value;
        }

        public float hipPositionNoise {
            get => _hipPositionNoise;
            set => _hipPositionNoise = value;
        }

        public float hipRotationNoise {
            get => _hipRotationNoise;
            set => _hipRotationNoise = value;
        }

        public float spineBend {
            get => _spineBend;
            set => _spineBend = value;
        }

        public Vector3 spineRotationNoise {
            get => _spineRotationNoise;
            set => _spineRotationNoise = value;
        }

        public Vector3 handPosition {
            get => _handPosition;
            set => _handPosition = value;
        }

        public Vector3 handPositionNoise {
            get => _handPositionNoise;
            set => _handPositionNoise = value;
        }

        public float headMove {
            get => _headMove;
            set => _headMove = value;
        }

        public float noiseFrequency {
            get => _noiseFrequency;
            set => _noiseFrequency = value;
        }

        public uint randomSeed {
            get => _randomSeed;
            set => _randomSeed = value;
        }
    }
}
