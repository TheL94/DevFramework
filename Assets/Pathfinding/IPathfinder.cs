using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pathfinding
{
    public interface IPathfinder
    {
        INetworkable Objective { get; set; }
        INetworkable CurrentNetworkable { get; set; }

        Path Path { get; set; }
    }
}
