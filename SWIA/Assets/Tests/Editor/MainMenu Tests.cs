using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTests
{

    [Test]
    public void campaignChooserChangeslevelname()
    {
        var menu = new GameObject().AddComponent<mainmenu>();

        // Assign
        menu.campaignChoser(0);
        string name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "Campaign1Level1");

        // Assign
        menu.campaignChoser(0);
        name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "Campaign2Level1");

        // Assign
        menu.campaignChoser(0);
        name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "Campaign3Level1");
    }


    [Test]
    public void skirmishChooserChangeslevelname()
    {
        var menu = new GameObject().AddComponent<mainmenu>();

        // Assign
        menu.skirmishChoser(0);
        string name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "SkirmishLevel1");

        // Assign
        menu.skirmishChoser(0);
        name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "SkirmishLevel2");

        // Assign
        menu.skirmishChoser(0);
        name = menu.lvlName;

        // Assert
        Assert.AreEqual(name, "SkirmishLevel3");
    }

    [Test]
    public void difficultyChangesWhenButtonSelectedInSkirmish()
    {

        var test = new GameObject().AddComponent<mainmenu>();
        var testGSC = new GameObject().AddComponent<GameStateController>();
        
        test.skirmishChoser(0);

        int diff = testGSC.gameType;
        Assert.AreEqual(0, diff);

        test.skirmishChoser(1);

        diff = testGSC.gameType;
        Assert.AreEqual(1, diff);

        test.skirmishChoser(2);

        diff = testGSC.gameType;
        Assert.AreEqual(2, diff);

        test.skirmishChoser(-1);

        diff = testGSC.gameType;
        Assert.AreEqual(0, diff);
    }

    [Test]
    public void difficultyChangesWhenButtonSelectedInCampaign()
    {

        var test = new GameObject().AddComponent<mainmenu>();
        var testGSC = new GameObject().AddComponent<GameStateController>();

        test.campaignChoser(0);

        int diff = testGSC.gameType;
        Assert.AreEqual(0, diff);

        test.campaignChoser(1);

        diff = testGSC.gameType;
        Assert.AreEqual(1, diff);

        test.campaignChoser(2);

        diff = testGSC.gameType;
        Assert.AreEqual(2, diff);

        test.campaignChoser(-1);

        diff = testGSC.gameType;
        Assert.AreEqual(0, diff);
    }
}