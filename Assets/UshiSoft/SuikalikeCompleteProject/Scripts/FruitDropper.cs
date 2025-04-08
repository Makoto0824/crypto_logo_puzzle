using UnityEngine;
using UnityEngine.EventSystems;

namespace UshiSoft
{
    public class FruitDropper : MonoBehaviour
    {
        [SerializeField] private NextFruitManager _nextFruitManager;

        [SerializeField] private FruitGenerator _fruitGenerator;

        [SerializeField] private LineRenderer _trajectoryLine;

        [SerializeField] private float _dropY = 5f;

        [SerializeField] private float _moveRange = 5f;
        [SerializeField] private float _wallOffset = 0.01f;

        private Fruit _fruit;

        private bool _dragging;
        private float _prevDragX;
        private float _dropX;

        private void Start()
        {
            _nextFruitManager.ResetNextFruits();

            GenerateFruit();
        }

        private void Update()
        {
            MouseControl();
        }

        private void FixedUpdate()
        {
            UpdateFruitPose();
            UpdateTrajectoryLine();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(new Vector3(-_moveRange / 2f, _dropY), new Vector3(_moveRange / 2f, _dropY));
        }

        public void ChangeFruits()
        {
            _nextFruitManager.ResetNextFruits();

            DestroyFruit();

            GenerateFruit();
        }

        public void GameOver()
        {
            DestroyFruit();

            HideTrajectoryLine();
        }

        private void MouseControl()
        {
            if (IsMouseDown() && !IsMouseOverUI())
            {
                var mousePos = GetMousePosition();
                var worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
                _prevDragX = worldMousePos.x;

                _dragging = true;
            }

            if (_dragging)
            {
                var mousePos = GetMousePosition();
                var worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
                _dropX += worldMousePos.x - _prevDragX;
                _prevDragX = worldMousePos.x;
            }

            if (IsMouseUp() && _dragging)
            {
                DropFruit();
                
                _dragging = false;
            }
        }

        private bool IsMouseDown()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }

            return false;
        }

        private bool IsMouseUp()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    return true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                return true;
            }

            return false;
        }

        private Vector3 GetMousePosition()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                return touch.position;
            }

            return Input.mousePosition;
        }

        private bool IsMouseOverUI()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            return false;
        }

        private void GenerateFruit()
        {
            var fruitSize = _nextFruitManager.GetNextFruit();
            _fruit = _fruitGenerator.Generate(fruitSize);

            _fruit.OnHit.AddListener(OnFruitHit);
            _fruit.DisablePhysics();
            _fruit.IgnoreGameOverCollider = true;

            _dropX = 0f;

            UpdateFruitPose();

            ShowTrajectoryLine();
        }

        private void DestroyFruit()
        {
            if (_fruit == null)
            {
                return;
            }

            _fruit.OnHit.RemoveAllListeners();
            Destroy(_fruit.gameObject);
            _fruit = null;
        }

        private void OnFruitHit(Fruit fruit)
        {
            fruit.OnHit.RemoveAllListeners();

            fruit.IgnoreGameOverCollider = false;

            GenerateFruit();
        }

        private void DropFruit()
        {
            if (_fruit == null)
            {
                return;
            }

            _fruit.EnablePhysics();

            _fruit = null;

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySE("DropFruit");
            }

            HideTrajectoryLine();
        }

        private void UpdateFruitPose()
        {
            if (_fruit == null)
            {
                return;
            }

            var minX = -_moveRange / 2f - _fruit.LocalBoundsLeft + _wallOffset;
            var maxX = _moveRange / 2f - _fruit.LocalBoundsRight - _wallOffset;
            _dropX = Mathf.Clamp(_dropX, minX, maxX);

            _fruit.transform.position = new Vector3(_dropX, _dropY);
        }

        private void UpdateTrajectoryLine()
        {
            if (_trajectoryLine == null)
            {
                return;
            }

            _trajectoryLine.transform.position = new Vector3(_dropX, 0f);
        }

        private void ShowTrajectoryLine()
        {
            if (_trajectoryLine == null)
            {
                return;
            }

            _trajectoryLine.enabled = true;
            
            UpdateTrajectoryLine();
        }

        private void HideTrajectoryLine()
        {
            if (_trajectoryLine == null)
            {
                return;
            }

            _trajectoryLine.enabled = false;
        }
    }
}