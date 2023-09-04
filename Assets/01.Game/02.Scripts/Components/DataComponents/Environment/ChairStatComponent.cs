using System.Collections;
using System.Collections.Generic;
using Scripts.Components;
using UnityEngine;

namespace Scripts.Entities
{
    public class ChairStatComponent : DataComponent
    {
        public bool       IsOccupied;
        public BaseEntity Occupant;
        public Transform  Table;
    }
}