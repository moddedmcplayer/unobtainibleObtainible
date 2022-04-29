using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace unobtainibleObtainible.Patches
{
    [HarmonyPatch(typeof(CandyPink), nameof(CandyPink.SpawnChanceWeight), MethodType.Getter)]
    internal static class PinkCandyPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4);
            newInstructions.RemoveAt(index);
            if (Plugin.Instance.RAChance != -1f)
            {
                newInstructions.InsertRange(index, new[]
                {
                    new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                    new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.RAChance))),
                });
            }
            else
            {
                newInstructions.InsertRange(index, new[]
                {
                    new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                    new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                    new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.weight))),
                });
            }

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}