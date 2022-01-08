using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public static class PasswordDictionary
    {
        public static Dictionary<int, string> goodPasswords =  new Dictionary<int, string>()
        {
            {1919, "Give me Banana Chilly"},
            {1920, "I wanna see a tiger"},
            {1921, "Joe sent me"},
            {1922, "Strike me dead"},
            {1923, "May I see Brown's Rex?"},
            {1924, "Can you play Rhapsody in Blue?"},
            {1925, "Make me some Sugar Moon"},
            {1926, "Do a little Charleston"},
            {1927, "Did you get that call from London?"},
            {1928, "Isadora's silk scarf"},
            {1929, "A Farewell to Arms"},
            {1930, "May I have blind power?"},
            {1931, "I would have Dracula juice on rocks"},
            {1932, "I heard you make mean Brick of wine"},
            {1933, "Painkiller with extra pinapple, please"}
        };

        public static Dictionary<int, string[]> badPasswords =  new Dictionary<int, string[]>();

        static PasswordDictionary()
        {
            badPasswords.Add(1919, new[] {"Give me Panama Canal", "Give me Palm Channel", "Give me Public Combo"});
            badPasswords.Add(1920, new[] {"I wanna see a deer", "I wanna pet a tiger", "I wanna paint a zebra"});
            badPasswords.Add(1921, new[] {"Greg sent me", "Joe told me to come", "Amelia sent me flowers"});
            badPasswords.Add(1922, new[] {"Strike me good", "Strike me with lighting", "Strike a conversation"});
            badPasswords.Add(1923, new[] {"May I see purple rat?", "May I see black parrot?", "May I see Montana clock?"});
            badPasswords.Add(1924, new[] {"Let me hear Harlem Blues", "I want to hear Baltimore Buzz", "Play that Louisville Lou"});
            badPasswords.Add(1925, new[] {"Make me some Spicy moon", "Make me some Curly moon", "Make me some Sugar sun"});
            badPasswords.Add(1926, new[] {"Show me the Castle Walk", "Can you do a Foxtrot", "Would you like to Tango?"});
            badPasswords.Add(1927, new[] {"You hear from anyone in Paris?", "Anyone ring from Tokyo?", "Someone call you from Moscow?"});
            badPasswords.Add(1928, new[] {"Jarmila’s dancing shoes", "Zelda’s pen and paper", "Gertrude’s swimming cap"});
            badPasswords.Add(1929, new[] {"The Sun Also Rises", "In Our Time", "The Torrents of Spring"});
            badPasswords.Add(1930, new[] {"May I have blink power?", "May I have bath powder?", "May I have bar power?"});
            badPasswords.Add(1931, new[] {"I would have Black widow juice", "I would have Dracula juice, raw", "I would have Tarantula brew on rocks"});
            badPasswords.Add(1932, new[] {"I heard you make mean Beam of whiskey", "I heard you make mean Brick of beer", "I heard you make mean Cinderblock of wine"});
            badPasswords.Add(1933, new[] {"Painkiller with extra straw, please", "Gooddealer with extra pineapple, please", "Painkiller with extra DOLE"});
        }

        public static string GetGoodPassword()
        {
            return goodPasswords[GlobalBar.Year];
        }

        public static string GetBadPassword(int index)
        {
            if(index < badPasswords[GlobalBar.Year].Length)
            {
                return badPasswords[GlobalBar.Year][index];
            }
            else
            {
                return badPasswords[GlobalBar.Year][0];
            }
        }
    }
}