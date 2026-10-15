using Unity.Collections;
using Unity.Entities;

// Makes water flow instant. WaterSpreadingSystem only spreads a water spreader
// once its timer elapses (1.0-2.0 s per tile). Here every spreader's timer is
// restarted with zero lifespan right before that system runs, so it spreads on
// the next frame and the whole flood resolves in a few frames.
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(WaterSpreadingSystem))]
public class InstantWaterSpreadSystem : SystemBase
{
	protected override void OnUpdate()
	{
		double now = World.Time.ElapsedTime;
		EntityQuery query = GetEntityQuery(ComponentType.ReadWrite<WaterSpreaderCD>());
		NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp);
		for (int i = 0; i < entities.Length; i++)
		{
			WaterSpreaderCD spreader = EntityManager.GetComponentData<WaterSpreaderCD>(entities[i]);
			spreader.timer.Start(now, 0f);
			EntityManager.SetComponentData(entities[i], spreader);
		}
		entities.Dispose();
	}
}
