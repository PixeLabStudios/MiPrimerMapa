using UnityEngine;

public class ArtifactZoneManager : MonoBehaviour
{
    public EnemyArtefacto[] artifacts;
    public AnimalAI[] animalsInZone;

    private void Update()
    {
        if (AllDestroyed())
        {
            foreach (var animal in animalsInZone)
            {
                animal.isAggressive = false;
            }
            this.enabled = false;
        }
    }

    bool AllDestroyed()
    {
        foreach (var a in artifacts)
        {
            if (!a.IsDestroyed()) return false;
        }
        return true;
    }
}
