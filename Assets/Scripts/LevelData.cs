using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    [SerializeField] [Range(5,20)] private int Width = 10;
    [SerializeField] [Range(5,20)] private int Height = 10;
    
    public string GetPrompt()
    {
        var prompt = @"Welcome to our linking game! This game is played on a 10x10 board with various objects located on it. The 0's on the board represent empty spaces. To make the game more challenging, some sections of the board contain empty spaces, and the difficulty level varies depending on the section. The objects are identified by ID numbers 1-5, and players must match them with their own object IDs in order to pop them. The objective of the game varies from level to level, for example, in the first level, you might need to pop 10 of the number 1 objects, or 5 of the number 2 objects and 6 of the number 3 objects, and so on.

        The difficulty level of the game is calculated using the following formula:

        Difficulty level =  ((board width * board height) * (number of objects that need to be collected) - (empty spaces * 100))

        The empty spaces can uses just one row. You dont need to use empty spaces for evert time. it is not mandatory.

            And the board should look aesthetically pleasing and attractive. instead of complexity, there should be nice patterns.

            Can you design a level around 2500 points for me with this formula? and please give me the board. Can you please give me a brief and concise answer without any unnecessary details or explanations?
            Please give me a brief and to-the-point answer without any additional details.

         
            Because I will use this information at our level editor. I just need raw data.

            thanks";
        
        return $"Turkiyenin baskenti neresidir?{System.Environment.NewLine}Türkiye'nin başkenti Ankara'dır.{System.Environment.NewLine}Türkiye'nin başkenti neresidir?{System.Environment.NewLine}";
    }
}
