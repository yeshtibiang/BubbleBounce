using UnityEngine;

[CreateAssetMenu(fileName = "KeySO", menuName = "Scriptable Objects/KeySO")]
public class KeySO : ScriptableObject
{
    [Tooltip("Identifiant unique de la cl�")]
    public string keyID; // Identifiant unique de la cl�
    public Color keyColor; 
}
