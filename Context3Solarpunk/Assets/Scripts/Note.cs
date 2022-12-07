using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Note : MonoBehaviour
{
    [SerializeField, ResizableTextArea] private string notition;
}
