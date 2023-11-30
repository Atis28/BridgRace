using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	[SerializeField] private Transform bricksParent; // brick gennerate

	private List<Vector3> brickPoints = new List<Vector3>();
	private List<Vector3> emptyPoint = new List<Vector3>();
	private List<PlatformBrick> bricks = new List<PlatformBrick>();

	private List<Vector3> spawnBrickPos = new List<Vector3>();

	private void Start() {
		for (int i = -Constant.BRICK_ROWS  + 1; i  < Constant.BRICK_ROWS; i += 2) {
			for (int j = -Constant.BRICK_COLS  + 1; j  < Constant.BRICK_COLS; j += 2) {
				brickPoints.Add(bricksParent.position + new Vector3(i, 0, j));

			}
		}
		OnInit();
	}

	public void OnInit() {
		foreach (var item in brickPoints) {
			emptyPoint.Add(item);	
		}
	}

	public void InitColor(ColorType colorType) {
		int amount = brickPoints.Count;

		for (int i = 0; i < amount; i++)
		{
			NewBrick(colorType);
			//NewBrick(ColorType.Red);
		}
	}
	public void NewBrick(ColorType colorType)
	{
		if (emptyPoint.Count > 0)
		{
			int rand = Random.Range(0, emptyPoint.Count);
			PlatformBrick brick = SimplePool.Spawn<PlatformBrick>(PoolType.PlatformBrick, emptyPoint[rand], Quaternion.identity);
			brick.stage = this;

            // Generate a random ColorType
            ColorType randomColor = (ColorType)Random.Range(3, 8);

            //brick.ChangeColor(colorType);
			brick.ChangeColor(randomColor);
			brick.transform.SetParent(bricksParent);
			emptyPoint.RemoveAt(rand);
			bricks.Add(brick);
		}
	}
	
	public void RemoveBrick(PlatformBrick brick)
	{
		emptyPoint.Add(brick.tf.position);
		bricks.Remove(brick);
	}

}
