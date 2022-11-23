using Unity.AI.Navigation;
using UnityEngine;

public class NavigationBaker : MonoBehaviour
{
    private NavMeshSurface surface;

	private void Start()
	{
		surface = GetComponent<NavMeshSurface>();
	}

	private void RefreshNavMesh()
    {
        surface.BuildNavMesh();
    }
}