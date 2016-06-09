namespace PixelMagic.Helpers
{
    public class Aura
    {
        private int _auraId;
        private string _auraName;

        public int AuraId
        {
            get
            {
                return _auraId;
            }
        }

        public string AuraName
        {
            get
            {
                return _auraName;
            }
        }

        public Aura(int auraId, string auraName)
        {
            _auraId = auraId;
            _auraName = auraName;
        }
    }
}
