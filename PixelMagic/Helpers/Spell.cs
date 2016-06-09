namespace PixelMagic.Helpers
{
    public class Spell
    {
        private int _spellId;
        private string _spellName;

        public int SpellId
        {
            get
            {
                return _spellId;
            }
        }

        public string SpellName
        {
            get
            {
                return _spellName;
            }
        }

        public Spell(int spellId, string spellName)
        {
            _spellId = spellId;
            _spellName = spellName;
        }
    }
}
