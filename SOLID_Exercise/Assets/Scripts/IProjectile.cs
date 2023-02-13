using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    int _Damage { get; }
    IShapeBehaviour _Owner { get; }
}
