using System;
using System.IO;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x206
    internal class BuyResourcesCommand : Command
    {
        private readonly object m_vCommand;
        private readonly bool m_vIsCommandEmbedded;
        private readonly int m_vResourceCount;
        private readonly int m_vResourceId;
        private readonly int Unknown1;

        public BuyResourcesCommand(BinaryReader br)
        {
            m_vResourceId = br.ReadInt32WithEndian();
            m_vResourceCount = br.ReadInt32WithEndian();
            m_vIsCommandEmbedded = br.ReadBoolean();
            if (m_vIsCommandEmbedded)
            {
                Depth++;

                if (Depth >= MaxEmbeddedDepth)
                    throw new ArgumentException(
                        "A command contained embedded command depth was greater than max embedded commands.");
                m_vCommand = CommandFactory.Read(br);
            }
            Unknown1 = br.ReadInt32WithEndian();
        }

        public override void Execute(Level level)
        {
            var rd = (ResourceData) ObjectManager.DataTables.GetDataById(m_vResourceId);
            if (rd != null)
            {
                if (m_vResourceCount >= 1)
                {
                    if (!rd.PremiumCurrency)
                    {
                        var avatar = level.GetPlayerAvatar();
                        var diamondCost = GamePlayUtil.GetResourceDiamondCost(m_vResourceCount, rd);
                        var unusedResourceCap = avatar.GetUnusedResourceCap(rd);
                        if (m_vResourceCount <= unusedResourceCap)
                        {
                            if (avatar.HasEnoughDiamonds(diamondCost))
                            {
                                avatar.UseDiamonds(diamondCost);
                                avatar.CommodityCountChangeHelper(0, rd, m_vResourceCount);
                                if (m_vIsCommandEmbedded)
                                {
                                    Depth++;

                                    if (Depth >= MaxEmbeddedDepth)
                                        throw new ArgumentException(
                                            "A command contained embedded command depth was greater than max embedded commands.");

                                    ((Command) m_vCommand).Execute(level);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}