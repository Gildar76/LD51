using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public interface ISpawnable 
    {
        public Spawner Spawner { get; set; }
    }
}
