﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardManager 
{
    ICard GetCard(List<IStat> currentStats);
}
