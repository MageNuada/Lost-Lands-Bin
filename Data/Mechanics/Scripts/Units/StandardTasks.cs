//Don't modify names!

void Idle(Entity unit, ControlledUnitPrototype.UnitTask currentTask)
{
    if(unit as UnitPrototype == null || (unit as UnitPrototype).Intellect as BaseNpcIntellect == null) return;

    BaseNpcIntellect ai = (unit as UnitPrototype).Intellect as BaseNpcIntellect;
    ai.IdleStartPosition = (unit as UnitPrototype).CurrentPosition;
    ai.DoTask(new BaseIntellect.Motivation(BaseIntellect.Motivation.Types.Idle, (unit as UnitPrototype).CurrentPosition,
        BaseIntellect.Motivation.DefaultPriority), BaseIntellect.TaskQueue.ClearAndNew);
}

void Patrol(Entity unit, ControlledUnitPrototype.UnitTask currentTask)
{
    _internalPointsMove(unit, currentTask, BaseIntellect.Motivation.Types.Patrol);
}

void Assault(Entity unit, ControlledUnitPrototype.UnitTask currentTask)
{
    _internalPointsMove(unit, currentTask, BaseIntellect.Motivation.Types.Assault);
}

void TestTask(Entity unit, ControlledUnitPrototype.UnitTask currentTask)
{
    BaseNpcIntellect ai = (unit as UnitPrototype).Intellect as BaseNpcIntellect;
    ai.DoTask(new BaseIntellect.Motivation(BaseIntellect.Motivation.Types.SpecialAnimation, World.Instance.Random.Next(10), 100),
        BaseIntellect.TaskQueue.ClearAndNew);
}


//internal functions

void _internalPointsMove(Entity unit, ControlledUnitPrototype.UnitTask currentTask, BaseIntellect.Motivation.Types mt)
{
    MapCurve curve = currentTask.TargetEntity as MapCurve;
    if(curve == null || unit as UnitPrototype == null || (unit as UnitPrototype).Intellect as BaseNpcIntellect == null) return;

    MapCurvePoint waypoint = ((unit as UnitPrototype).Intellect as BaseNpcIntellect).CurrentWayPoint;
    if(waypoint == null || waypoint.Owner != curve)
        ((unit as UnitPrototype).Intellect as BaseNpcIntellect).CurrentWayPoint = curve.Points[0];

    (unit as UnitPrototype).Intellect.DoTask(new BaseIntellect.Motivation(mt, curve.Points[0].Position,
        BaseIntellect.Motivation.DefaultPriority), BaseIntellect.TaskQueue.ClearAndNew);
}