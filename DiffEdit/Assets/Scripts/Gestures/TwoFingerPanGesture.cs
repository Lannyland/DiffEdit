using System;
using System.Collections.Generic;
using TouchScript.Utils;
using UnityEngine;
using TouchScript.Clusters;

namespace TouchScript.Gestures
{
    /// <summary>
    /// Recognizes cluster movement.
    /// </summary>
    [AddComponentMenu("TouchScript/Gestures/Two Finger Pan Gesture")]
    public class TwoFingerPanGesture : TwoClusterTransform2DGestureBase
    {
        #region Private variables

        [SerializeField]
        private float movementThreshold = 0.5f;

        private Vector2 movementBuffer;
        private bool isMoving = false;

        #endregion

        #region Public properties

        /// <summary>
        /// Minimum distance in cm for cluster to move to be considered as a possible gesture. 
        /// </summary>
        public float MovementThreshold
        {
            get { return movementThreshold; }
            set { movementThreshold = value; }
        }

        /// <summary>
        /// 3D delta position in global coordinates.
        /// </summary>
        public Vector3 WorldDeltaPosition { get; private set; }

        /// <summary>
        /// 3D delta position in local coordinates.
        /// </summary>
        public Vector3 LocalDeltaPosition { get; private set; }

        // Added two new properties
        public Vector2 FingerPositionBegin { get; private set; }
        public Vector2 FingerPositionEnd { get; private set; }

        #endregion

        #region Gesture callbacks

        /// <inheritdoc />
        protected override void touchesMoved(IList<TouchPoint> touches)
        {
            base.touchesMoved(touches);

            if (!clusters.HasClusters) return;

            var globalDelta3DPos = Vector3.zero;
            var localDelta3DPos = Vector3.zero;
            Vector3 oldGlobalCenter3DPos, oldLocalCenter3DPos, newGlobalCenter3DPos, newLocalCenter3DPos;

            var old2DPos1 = clusters.GetPreviousCenterPosition(Clusters2.CLUSTER1);
            var old2DPos2 = clusters.GetPreviousCenterPosition(Clusters2.CLUSTER2);
            var new2DPos1 = clusters.GetCenterPosition(Clusters2.CLUSTER1);
            var new2DPos2 = clusters.GetCenterPosition(Clusters2.CLUSTER2);

            Vector2 oldCenter2DPos = (old2DPos1 + old2DPos2) * .5f;
            Vector2 newCenter2DPos = (new2DPos1 + new2DPos2) * .5f;

            FingerPositionBegin = oldCenter2DPos;
            FingerPositionEnd = newCenter2DPos;

            if (isMoving)
            {
                oldGlobalCenter3DPos = ProjectionUtils.CameraToPlaneProjection(oldCenter2DPos, projectionCamera, WorldTransformPlane);
                newGlobalCenter3DPos = ProjectionUtils.CameraToPlaneProjection(newCenter2DPos, projectionCamera, WorldTransformPlane);
                globalDelta3DPos = newGlobalCenter3DPos - oldGlobalCenter3DPos;
                oldLocalCenter3DPos = globalToLocalPosition(oldGlobalCenter3DPos);
                newLocalCenter3DPos = globalToLocalPosition(newGlobalCenter3DPos);
                localDelta3DPos = newLocalCenter3DPos - globalToLocalPosition(oldGlobalCenter3DPos);
            } else
            {
                movementBuffer += newCenter2DPos - oldCenter2DPos;
                var dpiMovementThreshold = MovementThreshold*manager.DotsPerCentimeter;
                if (movementBuffer.sqrMagnitude > dpiMovementThreshold*dpiMovementThreshold)
                {
                    isMoving = true;
                    oldGlobalCenter3DPos = ProjectionUtils.CameraToPlaneProjection(oldCenter2DPos - movementBuffer, projectionCamera, WorldTransformPlane);
                    newGlobalCenter3DPos = ProjectionUtils.CameraToPlaneProjection(newCenter2DPos, projectionCamera, WorldTransformPlane);
                    globalDelta3DPos = newGlobalCenter3DPos - oldGlobalCenter3DPos;
                    oldLocalCenter3DPos = globalToLocalPosition(oldGlobalCenter3DPos);
                    newLocalCenter3DPos = globalToLocalPosition(newGlobalCenter3DPos);
                    localDelta3DPos = newLocalCenter3DPos - globalToLocalPosition(oldGlobalCenter3DPos);
                } else
                {
                    newGlobalCenter3DPos = ProjectionUtils.CameraToPlaneProjection(newCenter2DPos - movementBuffer, projectionCamera, WorldTransformPlane);
                    newLocalCenter3DPos = globalToLocalPosition(newGlobalCenter3DPos);
                    oldGlobalCenter3DPos = newGlobalCenter3DPos;
                    oldLocalCenter3DPos = newLocalCenter3DPos;
                }
            }

            if (globalDelta3DPos != Vector3.zero)
            {
                switch (State)
                {
                    case GestureState.Possible:
                    case GestureState.Began:
                    case GestureState.Changed:
                        PreviousWorldTransformCenter = oldGlobalCenter3DPos;
                        WorldTransformCenter = newGlobalCenter3DPos;
                        WorldDeltaPosition = globalDelta3DPos;
                        PreviousWorldTransformCenter = oldGlobalCenter3DPos;
                        LocalTransformCenter = newLocalCenter3DPos;
                        LocalDeltaPosition = localDelta3DPos;
                        PreviousLocalTransformCenter = oldLocalCenter3DPos;

                        if (State == GestureState.Possible)
                        {
                            setState(GestureState.Began);
                        } else
                        {
                            setState(GestureState.Changed);
                        }
                        break;
                }
            }
        }

        /// <inheritdoc />
        protected override void reset()
        {
            base.reset();
            WorldDeltaPosition = Vector3.zero;
            LocalDeltaPosition = Vector3.zero;
            movementBuffer = Vector2.zero;
            isMoving = false;
        }

        #endregion
    }
}