using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTileTests
{
    [Test]
    public void MousedUp()
    {
        var test = new GameObject().AddComponent<ClickableTile>();

        test.Start();
        test.OnMouseUp();

        if (GSC.gameType == 1)
        {
            if (GSC.data.clickedTile != null && GSC.moving)
                Assert.AreEqual(GSC.data.clickedTile.GetComponent<Renderer>().materia, GSC.data.moveTileUnselected);
            else if (GSC.data.clickedTile != null && GSC.attacking)
                Assert.AreEqual(GSC.data.clickedTile.GetComponent<Renderer>().materia, GSC.data.attackTile);
            if (GSC.selectedUnit != null && !EventSystem.current.IsPointerOverGameObject())
            {
                if (GSC.attacking)
                {
                    Assert.AreEqual(GSC.data.clickedTile, GSC.data.attackSpaces[new Vector2(tileX, tileY)]);
                    Assert.AreEqual(GSC.data.clickedTile.GetComponent<Renderer>().material, GSC.data.moveTileSelected);
                    Assert.AreEqual(GSC.selectedUnit.GetComponent<Unit>().attackRange, GSC.selectedUnit.GetComponent<Unit>().currentPath.Count - 1);
                    Assert.AreEqual(GSC.selectedUnit.GetComponent<Unit>().currentPath, null);
                }
                else if (GSC.moving)
                {
                    Assert.AreEqual(GSC.data.clickedTile, GSC.data.moveSpaces[new Vector2(tileX, tileY)]);
                    Assert.AreEqual(GSC.data.clickedTile.GetComponent<Renderer>().material, GSC.data.moveTileSelected);
                }
            }
        }
        else
        {
            if (map.clickedTile != null && GSC.moving)
                Assert.AreEqual(map.clickedTile.GetComponent<Renderer>().material, map.moveTileUnselected);
            else if (map.clickedTile != null && GSC.attacking)
                Assert.AreEqual(map.clickedTile.GetComponent<Renderer>().material, map.attackTile);

            if (map.SelectedUnit != null && !EventSystem.current.IsPointerOverGameObject())
            {
                if (GSC.attacking)
                {
                    Assert.AreEqual(map.clickedTile, map.attackSpaces[new Vector2(tileX, tileY)]);
                    Assert.AreEqual(map.clickedTile.GetComponent<Renderer>().material, map.moveTileSelected);
                    Assert.AreEqual(map.SelectedUnit.GetComponent<Unit>().attackRange, map.SelectedUnit.GetComponent<Unit>().currentPath.Count - 1);
                    Assert.AreEqual(map.SelectedUnit.GetComponent<Unit>().currentPath, null);
                }
                else
                if (GSC.moving)
                {
                    Assert.AreEqual(map.clickedTile, map.moveSpaces[new Vector2(tileX, tileY)]);
                    Assert.AreEqual(map.clickedTile.GetComponent<Renderer>().material, map.moveTileSelected);
                }
            }
        }
    }
}
