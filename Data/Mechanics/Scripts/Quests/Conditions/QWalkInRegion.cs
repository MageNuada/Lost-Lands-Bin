public bool InRegionPreStart(Rpg.QuestStage stage)
{
    for(int j = 0; j < stage.ConditionsObjects.Length; j++)
        if(stage.ConditionsObjects[j] is Region)
        {
            Region r = (stage.ConditionsObjects[j] as Region);
            r.Enabled = true;
            AddHighlight(r, stage);
            return true;
        }
    return false;
}

public bool InRegionStart(Rpg.QuestStage stage)
{
    for(int j = 0; j < stage.ConditionsObjects.Length; j++)
        if(stage.ConditionsObjects[j] is Region)
        {
            Region r = (stage.ConditionsObjects[j] as Region);
            r.Enabled = true;
            AddHighlight(r, stage);
            return true;
        }
    Log.Info("Stage " + stage.Name + " started.");
    return true;
}

public bool InRegionTick(Rpg.QuestStage stage)
{
    for(int i = 0; i < PlayerCreatures.Count; i++)
        for(int j = 0; j < stage.ConditionsObjects.Length; j++)
            if(stage.ConditionsObjects[j] is Region)
                if(((Region)stage.ConditionsObjects[j]).ObjectsInRegion.Contains(PlayerCreatures[i]))
                    return true;
    return false;
}

public bool InRegionEnd(Rpg.QuestStage stage)
{
    for(int j = 0; j < stage.ConditionsObjects.Length; j++)
        if(stage.ConditionsObjects[j] is Region)
        {
            Region r = (stage.ConditionsObjects[j] as Region);
            r.Enabled = false;
            RemoveHighlight(stage.ConditionsObjects[j] as MapObject);
            return true;
        }
    return false;
}