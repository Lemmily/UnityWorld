using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IUnit 
{
    bool IsIdle();
    void MoveTo(Vector3 destination);
}
