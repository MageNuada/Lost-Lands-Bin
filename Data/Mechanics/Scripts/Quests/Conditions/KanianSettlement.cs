void IncreaseExp(string val)
{
	foreach(var unit in PlayerCreatures)
		unit.IncreaseExperience(uint.Parse(val));
}

bool InitWalkIntoVillage(Rpg.QuestStage stage)
{
	var obj = stage.ConditionsObjects.FirstOrDefault(o => o is UnitPrototype) as MapObject;
	if(obj != null)
		AddHighlight(obj, stage);
	return true;
}

bool FinalizeWalkIntoVillage(Rpg.QuestStage stage)
{
	var obj = stage.ConditionsObjects.FirstOrDefault(o => o is UnitPrototype) as MapObject;
	if(obj != null)
		RemoveHighlight(obj);
	return true;
}

int DlgItemAvailableByAnother(Rpg.DialogSystemBase dsb, Player p)
{
    string val = null;
    if (dsb.ActionTypes.TryGetValue(DialogAnswerAction.AdditionalData, out val) && !string.IsNullOrEmpty(val))
    {
        foreach (var s in val.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries))
        {
            int uin = 0;
            if (!int.TryParse(s, out uin)) return -1;
            var note = p.PlayerJournalNotes[dsb.DataType, uin];
            if (note == null || note.Status != JournalNoteStatus.Disabled)
                return -1;
        }
    }

    return 50;
}

int DlgItemQuestsDone(Rpg.DialogSystemBase dsb, Player p)
{
    string val = null;
    if (dsb.ActionTypes.TryGetValue(DialogAnswerAction.AdditionalData, out val) && !string.IsNullOrEmpty(val))
    {
        foreach (var s in val.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries))
        {
            int uin = 0;
            if (!int.TryParse(s, out uin)) return -1;
            var note = p.PlayerJournalNotes[QuestNoteType.QuestType, uin];
            if (note != null && note.Status != JournalNoteStatus.Complited)
                return -1;
        }
    }

    return 49;
}

int DlgItemQuestsNotDone(Rpg.DialogSystemBase dsb, Player p)
{
    return DlgItemQuestsDone(dsb, p) > 0 ? -1 : 1;
}

int PlayerHasItemsInInventory(Rpg.DialogSystemBase dsb, Player p)
{
    string val = null;
    if (dsb.ActionTypes.TryGetValue(DialogAnswerAction.AdditionalData, out val) && !string.IsNullOrEmpty(val))
    {
        var tuple = val.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (tuple.Length != 2) return -1;
        int dbId, count;
        if (!int.TryParse(tuple[0], out dbId) || !int.TryParse(tuple[1], out count)) return -1;
        int itemCount = 0;
        foreach (var item in p.GetAllInventoryItems())
        {
            if (item == null || item.Item == null) continue;
            if (item.Item.DataBaseID == dbId) itemCount += item.Count;
            if (itemCount >= count) return itemCount;
        }
    }
    return -1;
}

public bool KillRatsPreStart(Rpg.QuestStage stage)
{
    UnitPrototype.CreatureDiedStatic += (new StageHelper(stage)).RatKilled;

    return true;
}

public bool KillRatsFinalize(Rpg.QuestStage stage)
{
    var helper = StageHelper.GetHelper(stage);
    if (helper == null) return true;
    UnitPrototype.CreatureDiedStatic -= helper.RatKilled;
    helper.Dispose();

    return true;
}

static int ratCounter = 0;

public partial class StageHelper
{
    public void RatKilled(UnitPrototype sender, UnitPrototype killer)
    {
        if (PlayerCreatures.Contains(killer))
        {
            if (ratCounter++ < 6) return;

            UnitPrototype.CreatureDiedStatic -= RatKilled;
            QuestsManager.Instance.SetQuestOrQuestStage(this.Stage, JournalNoteStatus.Complited);
            QuestsManager.Instance.SetQuestOrQuestStage(this.Stage.OwnerQuest, JournalNoteStatus.Complited);
            Dispose();
        }
    }
}