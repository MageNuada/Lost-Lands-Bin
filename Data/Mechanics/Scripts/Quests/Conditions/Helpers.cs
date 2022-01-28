static void AddHighlight(MapObject o, Rpg.QuestStage stage)
{
    if(o == null || stage == null || !Player.Instance.IsQuestStageStarted(stage)) return;
	string name = QuestLight.GetNameForObject(o, Vec3.Zero);
	QuestLight ql = Entities.Instance.GetByName(name) as QuestLight;
	if(ql != null) return;
    ql = (QuestLight)Entities.Instance.Create("QuestLight", Map.Instance);
    ql.Init(o, Vec3.Zero);
    ql.PostCreate();
}

static void AddHighlight(Vec3 pos, Rpg.QuestStage stage)
{
    if(pos == Vec3.Zero || stage == null || !Player.Instance.IsQuestStageStarted(stage)) return;
	string name = QuestLight.GetNameForObject(null, pos);
	QuestLight ql = Entities.Instance.GetByName(name) as QuestLight;
	if(ql != null) return;
    ql = (QuestLight)Entities.Instance.Create("QuestLight", Map.Instance);
	ql.Name = name;
    ql.Init(null, pos);
    ql.PostCreate();
}

static void RemoveHighlight(MapObject o)
{
    if(o == null) return;
    QuestLight.Remove(o, Vec3.Zero);
}

static void RemoveHighlight(Vec3 pos)
{
    if(pos == Vec3.Zero) return;
    QuestLight.Remove(null, pos);
}

public partial class StageHelper: IDisposable
{
    private StageHelper() {}

    public StageHelper(Rpg.QuestStage stage)
    {
        Stage = stage;
        allStageHelpers.Add(this);
    }

    public void Dispose()
    {
        allStageHelpers.Remove(this);
        //Dispose();
    }

    public static StageHelper GetHelper(Rpg.QuestStage stage)
    {
        return allStageHelpers.Find(helper => helper.Stage == stage);
    }

    public Rpg.QuestStage Stage { get; set; }

    private static readonly List<StageHelper> allStageHelpers = new List<StageHelper>();
}