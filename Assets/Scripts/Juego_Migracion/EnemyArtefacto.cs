using UnityEngine;

public class EnemyArtefacto : MonoBehaviour
{
    public int hitsToDestroy = 5;
    private int currentHits = 0;
    private bool isDestroyed = false;

    public delegate void OnArtifactDestroyed();
    public event OnArtifactDestroyed ArtifactDestroyed;

    public void ReceiveHit()
    {
        if (isDestroyed) return;

        currentHits++;
        if (currentHits >= hitsToDestroy)
        {
            isDestroyed = true;
            ArtifactDestroyed?.Invoke();
            Destroy(gameObject); // o jugar animación de destrucción
        }
    }

    public bool IsDestroyed() => isDestroyed;
}
