#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA2235 // Mark all non-serializable fields

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Depra.Toolkit.Serialization.SerializeReferenceExtensions.Example.ExampleAssets.Scripts.Runtime
{
    public class Example : MonoBehaviour
    {
        // The type that implements ICommand will be displayed in the popup.
        [SerializeReference, SubclassSelector] private ICommand _command;

        // Collection support.
        [SerializeReference, SubclassSelector] private ICommand[] _commands = Array.Empty<ICommand>();

        private void Start()
        {
            _command?.Execute();

            foreach (ICommand command in _commands)
            {
                command?.Execute();
            }
        }

        // Nested type support
        [Serializable]
        public class NestedCommand : ICommand
        {
            public void Execute()
            {
                Debug.Log("Execute NestedCommand");
            }
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    [Serializable]
    public class DebugCommand : ICommand
    {
        [SerializeField] private string _message;

        public void Execute()
        {
            Debug.Log(_message);
        }
    }

    [Serializable]
    public class InstantiateCommand : ICommand
    {
        [SerializeField] private GameObject _prefab;

        public void Execute()
        {
            Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        }
    }

    // Menu override support.
    [AddTypeMenu("Example/Add Type Menu Command")]
    [Serializable]
    public class AddTypeMenuCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("Execute AddTypeMenuCommand");
        }
    }

    [Serializable]
    public struct StructCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("Execute StructCommand");
        }
    }
}