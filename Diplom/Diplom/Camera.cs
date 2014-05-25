using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplom
{
    public class Camera
    {
        Vector3 _position, _target, _up;
        float aspectRatio, nearClip, farClip;
        float rotationSpeed, translationSpeed, zoomSpeed;
        Matrix worldMatrix, viewMatrix, projectionMatrix;

        public float AspectRatio
        {
            get { return aspectRatio; }
            set 
            { 
                aspectRatio = value;
                projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1, aspectRatio, nearClip, farClip);
            }
        }

        public Vector3 Position { get { return _position; } }
        public Vector3 Up { get { return _up; } }

        public Matrix WorldMatrix { get { return worldMatrix; } }
        public Matrix ViewMatrix { get { return viewMatrix; } }
        public Matrix ProjectionMatrix { get { return projectionMatrix; } }

        public Camera(float aspectRatio, Vector3 position, Vector3 target)
        {
            _up = Vector3.Up;

            nearClip = 0.01f;
            farClip = 20000f;

            rotationSpeed = 0.01f;
            translationSpeed = 0.05f;
            zoomSpeed = 0.05f;

            this.aspectRatio = aspectRatio;
            this._position = position;
            this._target = target;

            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(position, target, _up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1, aspectRatio, nearClip, farClip);
        }

        public void RotateAroundTarget(float deltaX, float deltaY)
        {
            Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.Normalize(_position - _target), _up));
            Quaternion qRotate = Quaternion.CreateFromAxisAngle(right, deltaY * rotationSpeed) * 
                                 Quaternion.CreateFromAxisAngle(_up, -deltaX * rotationSpeed);
            _position = Vector3.Transform(_position - _target, qRotate) + _target;

            Vector3 newRight = Vector3.Normalize(Vector3.Cross(Vector3.Normalize(_position - _target), _up));
            if (Vector3.Dot(right, newRight) <= -0.94f) _up *= -1;

            viewMatrix = Matrix.CreateLookAt(_position, _target, _up);
        }

        public void MoveToTarget(float dist)
        {
            Vector3 direction = dist < 0 ? _position - _target : _target - _position;
            Matrix translationMatrix = Matrix.CreateTranslation(direction * zoomSpeed);
            _position = Vector3.Transform(_position, translationMatrix);
            viewMatrix = Matrix.CreateLookAt(_position, _target, _up);
        }

        public void Move(int deltaX, int deltaY)
        {
            Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.Normalize(_position - _target), _up));
            Vector3 up = Vector3.Normalize(Vector3.Cross(right, Vector3.Normalize(_position - _target)));
            Matrix translationMatrix = Matrix.CreateTranslation(right * translationSpeed * deltaX) *
                                       Matrix.CreateTranslation(up * translationSpeed * deltaY);
            _position = Vector3.Transform(_position, translationMatrix);
            _target = Vector3.Transform(_target, translationMatrix);
            viewMatrix = Matrix.CreateLookAt(_position, _target, _up);
        }

        public void LookAtSelection()
        {
            _target = Engine.ActiveControlAxis.Position;
            viewMatrix = Matrix.CreateLookAt(_position, _target, _up);
        }
    }
}
