using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorField : Force
{
    public FloatData noiseOffset;
    public FloatData noiseScale;
    public FloatData forceMagnitude;
    public BoolData enable;
    public BoolData showDebug;

    float prevNoiseOffset;
    float prevNoiseScale;

    Vector2[,] grid;
    Vector2 gridSize;
    float cellSize;

    void Start()
    {
        CreateGrid();
    }

	private void Update()
	{
        if (!enable) return;

		if (showDebug)
		{
            Draw();
		}

        if (noiseOffset != prevNoiseOffset ||
            noiseScale != prevNoiseOffset)
		{
            CreateGrid();
		}

        prevNoiseOffset = noiseOffset;
        prevNoiseScale = noiseScale;
    }

	public override void ApplyForce(Body body)
    {
        if (!enable) return;

        Vector2 position = body.position;
        position += World.Instance.AABB.extents;

        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1)) return;

        Vector2 force = grid[x, y] * forceMagnitude;
        body.AddForce(force, Body.eForceMode.Acceleration);
    }

    private void CreateGrid()
	{
        gridSize = World.Instance.AABB.size;
        grid = new Vector2[Mathf.CeilToInt(gridSize.x), Mathf.CeilToInt(gridSize.y)];

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                float px = noiseOffset + (x * noiseScale);
                float py = noiseOffset + (y * noiseScale);
                float angel = Mathf.PerlinNoise(px,py) * Mathf.PI * 2;

                Vector2 direction = Vector2.zero;
                direction.x = Mathf.Cos(angel);
                direction.y = Mathf.Sin(angel);

                grid[x, y] = direction;
            }
        }
    }

    private void Draw()
	{
        AABB aabb = World.Instance.AABB;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            float xp = x + aabb.min.x;
            Lines.Instance.AddLine(new Vector2(xp, aabb.min.y), new Vector2(xp, aabb.max.y), Color.white, 0.05f);
        }
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            float yp = y + aabb.min.y;
            Lines.Instance.AddLine(new Vector2(aabb.min.x, yp), new Vector2(aabb.max.x, yp), Color.white, 0.05f);
        }

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Vector2 direction = grid[x, y];
                Vector2 position = aabb.min + new Vector2(x + 0.5f, y + 0.5f);

                Lines.Instance.AddLine(position, position + direction * 0.5f, Color.green, 0.025f);
            }
        }
    }
}
