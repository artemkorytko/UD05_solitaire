using UnityEngine;
using Zenject;

namespace Solitaire
{
    public class PlayerController : ITickable, IInitializable
    {
        private readonly Camera _camera;
        private PlayingCard _holdCard;
        private LayerMask _layerMask;

        private Vector3 _startCardPosition;
        public PlayerController(Camera camera)
        {
            _camera = camera;
        }
        
        public void Initialize()
        {
            _layerMask = LayerMask.GetMask("PlayingCard");
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryHoldCard();
            }

            if (Input.GetMouseButton(0) && _holdCard)
            {
                MoveCard();
            }

            if (Input.GetMouseButtonUp(0) && _holdCard)
            {
                ReleaseCard();
            }
        }
        
        private void TryHoldCard()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit,  float.MaxValue, _layerMask))
            {
                var card = hit.collider.GetComponent<PlayingCard>();
                if (card && card.IsOpen)
                {
                    _holdCard = card;
                    _startCardPosition = _holdCard.transform.position;
                    _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World);
                }
            }
        }
        
        private void MoveCard()
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = _holdCard.transform.position.z;
            _holdCard.transform.position = worldPosition;
        }
        
        private void ReleaseCard()
        {
            //TODO: add release logic
            _holdCard.transform.position = _startCardPosition;
        }
    }
}