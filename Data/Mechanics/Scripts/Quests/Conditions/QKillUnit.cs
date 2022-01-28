public bool KillUnitStart(Rpg.QuestStage stage)
{
    Log.Info("Stage " + stage.Name + " started.");
    for(int j = 0; j < stage.ConditionsObjects.Length; j++)
    {            
        UnitPrototype u = (stage.ConditionsObjects[j] as UnitPrototype);
        if(u == null) continue;
        AddHighlight(u, stage);

        if(StageHelper.GetHelper(stage) != null) return true;
        u.CreatureDied += (new StageHelper(stage)).CreatureKilled;

        return true;
    }
    return true;
}

public bool KillUnitFinalize(Rpg.QuestStage stage)
{
	for(int j = 0; j < stage.ConditionsObjects.Length; j++)
    {            
        UnitPrototype u = (stage.ConditionsObjects[j] as UnitPrototype);
        if(u == null) continue;
        RemoveHighlight(u);
        return true;
    }
	return true;
}

public bool KillUnitPreStart(Rpg.QuestStage stage)
{
	//указываем 1-ого юнита в списке юнитов для стадии квеста
    for(int j = 0; j < stage.ConditionsObjects.Length; j++)
    {            
        UnitPrototype u = (stage.ConditionsObjects[j] as UnitPrototype);
        if(u == null) continue;
        AddHighlight(u, stage);

        if(StageHelper.GetHelper(stage) != null) return true;
        u.CreatureDied += (new StageHelper(stage)).CreatureKilled;

        return true;
    }
    return false;
}

public partial class StageHelper
{
    public void CreatureKilled(UnitPrototype sender, UnitPrototype killer)
    {
        sender.CreatureDied -= CreatureKilled;
        RemoveHighlight(sender);
        if(PlayerCreatures.Contains(killer))
        {
            QuestsManager.Instance.SetQuestOrQuestStage(Stage, JournalNoteStatus.Complited);
            Dispose();
        }
        else
        {
            QuestsManager.Instance.SetQuestOrQuestStage(Stage, JournalNoteStatus.Failed);
            Dispose();
        }
    }
}