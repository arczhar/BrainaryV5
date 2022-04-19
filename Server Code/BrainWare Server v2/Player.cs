using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWare_Server_v2
{
    public class Player : BasePlayer
    {
        public int PlayerId;
        public bool UserSuccessLoadScene;
        public int PlayerScore = 0;
    }
}
