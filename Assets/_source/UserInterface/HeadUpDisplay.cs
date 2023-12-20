using UnityEngine;

namespace UserInterface
{
    public class HeadUpDisplay : MonoBehaviour
    {
        [field: SerializeField] public ChooseTowerPanel ChooseTowerPanel { get; private set; }
    }
}