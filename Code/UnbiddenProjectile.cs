using Terraria.ModLoader;

namespace UnbiddenMod
{
    public class UnbiddenProjectile : GlobalProjectile
  {
    public override bool InstancePerEntity => true;
    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place
    
  }
}