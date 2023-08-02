using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/WindowStaticData", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}