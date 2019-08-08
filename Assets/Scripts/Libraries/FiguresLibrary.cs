using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniverseTeam.Libraries
{
    [CreateAssetMenu(fileName = "FiguresLibrary", menuName = "Library/FiguresLibrary")]
    public class FiguresLibrary : ScriptableObject
    {
        [Header("Observe line size! (20 symbol)")]
        [SerializeField] [TextArea(5, int.MaxValue)] private List<string> examples;

        public List<string> Exaples => examples;
        public int SymbolPerLine { get; } = 20;
    }
}