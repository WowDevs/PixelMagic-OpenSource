namespace PixelMagic.Helpers
{
    public class Spell
    {
        public Spell(int spellId, string spellName)
        {
            SpellId = spellId;
            SpellName = spellName;
        }

        public int SpellId { get; }

        public string SpellName { get; }
    }
}