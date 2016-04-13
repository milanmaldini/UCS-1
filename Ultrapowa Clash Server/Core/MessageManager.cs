using System;
using System.Collections.Concurrent;
using System.Threading;
using UCS.PacketProcessing;

namespace UCS.Core
{
    internal class MessageManager
    {
        private static ConcurrentQueue<Message> m_vPackets;
        private static readonly EventWaitHandle m_vWaitHandle = new AutoResetEvent(false);
        private bool m_vIsRunning;

        /// <summary>
        ///     The loader of the MessageManager class.
        /// </summary>
        public MessageManager()
        {
            m_vPackets = new ConcurrentQueue<Message>();
            m_vIsRunning = false;
        }

        /// <summary>
        ///     This function start the MessageManager.
        /// </summary>
        public void Start()
        {
            PacketProcessingDelegate packetProcessing = PacketProcessing;
            packetProcessing.BeginInvoke(null, null);
            m_vIsRunning = true;
            Console.WriteLine("[UCS]    Message manager has been successfully started !");
        }

        /// <summary>
        ///     This function process packets.
        /// </summary>
        private void PacketProcessing()
        {
            while (m_vIsRunning)
            {
                m_vWaitHandle.WaitOne();

                Message p;
                while (m_vPackets.TryDequeue(out p))
                {
                    var pl = p.Client.GetLevel();
                    var player = "";
                    if (pl != null)
                        player += " (" + pl.GetPlayerAvatar().GetId() + ", " + pl.GetPlayerAvatar().GetAvatarName() +
                                  ")";
                    try
                    {
                        Debugger.WriteLine("[UCS][" + p.GetMessageType() + "] Processing " + p.GetType().Name + player);
                        p.Decode();
                        p.Process(pl);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Debugger.WriteLine(
                            "[UCS][" + p.GetMessageType() + "] An exception occured during processing of message " +
                            p.GetType().Name + player, ex);
                        Console.ResetColor();
                    }
                }
            }
        }

        /// <summary>
        ///     This function handle the packet by enqueue him.
        /// </summary>
        /// <param name="p">The message/packet.</param>
        public static void ProcessPacket(Message p)
        {
            m_vPackets.Enqueue(p);
            m_vWaitHandle.Set();
        }

        private delegate void PacketProcessingDelegate();
    }
}