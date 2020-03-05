using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Unit : MonoBehaviour
{
	GameStateController GSC;

	public int tileX;
	public int tileY;

	// Unit specifics
	public int unit;
	public int maxHealth, health;
	public int maxEndurance, endurance;
	public int movement, MaxMovemment, remainingMovement;
	public bool isMobile;
	public int[] defDice;
	public WeaponType weapon;
	public int[] strength;
	public int[] insight;
	public int[] tech;
	public Sprite playerImage;
	public Sprite playerCard;
	public int actions;
	public int attackRange, minRange, maxRange, midRange;
	public bool bleed = false, stun = false, weaken = false, focused = false, hidden = false;

	// Our pathfinding info. Null if we have no destination ordered.
	public TileMap map;
	public List<Node> currentPath = null;
	Vector3 newPos;

	private void Start()
	{
		actions = 2;
		GSC = FindObjectOfType<GameStateController>();
		newPos = transform.position;
	}

	void Update()
	{
		// Draw our debug line showing the pathfinding!
		// NOTE: This won't appear in the actual game view.
		if (currentPath != null)
		{
			int currNode = 0;

			while (currNode < currentPath.Count - 1)
			{

				Vector3 start = new Vector3(currentPath[currNode].x + (int)GSC.data.minX, currentPath[currNode].y + (int)GSC.data.minY, 0) +
					new Vector3(0, 0, -0.5f);
				Vector3 end = new Vector3(currentPath[currNode + 1].x + (int)GSC.data.minX, currentPath[currNode + 1].y + (int)GSC.data.minY, 0) +
					new Vector3(0, 0, -0.5f);

				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}
		}

		// Have we moved our visible piece close enough to the target tile that we can
		// advance to the next step in our pathfinding?
		if (Vector3.Distance(transform.position, new Vector3(tileX, tileY, 0)) < 0.1f)
			AdvancePathing();

		// Smoothly animate towards the correct map tile.
		transform.position = Vector3.Lerp(transform.position, newPos, 5f * Time.deltaTime);

		// Change rotation when moving
		//transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, Quaternion.LookRotation(newDirection - currDirection, Vector3.back) , 3f * Time.deltaTime);
	}

	// Advances our pathfinding progress by one tile.
	void AdvancePathing()
	{
		if (currentPath == null)
			return;

		if (remainingMovement <= 0)
			return;

		// Teleport us to our correct "current" position, in case we
		// haven't finished the animation yet.
		transform.position = new Vector3(tileX, tileY, 0);

		// Get cost from current tile to next tile
		if (GSC.gameType == 1)
			remainingMovement -= (int)GSC.path.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y, true);
		else
			remainingMovement -= (int)map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

		// Move us to the next tile in the sequence
		if (GSC.gameType == 1)
		{
			tileX = currentPath[1].x + (int)GSC.data.minX;
			tileY = currentPath[1].y + (int)GSC.data.minY;
		}
		else
		{
			tileX = currentPath[1].x;
			tileY = currentPath[1].y;
		}

		newPos = new Vector3(tileX, tileY, 0);
		// Remove the old "current" tile from the pathfinding list
		currentPath.RemoveAt(0);

		if (currentPath.Count == 1)
		{
			// We only have one tile left in the path, and that tile MUST be our ultimate
			// destination -- and we are standing on it!
			// So let's just clear our pathfinding info.
			movement = remainingMovement;
			currentPath = null;
			remainingMovement = 0;
		}
	}

	// The "Next Turn" button calls this.
	public void NextTurn()
	{
		remainingMovement = movement;

		// Make sure to wrap-up any outstanding movement left over.
		while (currentPath != null && remainingMovement > 0)
		{
			AdvancePathing();
		}

		// Remove move spaces
		if (GSC.gameType == 1)
		{
			foreach (KeyValuePair<Vector2, GameObject> go in GSC.data.moveSpaces)
			{
				GSC.data.clickables[new Vector2(go.Value.transform.position.x, go.Value.transform.position.y)].GetComponent<BoxCollider2D>().enabled = false;
				Destroy(go.Value);
			}
		}
		else
		{
			foreach (KeyValuePair<Vector2, GameObject> go in map.moveSpaces)
			{
				map.clickables[new Vector2(go.Value.transform.position.x, go.Value.transform.position.y)].GetComponent<BoxCollider>().enabled = false;
				Destroy(go.Value);
			}
		}

	}

	public void NextTurnEnemy()
	{
		remainingMovement = MaxMovemment;
		Debug.Log("Starting movement = " + remainingMovement);
		// Make sure to wrap-up any outstanding movement left over.
		while (currentPath != null && remainingMovement > 0)
		{
			AdvancePathing();
		}

		Debug.Log("movement left = " + remainingMovement);

	}

	public void CheckForDeath(GameObject uiDeath, GameStateController owner)
	{
		if (health <= 0)
		{
			for (int i = 0; i < uiDeath.transform.childCount; i++)
			{
				if (uiDeath.transform.GetChild(i).GetComponent<Image>().sprite == this.playerImage)
				{
					owner.activated[i] = -1;
				}
			}

			this.gameObject.transform.position = new Vector3(1000, 1000, 1000);
			this.gameObject.SetActive(false);
		}
	}

	public void CheckForDeathEnemy(GameStateController owner)
	{
		if (health <= 0)
		{
			for (int i = 0; i < owner.enemies.Count; i++)
			{

				if (GameObject.ReferenceEquals(this.gameObject, owner.enemies[i]))
				{
					owner.enemies.RemoveAt(i);
					Debug.Log("removing enemy from array");
					break;
				}
			}

			this.gameObject.transform.position = new Vector3(1000, 1000, 1000);
			this.gameObject.SetActive(false);
		}
	}
}
