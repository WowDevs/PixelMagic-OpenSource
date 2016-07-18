using System;

namespace PixelMagic.Helpers
{
    public class Spell
    {
        public Spell(int spellId, string spellName, string keyBind, int internalSpellNo)
        {
            InternalSpellNo = internalSpellNo;

            SpellId = spellId;
            SpellName = spellName.Replace("\r", "").Replace("\n", "");
            KeyBind = keyBind.Replace("\r", "").Replace("\n", "");
        }

        public int InternalSpellNo { get; }

        public int SpellId { get; }

        public string SpellName { get; }

        public string KeyBind { get; }

        private static WoW.Keys toKey(string keystr)
        {
            return (WoW.Keys)Enum.Parse(typeof(WoW.Keys), keystr);
        }

        public WoW.Keys Key
        {
            get
            {
                return toKey(KeyBind);
            }
        }
    }
}