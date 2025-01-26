using UnityEngine;

[CreateAssetMenu(fileName = "KeySO", menuName = "Scriptable Objects/KeySO")]
public class KeySO : ScriptableObject
{
    [Tooltip("Identifiant unique de la clé")]
    public string keyID; // Identifiant unique de la clé
    public Color keyColor; 
}
