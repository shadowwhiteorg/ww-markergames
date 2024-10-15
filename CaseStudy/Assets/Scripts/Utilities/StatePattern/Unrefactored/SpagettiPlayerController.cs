using System;
using UnityEngine;

namespace ww.Portfolio.DesignPatterns.StatePattern
{
    public enum StudentState
    {
        Study,
        Sleep,
        Play
    }

    internal class SpagettiPlayerController : MonoBehaviour
    {
        private StudentState state;

        private void Update()
        {
            InputController();

            switch (state)
            {
                case StudentState.Study:
                    Study();
                    break;
                case StudentState.Sleep:
                    Sleep();
                    break;
                case StudentState.Play:
                    Play();
                    break;
                default:
                    break;
            }
        }

        private void InputController()
        {
            // Input handler that directs student states
        }

        private void Play()
        {
            // Playing with phone logic
        }

        private void Sleep()
        {
            // Sleep logic
        }

        private void Study()
        {
            // Study logic
        }

    }
}