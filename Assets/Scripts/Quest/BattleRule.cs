using System;

namespace Quest
{
    public class BattleRule
    {
        public int CanCharacterAttacks(Character[] characters, double adt){
            var characterIndex = 0;
            foreach (var character in characters)
            {
                if (Convert.ToInt32(adt*10) % 10/character.GetSpeed() == 0)
                {
                    return characterIndex;
                }
                characterIndex++;
            }
            return -1;
        }

        public int DecideAttackObject(Character[] characters)
        {
            int i = 0;
            foreach (var character in characters)
            {
                if (character.hp > 0)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}