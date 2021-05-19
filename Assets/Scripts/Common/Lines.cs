using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lines : MonoBehaviour
{
    public int initialLines = 20;
    public int maxLines = 40;
    public Material material;

    static Lines instance;
    static public Lines Instance { get => instance; }

    int numLines = 0;
    List<LineRenderer> lineRenderers = new List<LineRenderer>();

    private void Awake()
    {
        instance = this;
    }

	private void OnEnable()
	{
        RenderPipelineManager.endCameraRendering += OnResetRender;
    }

	void Start()
    {
        for (int i = 0; i < initialLines; i++)
		{
            lineRenderers.Add(CreateLineRenderer());
        }
    }

	public void Reset()
	{
        foreach (LineRenderer lineRenderer in lineRenderers)
		{
            lineRenderer.gameObject.SetActive(false);
        }
    }

	public void AddLine(Vector3 start, Vector3 end, Color color, float width = 0.1f)
	{
        LineRenderer lineRenderer = GetInactiveLineRenderer();
        if (lineRenderer == null)
        {
            lineRenderer = CreateLineRenderer();
            lineRenderers.Add(lineRenderer);
        }

        lineRenderer.gameObject.SetActive(true);

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    LineRenderer GetInactiveLineRenderer()
	{
        foreach (LineRenderer lineRenderer in lineRenderers)
		{
            if (!lineRenderer.gameObject.activeSelf)
			{
                return lineRenderer;
			}
		}

        return null;
	}

    LineRenderer CreateLineRenderer()
	{
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = transform;
        gameObject.AddComponent<LineRenderer>();
        gameObject.SetActive(false);
        gameObject.name = "Line" + (numLines + 1);
        numLines++;

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;

        return lineRenderer;
    }

    private void OnResetRender(ScriptableRenderContext context, Camera camera)
    {
        if(Application.isPlaying)
        {
            Reset();
        }
    }
}
