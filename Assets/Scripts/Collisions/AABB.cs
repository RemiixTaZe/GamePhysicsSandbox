using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AABB
{
	public Vector2 center { get; set; }
	public Vector2 size { get; set; }
	public Vector2 extents { get => size * 0.5f; }

	public Vector2 min { get => center - extents; }
	public Vector2 max { get => center + extents; }

	public AABB(Vector2 center, Vector2 size)
	{
		this.center = center;
		this.size = size;
	}

	public bool Contains(Vector2 point)
    {
		return (point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y);
    }

	public bool Contains(AABB aabb)
    {
		return (aabb.max.x >= min.x && aabb.min.x <= max.x && aabb.max.y >= min.y && aabb.min.y <= max.y);

	}

	public void Draw(Color color, float width = 0.05f)
    {
		Lines.Instance.AddLine(new Vector2(min.x, min.y), new Vector2(max.x, min.y), color, width);
		Lines.Instance.AddLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y), color, width);
		Lines.Instance.AddLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y), color, width);
		Lines.Instance.AddLine(new Vector2(max.x, min.y), new Vector2(max.x, max.y), color, width);
    }

	public void SetMinMax(Vector2 min, Vector2 max) 
	{ 
		size = (max - min); 
		center = min + extents; 
	}

	public void Expand(Vector2 point) 
	{ 
		SetMinMax(Vector2.Min(min, point), Vector2.Max(max, point));
	}

	public void Expand(AABB aabb) 
	{ 
		SetMinMax(Vector2.Min(min, aabb.min), Vector2.Max(max, aabb.max));
	}
}
