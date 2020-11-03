using UnityEngine;


namespace VoxelPlay.GPURendering.Instancing
{

    class BatchedMesh {
		public VoxelDefinition voxelDefinition;
		public Material material;
		public FastList<Batch> batches;

		public BatchedMesh(VoxelDefinition voxelDefinition) {
			this.voxelDefinition = voxelDefinition;
			batches = new FastList<Batch> ();
		}
	}
}
