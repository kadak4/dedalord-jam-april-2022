using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecitionManager
{
    event Action OnDecitionMade;
    event Action OnFinishedDecitions;
}
