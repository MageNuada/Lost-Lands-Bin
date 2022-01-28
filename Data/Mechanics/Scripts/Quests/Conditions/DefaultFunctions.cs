public bool StartQuestById(int id)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(QuestsManager.Instance.GetQuest(id), JournalNoteStatus.Active);
}

public bool FinishQuestById(int id)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(QuestsManager.Instance.GetQuest(id), JournalNoteStatus.Complited);
}

public bool FailQuestById(int id)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(QuestsManager.Instance.GetQuest(id), JournalNoteStatus.Failed);
}

public bool StartQuestByStage(Rpg.QuestStage stage)
{
	return QuestsManager.Instance.SetQuestOrQuestStage(stage.OwnerQuest, JournalNoteStatus.Active);
}

public bool FinishQuestByStage(Rpg.QuestStage stage, JournalNoteStatus status)
{
	return QuestsManager.Instance.SetQuestOrQuestStage(stage.OwnerQuest, status);
}

public bool FailQuestByStage(Rpg.QuestStage stage)
{
	return QuestsManager.Instance.SetQuestOrQuestStage(stage.OwnerQuest, JournalNoteStatus.Failed);
}

public bool StartQuestStage(Rpg.QuestStage stage)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(stage, JournalNoteStatus.Active);
}

public bool FinishQuestStage(Rpg.QuestStage stage, JournalNoteStatus status)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(stage, status);
}

public bool FailQuestStage(Rpg.QuestStage stage)
{
    return QuestsManager.Instance.SetQuestOrQuestStage(stage, JournalNoteStatus.Failed);
}

T CreateEntity<T>(string typeName) where T : Entity
{
	T e = (T) Entities.Instance.Create(typeName, Map.Instance);
	e.PostCreate();
	return e;
}

T CreateEntity<T>(EntityType type) where T : Entity
{
	T e = (T) Entities.Instance.Create(type, Map.Instance);
	e.PostCreate();
	return e;
}

//this stage will not be called specified amount of seconds in the OnTick event
void Sleep(float seconds, Rpg.QuestStage stage)
{
    stage.Sleep(seconds);
}

//example of serialization for game save/load
int exampleField1 = 0;
string exampleField2 = "abc";

protected override void OnSave()
{
	SerializationValues["exampleField1"] = exampleField1.ToString();
	SerializationValues["exampleField2"] = exampleField2;
	SerializationValues["ratCounter"] = ratCounter.ToString();

	base.OnSave();
}

protected override void OnLoad()
{
	base.OnLoad();
	
	if(SerializationValues.ContainsKey("exampleField1"))
		exampleField1 = int.Parse(SerializationValues["exampleField1"]);
	if(SerializationValues.ContainsKey("exampleField2"))
		exampleField2 = SerializationValues["exampleField2"];
	if (SerializationValues.ContainsKey("ratCounter"))
		ratCounter = int.Parse(SerializationValues["ratCounter"]);
}

protected override void OnTick()
{
	base.OnTick();
	
	exampleField1 = exampleField1 % 10 + 1;
}