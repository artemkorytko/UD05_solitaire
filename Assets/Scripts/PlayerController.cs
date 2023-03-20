using Solitaire.Signals;
using UnityEngine;
using Zenject;

namespace Solitaire
{
    public class PlayerController : ITickable, IInitializable
    {
        private readonly Camera _camera;
        private readonly SignalBus _signalBus;
        private PlayingCard _holdCard;
        private LayerMask _layerMask;

        private Vector3 _startCardPosition;

        private Vector3 _offset;

        public PlayerController(Camera camera, SignalBus signalBus)
        {
            _camera = camera;
            _signalBus = signalBus;
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
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                var card = hit.collider.GetComponent<PlayingCard>();
                if (card && card.IsOpen)
                {
                    _holdCard = card;
                    _startCardPosition = _holdCard.transform.position;
                    _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World);
                    _offset = _startCardPosition - hit.point;
                }
            }
        }

        private void MoveCard()
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = _holdCard.transform.position.z;
            _holdCard.transform.position = worldPosition + _offset;
        }

        private void ReleaseCard()
        {
            var checkPoint = _holdCard.transform.position;
            var ray = new Ray(checkPoint, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                var cardPlace = hit.collider.GetComponent<CardPlace>();
                if (cardPlace && cardPlace.IsCanConnect(_holdCard))
                {
                    
                    if (cardPlace.IsMain)
                    {
                        _signalBus.Fire(new AddToMainStackSignal(_holdCard.CardType, _holdCard.Value));
                    }

                    if (_holdCard.IsMain && !cardPlace.IsMain)
                    {
                        _signalBus.Fire(new RemoveFromMainStackSignal(_holdCard.CardType, _holdCard.Value));
                    }

                    _holdCard.SetParent(cardPlace);
                    if (cardPlace.IsMain)
                    {
                        var pos = cardPlace.transform.position;
                        pos.z -= 0.1f;
                        _holdCard.transform.position = pos;
                    }
                    if (_holdCard.IsInDeck)
                    {
                        _signalBus.Fire(new ExcludeFromDeckSignal(_holdCard));
                    }
                }
                else
                {
                    _holdCard.SetParent();
                }
            }
            else
            {
                _holdCard.SetParent();
            }

            _holdCard = null;
        }
    }
}