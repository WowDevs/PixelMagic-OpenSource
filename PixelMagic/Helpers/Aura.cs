namespace PixelMagic.Helpers
{
    public class Aura
    {
        public int InternalAuraNo { get; }

        public int AuraId { get; }

        public string AuraName { get; }

        public Aura(int auraId, string auraName, int internalAuraNo)
        {
            InternalAuraNo = internalAuraNo;

            AuraId = auraId;
            AuraName = auraName.Replace("\r", "").Replace("\n", "");
        }
    }
}
