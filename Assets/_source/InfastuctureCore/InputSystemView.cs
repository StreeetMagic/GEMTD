using System;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.InputServices;
using UnityEngine;

namespace InfastuctureCore
{
    public class InputSystemView : MonoBehaviour
    {
        private IInputService InputService => ServiceLocator.Instance.Get<IInputService>();
    }
}