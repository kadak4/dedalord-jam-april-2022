using System.Collections.Generic;

public interface ICardManager 
{
    ICard GetCard(List<IStat> currentStats);
}
